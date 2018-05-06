using ClueLessServer.Controllers;
using ClueLessServer.Models;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http.Results;

namespace ClueLessServer.Tests.TestHelpers
{
    using PlayerData = AuthCodeController.PlayerData;

    class TestData
    {
        public static List<PlayerData> getAuthorizedPlayers()
        {
            var players = new List<PlayerData>();
            String[] names = { "Harry Potter", "Hermione Granger", "Ronald Weasley" };
            var authCodeController = new AuthCodeController();

            foreach (var name in names) {
                PlayerData data = new PlayerData();
                data.PlayerName = name;
                var result = authCodeController.GetAuthCode(data);
                Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<PlayerData>));
                players.Add(data);
            }

            return players;
        }
    }
}
