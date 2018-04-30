using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ClueLessServer.Models
{
    public class PlayerIdentity
    {
        public static PlayerModel authorizePlayer(HttpRequestMessage Request)
        {
            string playerAuthCode = null;
            try
            {
                playerAuthCode = Request.Headers.Authorization.Scheme;
            }
            catch (NullReferenceException)
            {
            }

            return authorizePlayer(playerAuthCode);
        }

        public static PlayerModel authorizePlayer(string authCode)
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