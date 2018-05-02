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
            return Ok(pGames.Values.ToList());
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

            GameModel newGame = new GameModel(player.Name);
            pGames.Add(newGame.Id, newGame);

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

            // TODO: add player to game

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: /games/5/begin
        [Route("games/{id}/begin")]
        [HttpPost]
        // This API can only be called by the host player
        public IHttpActionResult StartGame(long id)
        {
            AuthResult auth = authorizePlayerAndGame(id);

            if (auth.result != null)
                return auth.result;

            // TODO: start game, signal to other players that game has started

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: /games/5/begin
        [Route("games/{id}/begin")]
        [HttpGet]
        public IHttpActionResult WaitForGame(long id)
        {
            AuthResult auth = authorizePlayerAndGame(id);

            if (auth.result != null)
                return auth.result;

            // TODO: wait for game to start. Turn API into async and
            // return a timeout if game has not started. Return OK
            // once host player hits start

            // will probably require use of a mutex and a condition variable
            // with a time out

            return Ok();
        }

        // DELETE: /games/5
        [Route("games/{id}")]
        [HttpDelete]
        public IHttpActionResult LeaveGame(long id)
        {
            if (!pGames.ContainsKey(id))
            {
                return NotFound();
            }
            else
            {
                return Ok();
            }
        }

    }
}
