using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ClueLessServer.Controllers;
using ClueLessServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClueLessServer.Tests.Controllers
{
    [TestClass]
    public class GamesControllerTest
    {
        [TestMethod]
        public void TestGameCreate()
        {
            GamesController controller = new GamesController();

            var result = controller.GetGames();
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<List<GameModel>>));

            var gameList = ((OkNegotiatedContentResult<List<GameModel>>)result).Content;

            long nextExpectedGameId = 0;
            if (gameList.Count != 0)
            {
                nextExpectedGameId = gameList[gameList.Count - 1].Id + 1;
            }

            // test that nextExpectedGameId does not yet exist
            result = controller.GetGame(nextExpectedGameId);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

            // verify that all others in gamelist can be found
            foreach (GameModel game in gameList) {
                result = controller.GetGame(game.Id);
                Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<GameModel>));
                Assert.AreEqual(game, ((OkNegotiatedContentResult<GameModel>)result).Content);
            }

            // Test Game Create and verify you can get it
            result = controller.CreateGame();

            // Need to provide authorization to create a game
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));

            // Add Authorization
            var context = new HttpControllerContext();
            var request = new HttpRequestMessage();

            request.Headers.Add("Authorization", "abc123");

            controller.ControllerContext = context;
            controller.Request = request;

            result = controller.CreateGame();
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<GameModel>));

            GameModel newGame = ((CreatedNegotiatedContentResult<GameModel>)result).Content;

            Assert.AreEqual("abc123", newGame.Hostname);  // TODO: This behavior will change
            Assert.AreEqual(nextExpectedGameId, newGame.Id);


            // TODO: JoinGame, LeaveGame, WaitForGame, StartGame
        }
    }
}
