using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using ClueLessServer.Controllers;
using System.Web.Http.Results;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace ClueLessServer.Tests.Controllers
{
    [TestClass]
    public class AuthCodeControllerTest
    {
        [TestMethod]
        public async Task TestGetAuthCode()
        {
            AuthCodeController controller = new AuthCodeController();

            AuthCodeController.PlayerNameData testData = new AuthCodeController.PlayerNameData();

            // test empty body
            var result = controller.GetAuthCode(testData);
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));

            testData.PlayerName = "Harry Potter";

            result = controller.GetAuthCode(testData);

            CancellationToken token = new CancellationToken();
            
            // Assert successful result
            Assert.IsInstanceOfType(result, typeof(CreatedNegotiatedContentResult<Dictionary<string, object>>));
            var response = (CreatedNegotiatedContentResult<Dictionary<string, object>>)result;
            var responseData = response.Content;

            // Test return data
            object value = null;

            Assert.IsTrue(responseData.ContainsKey("PlayerName"));
            responseData.TryGetValue("PlayerName", out value);
            Assert.AreEqual(testData.PlayerName, value);

            Assert.IsTrue(responseData.ContainsKey("AuthCode"));
            responseData.TryGetValue("AuthCode", out value);
            Assert.AreEqual("123abc", value);
        }
    }
}
