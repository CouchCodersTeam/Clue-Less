using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClueLessServer.Controllers
{
    /*
     * This class is in charge of setting up game components
     * for example: Cards, etc
     */
    public class GamePlayController : ClueLessController
    {
        [Route("cards")]
        [HttpGet]
        // [Authorize]
        public IHttpActionResult GetPlayerCards()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            // Return the calling player's cards
            return NotFound();
        }

        [Route("charaters")]
        [HttpGet]
        public IHttpActionResult GetAvailableCharacters()
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("characters/{name}")]
        [HttpPost]
        public IHttpActionResult ChooseCharacter(string characterName)
        {
            AuthResult auth = authorizePlayerAndGame();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

    }
}
