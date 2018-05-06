using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Results;
using ClueLessServer.Controllers;
using ClueLessServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClueLessServer.Tests.Controllers
{
    using GetGameListType = OkNegotiatedContentResult<List<GameModel>>;
    using GetGameType = OkNegotiatedContentResult<GameModel>;
    using PostGameType = CreatedNegotiatedContentResult<GameModel>;

    [TestClass]
    public class GamesControllerTest
    {
        [TestMethod]
        public void TestGameCreationFlow()
        {
            var game = TestGameCreate();
            TestGameJoin(game);
            TestGameLeave(game);
            // TODO: WaitForGame, StartGame
        }

        private GameModel TestGameCreate()
        {
            GamesController controller = new GamesController();

            var result = controller.GetGames();
            Assert.IsInstanceOfType(result, typeof(GetGameListType));

            var gameList = ((GetGameListType)result).Content;

            long nextExpectedGameId = 1;
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
                Assert.IsInstanceOfType(result, typeof(GetGameType));
                Assert.AreEqual(game, ((GetGameType)result).Content);
            }

            var player = TestHelpers.TestData.getAuthorizedPlayers()[0];

            // Test Game Create and verify you can get it
            result = controller.CreateGame();

            // Need to provide authorization to create a game
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));

            // Add Authorization
            var context = new HttpControllerContext();
            var request = new HttpRequestMessage();

            request.Headers.Add("Authorization", player.AuthCode);

            controller.ControllerContext = context;
            controller.Request = request;

            result = controller.CreateGame();
            Assert.IsInstanceOfType(result, typeof(PostGameType));

            GameModel newGame = ((PostGameType)result).Content;

            Assert.AreEqual(player.PlayerName, newGame.Hostname);
            Assert.AreEqual(nextExpectedGameId, newGame.Id);

            // Test that game can now be found
            // test that nextExpectedGameId does not yet exist
            result = controller.GetGame(newGame.Id);
            Assert.IsInstanceOfType(result, typeof(GetGameType));

            return newGame;
        }

        private void TestGameJoin(GameModel game)
        {
            GamesController controller = new GamesController();
            // Add Authorization
            var context = new HttpControllerContext();
            var request = new HttpRequestMessage();

            controller.ControllerContext = context;
            controller.Request = request;


            var players = TestHelpers.TestData.getAuthorizedPlayers();

            // players[0] created game in previous test

            // verify host cannot join game
            request.Headers.Add("Authorization", players[0].AuthCode);
            var result = controller.JoinGame(game.Id);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));

            // verify other players can join game, but cannot join a second time
            for (int i = 1; i < players.Count; i++)
            {
                var testPlayer = players[i];
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", testPlayer.AuthCode);

                // can join
                result = controller.JoinGame(game.Id);
                Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
                Assert.AreEqual(HttpStatusCode.NoContent, ((StatusCodeResult)result).StatusCode);

                // cannot join again
                result = controller.JoinGame(game.Id);
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
        }

        private void TestGameLeave(GameModel game)
        {
            // remove everyone from game, player[0] is the host

            GamesController controller = new GamesController();
            // Add Authorization
            var context = new HttpControllerContext();
            var request = new HttpRequestMessage();

            controller.ControllerContext = context;
            controller.Request = request;

            var players = TestHelpers.TestData.getAuthorizedPlayers();

            // verify other players can leave game, but cannot leave a second time
            for (int i = players.Count - 1; i >= 0; i--)
            {
                var testPlayer = players[i];
                request.Headers.Remove("Authorization");
                request.Headers.Add("Authorization", testPlayer.AuthCode);

                // can leave
                var result = controller.LeaveGame(game.Id);
                Assert.IsInstanceOfType(result, typeof(OkResult));

                // cannot leave again
                result = controller.LeaveGame(game.Id);
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }

            // verify game no longer exists
            var getResult = controller.GetGame(game.Id);
            Assert.IsInstanceOfType(getResult, typeof(NotFoundResult));
        }

    }
}
