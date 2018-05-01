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
     * This class gives common behavior to all
     * API controllers for the game
     */
    public class ClueLessController : ApiController
    {
        // TODO: replace pGames with GameDatabase function calls
        protected Dictionary<long, GameModel> pGames = new Dictionary<long, GameModel>();

        /*
         * This class returns the requested data from child controllers.
         * If a request is not authorized, result will be non-null
         */
        protected class AuthResult
        {
            public GameModel game { get; set; }
            public PlayerModel player { get; set; }
            public IHttpActionResult result { get; set; }

            public AuthResult merge(AuthResult auth)
            {
                if (auth.result != null)
                {
                    result = auth.result;
                    game = null;
                    player = null;
                }
                else {
                    if (auth.game != null)
                        game = auth.game;
                    if (auth.player != null)
                        player = auth.player;
                }
                return this;
            }
        }

        public ClueLessController() : base()
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

        protected AuthResult authorizePlayer()
        {
            AuthResult result = new AuthResult();
            result.player = getPlayerFromHeaders();
            if (result.player == null)
                result.result = Unauthorized();
            return result;
        }

        protected AuthResult authorizeGame()
        {
            AuthResult result = new AuthResult();
            result.game = GetGameFromHeaders();
            if (result.game == null)
                result.result = NotFound();
            return result;
        }

        protected AuthResult authorizeGame(long gameId)
        {
            AuthResult result = new AuthResult();
            result.game = GetActiveGame(gameId);
            if (result.game == null)
                result.result = NotFound();
            return result;
        }

        protected AuthResult authorizePlayerAndGame()
        {
            return authorizeGame().merge(authorizePlayer());
        }

        protected AuthResult authorizePlayerAndGame(long gameId)
        {
            return authorizeGame(gameId).merge(authorizePlayer());
        }


        private GameModel GetActiveGame(long gameId)
        {
            GameModel game = null;
            pGames.TryGetValue(gameId, out game);
            return game;
        }

        private GameModel GetGameFromHeaders()
        {
            try
            {
                var values = Request.Headers.GetValues("Session");
                if (values != null && values.Count() > 0)
                {
                    if (long.TryParse(values.ElementAt(0), out long id))
                        return GetActiveGame(id);
                }
            }
            catch (NullReferenceException)
            {
            }

            return null;
        }

        private PlayerModel getPlayerFromHeaders()
        {
            string playerAuthCode = null;
            try
            {
                playerAuthCode = Request.Headers.Authorization.Scheme;
            }
            catch (NullReferenceException)
            {
            }

            return getPlayer(playerAuthCode);
        }

        private PlayerModel getPlayer(string authCode)
        {
            PlayerModel player = null;
            if (authCode != null && authCode.Length != 0)
            {
                // TODO: lookup authcode in player database
                player = new PlayerModel(authCode);
            }

            return player;
        }

    }
}
