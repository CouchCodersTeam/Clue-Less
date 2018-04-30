using ClueLessServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ClueLessServer.Controllers
{
    public class GamesController : ApiController
    {
        // TODO: replace pGames with GameDatabase function calls
        private Dictionary<long, GameModel> pGames = new Dictionary<long, GameModel>();

        public GamesController() : base()
        {
            // Generate Dummy Data
            List<GameModel> games = new List<GameModel>();

            games.Add(new GameModel("John Doe"));
            games.Add(new GameModel("Sean Connery"));
            games.Add(new GameModel("ClueMaster779"));

            foreach (GameModel game in games)
            {
                pGames.Add(game.Id, game);
            }

        }

        // GET: /games
        [Route("games")]
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
            GameModel game = GetActiveGame(id);
            if (game != null)
                return Ok(game);
            else
                return NotFound();
        }

        // POST: /games
        [Route("games")]
        [HttpPost]
        public IHttpActionResult CreateGame()
        {
            PlayerModel player = authorizePlayer();

            if (player == null)
            {
                return Unauthorized();
            }

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
            PlayerModel player = authorizePlayer();
            if (player == null)
                return Unauthorized();

            GameModel game = GetActiveGame(id);
            if (game == null)
                return NotFound();

            // TODO: add player to game

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: /games/5/begin
        [Route("games/{id}/begin")]
        [HttpPost]
        // This API can only be called by the host player
        public IHttpActionResult StartGame(long id)
        {
            var player = authorizePlayer();
            var game = GetActiveGame(id);

            if (player == null)
                return Unauthorized();
            else if (game == null)
                return NotFound();

            // TODO: start game, signal to other players that game has started

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: /games/5/begin
        [Route("games/{id}/begin")]
        [HttpGet]
        public IHttpActionResult WaitForGame(long id)
        {
            var player = authorizePlayer();
            var game = GetActiveGame(id);

            if (player == null)
                return Unauthorized();
            else if (game == null)
                return NotFound();

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

        private PlayerModel authorizePlayer()
        {
            return PlayerIdentity.authorizePlayer(Request);
        }

        private GameModel GetActiveGame(long gameId)
        {
            GameModel game = null;
            pGames.TryGetValue(gameId, out game);
            return game;
        }
    }
}
