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

            return Ok(new GameStateModel(game.getGame()));
        }

        [Route("command")]
        [HttpGet]
        public IHttpActionResult RequestCommand()
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: this will require waiting/async behavior
            return NotFound();
        }

        [Route("move")]
        [HttpPost]
        public IHttpActionResult MovePlayer([FromBody] LocationModel location)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player.asPlayer();
            var loc = location.asLocation();

            if (game.movePlayer(player, loc))
            {
                // TODO: notify other players of move
                return Created("", "");
            }

            return BadRequest("Invalid move");
        }

        [Route("suggest")]
        [HttpPost]
        public IHttpActionResult MakeSuggestion([FromBody] AccusationModel accusationData)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: validate accusationData data

            var game = auth.game.getGame();
            var player = auth.player.asPlayer();
            var accusation = accusationData.asAccusation();

            var disprovingPlayer = game.makeSuggestion(player, accusation);
            if (disprovingPlayer == null)
            {
                // TODO: notify other players that no one could disprove
                return Created("", "");
            }

            // WARNING: Complicated
            // TODO: notify disprovingPlayer to disprove,
            // take disprovingcard from disprovingPlayer's
            // call to 'DisproveSuggestion() and return to 'player' in this
            // call

            return Created("","");
        }

        [Route("disprove")]
        [HttpPost]
        public IHttpActionResult DisproveSuggestion([FromBody] CardListModel cardList)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            // TODO: validate cardList (validate it is only 1 card)
            // TODO: send card to player that is waiting in MakeSuggestion call

            return NotFound();
        }

        [Route("accuse")]
        [HttpPost]
        public IHttpActionResult MakeAccusation([FromBody] AccusationModel accusationData)
        {
            AuthResult auth = authorizeAndVerifyGameStart();
            if (auth.result != null)
                return auth.result;

            var game = auth.game.getGame();
            var player = auth.player.asPlayer();
            var accusation = accusationData.asAccusation();

            bool isCorrect = game.makeAccusation(player, accusation);
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
            return Ok(new AccusationModel(game.getGame().getSolution()));
        }
    }
}
