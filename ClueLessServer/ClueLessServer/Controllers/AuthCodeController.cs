using System.Collections.Generic;
using System.Web.Http;
using ClueLessServer.Models;
using Newtonsoft.Json;

namespace ClueLessServer.Controllers
{
    public class AuthCodeController : ClueLessController
    {
        private const string PROVIDE_PLAYER_NAME = "Provide a {'PlayerName': [PlayerName]} in request body.";
        
        public class PlayerData
        {
            public string PlayerName { get; set; }
            public string AuthCode { get; set; }
        }

        // POST: /auth_code
        [Route("auth_code")]
        [HttpPost]
        public IHttpActionResult GetAuthCode([FromBody]PlayerData value)
        {
            if (value.PlayerName == null)
            {
                return BadRequest(PROVIDE_PLAYER_NAME);
            }

            PlayerModel player = new PlayerModel(value.PlayerName);
            value.AuthCode = PlayerDatabase.GetOrCreateAuthCode(player);

            // return empty location. There is not a location to view this
            // item
            return Created("", value);
        }
    }
}
