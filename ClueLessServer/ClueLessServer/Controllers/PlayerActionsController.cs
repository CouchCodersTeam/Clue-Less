using ClueLessClient.Model.Game;
using ClueLessClient.Network;
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
     * This controller is in charge of API calls for taking
     * a turn
     */
    public class PlayerActionsController : ClueLessController
    {
        [Route("state")]
        [HttpGet]
        public IHttpActionResult GetBoardState()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game;

            if (!game.Started())
                return BadRequest("Game has not started");

            return Ok(game.getGame());
        }

        [Route("command")]
        [HttpGet]
        public IHttpActionResult RequestCommand()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;


            // TODO: this will require waiting/async behavior
            Command command = new Command();
            command.command = CommandType.TakeTurn;

            return Ok(command);
        }

        [Route("move")]
        [HttpPost]
        public IHttpActionResult MovePlayer([FromBody] Location location)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player;

            if (game.movePlayer(player.Name, location))
            {
                // TODO: notify other players of move
                return Created("", "");
            }

            return BadRequest("Invalid move");
        }

        [Route("suggest")]
        [HttpPost]
        public IHttpActionResult MakeSuggestion([FromBody] Accusation accusation)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: validate accusationData data

            var game = auth.game.getGame();
            var player = auth.player;

            var disprovingPlayer = game.makeSuggestion(player.Name, accusation);
            if (disprovingPlayer == null)
            {
                // TODO: notify other players that no one could disprove
                return Created("", "");
            }

            // Receive this from 'DisproveSuggestion'
            SuggestionResult result = null;

            // WARNING: Complicated
            // TODO: notify disprovingPlayer to disprove,
            // take disprovingcard from disprovingPlayer's
            // call to 'DisproveSuggestion() and return to 'player' in this
            // call

            return Created("","");
        }

        [Route("disprove")]
        [HttpPost]
        public IHttpActionResult DisproveSuggestion([FromBody] Card card)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            SuggestionResult result = new SuggestionResult();
            result.card = card;
            result.playerName = auth.player.Name;

            // TODO: send card to player that is waiting in MakeSuggestion call

            return Created("", "");
        }

        [Route("accuse")]
        [HttpPost]
        public IHttpActionResult MakeAccusation([FromBody] Accusation accusation)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player;

            bool isCorrect = game.makeAccusation(player.Name, accusation);
            if (isCorrect)
            {
                // TODO: notify everyone of game win/end
            }
            else
            {
                // notify everyone of incorrect guess
            }

            return Created("", isCorrect);
        }

        [Route("finish")]
        [HttpPost]
        public IHttpActionResult EndTurn()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;


            // TODO

            return Created("", "");
        }

        [Route("solution")]
        [HttpGet]
        public IHttpActionResult GetSolution()
        {
            AuthResult auth = authorizeGame();
            if (auth.result != null)
                return auth.result;

            var game = auth.game;
            if (!game.isEnded)
                return Unauthorized();

            // Game must be over for this API to be called
            return Ok(game.getGame().getSolution());
        }
    }
}
