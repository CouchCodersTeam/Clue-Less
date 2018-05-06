using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLessServer.Models
{
    public class GameDatabase
    {
        private Dictionary<long, GameModel> allGames;
        private long currId;

        private GameDatabase()
        {
            allGames = new Dictionary<long, GameModel>();
            currId = 1;
        }

        private static GameDatabase sInstance;

        private static GameDatabase Instance()
        {
            if (sInstance == null)
                sInstance = new GameDatabase();
            return sInstance;
        }

        public static GameModel GetGame(long gameId)
        {
            return Instance().getGame(gameId);
        }

        public static GameModel CreateGame(PlayerModel hostPlayer)
        {
            return Instance().createGame(hostPlayer);
        }

        public static bool RemoveGame(GameModel game)
        {
            return Instance().removeGame(game);
        }

        public static List<GameModel> GetAllGames()
        {
            return Instance().getAllGames();
        }

        public static List<GameModel> GetAllLobbies()
        {
            return Instance().getAllLobbies();
        }

        // instance methods
        private GameModel getGame(long gameId)
        {
            GameModel game = null;
            allGames.TryGetValue(gameId, out game);
            return game;
        }

        // Not thread safe!!
        private GameModel createGame(PlayerModel hostPlayer)
        {
            GameModel game = new GameModel(hostPlayer.Name);
            game.Id = currId++;
            allGames.Add(game.Id, game);
            game.addPlayer(hostPlayer);
            return game;
        }

        private bool removeGame(GameModel game)
        {
            return allGames.Remove(game.Id);
        }

        private List<GameModel> getAllGames()
        {
            return allGames.Values.ToList();
        }

        private List<GameModel> getAllLobbies()
        {
            return getAllGames().Where( game => !game.isStarted).ToList();
        }
    }
}