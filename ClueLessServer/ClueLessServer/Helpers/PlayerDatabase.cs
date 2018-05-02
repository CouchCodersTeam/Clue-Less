using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using ClueLessServer.Helpers;

namespace ClueLessServer.Models
{
    using PlayerAuthCode = String;

    /*
     * The player database returns representations of players.
     * Not the actual player objects found in the games
     */
    public class PlayerDatabase
    {

        private Dictionary<PlayerAuthCode, PlayerModel> mAllPlayers;
        private AuthCodeGenerator generator;

        private PlayerDatabase()
        {
            mAllPlayers = new Dictionary<PlayerAuthCode, PlayerModel>();
            generator = new AuthCodeGenerator();
        }

        private static PlayerDatabase sInstance;

        private static PlayerDatabase Instance()
        {
            if (sInstance == null)
                sInstance = new PlayerDatabase();
            return sInstance;
        }

        public static void SetTestAuthCodeGenerator(AuthCodeGenerator generator)
        {
            Instance().generator = generator;
        }

        public static PlayerModel GetPlayer(PlayerAuthCode authCode)
        {
            return Instance().getPlayer(authCode);
        }

        public static PlayerAuthCode GetOrCreateAuthCode(PlayerModel player)
        {
            return Instance().getOrCreateAuthCode(player);
        }



        // instance methods
        private PlayerModel getPlayer(PlayerAuthCode authCode)
        {
            PlayerModel player = null;
            mAllPlayers.TryGetValue(authCode, out player);
            return player;
        }

        /*
         * This function is not effecient, but it shouldn't be called often
         * and we won't be having very many players in this project
         */
        private PlayerAuthCode getOrCreateAuthCode(PlayerModel player)
        {
            foreach(var pair in mAllPlayers)
            {
                if (pair.Value.Equals(player))
                    return pair.Key;
            }

            // if not found, create one
            PlayerAuthCode newCode;
            do
            {
                newCode = generator.generateAuthCode();
            } while (mAllPlayers.ContainsKey(newCode));

            mAllPlayers.Add(newCode, player);

            return newCode;
        }
    }
}