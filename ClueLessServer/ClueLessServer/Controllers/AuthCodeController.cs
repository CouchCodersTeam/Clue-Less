using System.Collections.Generic;
using System.Web.Http;
using Newtonsoft.Json;

namespace ClueLessServer.Controllers
{
    public class AuthCodeController : ApiController
    {
        private const string PROVIDE_PLAYER_NAME = "Provide a {'PlayerName': [PlayerName]} in request body.";
        
        public class PlayerNameData
        {
            public string PlayerName { get; set; }
        }

        // POST: /auth_code
        [Route("auth_code")]
        [HttpPost]
        public IHttpActionResult GetAuthCode([FromBody]PlayerNameData value)
        {
            if (value.PlayerName == null)
            {
                return BadRequest(PROVIDE_PLAYER_NAME);
            }

            // return dummy data
            Dictionary<string, object> test = new Dictionary<string, object>();
            test.Add("PlayerName", value.PlayerName);
            test.Add("AuthCode", "123abc");

            // TODO: use PlayerIdentity class to get auth code

            // return empty location. There is not a location to view this
            // item
            return Created("", test);
        }
    }
}
