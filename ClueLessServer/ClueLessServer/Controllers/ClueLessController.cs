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
            result.game = GameDatabase.GetGame(gameId);
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

        protected AuthResult authorizePlayerMatchesGame()
        {
            AuthResult result = new AuthResult();
            var game = GetGameFromHeaders();
            if (game == null)
                result.result = NotFound();
            else
                result = authorizePlayerMatchesGame(game.Id);
            return result;
        }

        protected AuthResult authorizePlayerMatchesGame(long gameId)
        {
            AuthResult auth = authorizePlayerAndGame(gameId);
            if (auth.game != null && !auth.game.containsPlayer(auth.player))
            {
                auth.game = null;
                auth.player = null;
                auth.result = NotFound();
            }
            return auth;
        }

        protected AuthResult authorizeAndVerifyGameStart()
        {
            AuthResult auth = authorizePlayerMatchesGame();
            if (auth.result != null)
                return auth;

            if (!auth.game.isStarted)
            {
                auth.player = null;
                auth.game = null;
                auth.result = BadRequest("Game has not started yet");
            }
            return auth;
        }


        private GameModel GetActiveGame(long gameId)
        {
            return GameDatabase.GetGame(gameId);
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
                player = PlayerDatabase.GetPlayer(authCode);
            }

            return player;
        }

    }
}
