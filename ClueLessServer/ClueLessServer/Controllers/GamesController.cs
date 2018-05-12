using ClueLessClient.Network;
using ClueLessServer.Helpers;
using ClueLessServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClueLessServer.Controllers
{
    public class GamesController : ClueLessController
    {
        // GET: /games
        [Route("games")]
        [Route("")] // default
        [HttpGet]
        public IHttpActionResult GetGames()
        {
            // return a list of games
            return Ok(GameDatabase.GetAllLobbies());
        }

        // GET: /games/5
        [Route("games/{id}")]
        [HttpGet]
        public IHttpActionResult GetGame(long id)
        {
            AuthResult auth = authorizeGame(id);

            if (auth.result != null)
                return auth.result;
            else
                return Ok(auth.game);
        }

        // POST: /games
        [Route("games")]
        [HttpPost]
        public IHttpActionResult CreateGame()
        {
            AuthResult auth = authorizePlayer();

            if (auth.result != null)
                return auth.result;

            PlayerModel player = auth.player;

            GameModel newGame = GameDatabase.CreateGame(player);

            string location = "/games/" + newGame.Id;
            return Created(location, newGame);
        }

        // POST: /games/5
        [Route("games/{id}")]
        [HttpPost]
        public IHttpActionResult JoinGame(long id)
        {
            AuthResult auth = authorizePlayerAndGame(id);

            if (auth.result != null)
                return auth.result;

            if (auth.game.addPlayer(auth.player))
                return StatusCode(HttpStatusCode.NoContent);
            else
                return BadRequest();
        }

        // POST: /games/begin
        [Route("games/begin")]
        [HttpPost]
        // This API can only be called by the host player
        public IHttpActionResult StartGame()
        {
            AuthResult auth = authorizePlayerMatchesGame();

            if (auth.result != null)
                return auth.result;

            PlayerModel player = auth.player;
            GameModel game = auth.game;

            if (!game.Hostname.Equals(player.Name))
                return Unauthorized();

            if (game.start())
            {
                var cmd = new Command { command = CommandType.GameStart };
                CommandInterface.SetCommandForEveryone(game, cmd);

                var startPlayer = game.getGame().getPlayerTurn().name;
                cmd = new Command { command = CommandType.TakeTurn };
                CommandInterface.SetCommandForPlayer(startPlayer, cmd);

                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest();
            }

        }

        // GET: /games/begin
        [Route("games/begin")]
        [HttpGet]
        public IHttpActionResult WaitForGame()
        {
            AuthResult auth = authorizePlayerMatchesGame();

            if (auth.result != null)
                return auth.result;

            if (auth.game.Started())
            {
                return Ok();
            }

            // TODO: wait for game to start. Turn API into async and
            // return a timeout if game has not started. Return OK
            // once host player hits start


            // will probably require use of a mutex and a condition variable
            // with a time out

            return StatusCode(HttpStatusCode.RequestTimeout);
        }

        // DELETE: /games/5
        [Route("games/{id}")]
        [HttpDelete]
        public IHttpActionResult LeaveGame(long id)
        {
            AuthResult auth = authorizePlayerMatchesGame(id);

            if (auth.result != null)
                return auth.result;

            if (auth.game.removePlayer(auth.player))
            {
                // if host leaves, game is disbanded
                if (auth.game.Hostname.Equals(auth.player.Name))
                {
                    GameDatabase.RemoveGame(auth.game);
                    // TODO: notify remaining players in game
                }

                return Ok();
            }
            else
                return BadRequest();
        }

    }
}
