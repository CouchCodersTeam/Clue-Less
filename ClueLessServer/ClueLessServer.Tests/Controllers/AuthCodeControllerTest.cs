using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ClueLessServer.Controllers;
using System.Web.Http.Results;
using ClueLessServer.Models;

namespace ClueLessServer.Tests.Controllers
{
    using PlayerData = AuthCodeController.PlayerData;

    [TestClass]
    public class AuthCodeControllerTest
    {

        private static string GENERATED_AUTH_CODE = "81fcd0a";
        private class PredictableGenerator : Helpers.AuthCodeGenerator
        {
            public override string generateAuthCode()
            {
                return GENERATED_AUTH_CODE;
            }
        }

        [TestMethod]
        public void TestGetAuthCode()
        {
            PlayerDatabase.SetTestAuthCodeGenerator(new PredictableGenerator());

            AuthCodeController controller = new AuthCodeController();

            PlayerData testData = new PlayerData();

            // test empty body
            var result = controller.GetAuthCode(testData);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));

            testData.PlayerName = "Harry Potter";

            result = controller.GetAuthCode(testData);

            // Assert successful result
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<PlayerData>));
            var response = (CreatedNegotiatedContentResult<PlayerData>)result;
            var responseData = response.Content;

            // Test return data
            Assert.AreEqual(testData.PlayerName, responseData.PlayerName);
            Assert.AreEqual(GENERATED_AUTH_CODE, responseData.AuthCode);
        }

        [TestCleanup]
        public void AuthCodeCleanup()
        {
            PlayerDatabase.SetTestAuthCodeGenerator(new Helpers.AuthCodeGenerator());
        }
    }
}
