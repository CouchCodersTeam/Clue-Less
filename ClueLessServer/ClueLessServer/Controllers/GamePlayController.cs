using ClueLessClient.Model.Game;
using ClueLessServer.Models;
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
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var playerModel = auth.player;

            var cards = game.getPlayerHand(playerModel.Name);

            // Return the calling player's cards
            return Ok(cards);
        }

        [Route("charaters")]
        [HttpGet]
        public IHttpActionResult GetAvailableCharacters()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

        [Route("characters/{name}")]
        [HttpPost]
        public IHttpActionResult ChooseCharacter(string characterName)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            return NotFound();
        }

    }
}
