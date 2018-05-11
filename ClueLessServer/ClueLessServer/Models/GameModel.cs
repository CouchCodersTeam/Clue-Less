using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using ClueLessClient.Model.Game;

namespace ClueLessServer.Models
{
    [DataContract]
    public class GameModel
    {
        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Hostname { get; set; }

        private Game game;

        private bool isStarted;
        public bool isEnded { get; }

        public GameModel(string hostname)
        {
            Hostname = hostname;
            Id = -1;
            game = new Game();
            isStarted = false;
        }

        public bool Started()
        {
            return isStarted;
        }

        public bool addPlayer(PlayerModel player)
        {
            return game.addPlayer(player.Name);
        }

        public bool removePlayer(PlayerModel player)
        {
            return game.removePlayer(player.Name);
        }

        public bool containsPlayer(PlayerModel player)
        {
            return game.containsPlayer(player.Name);
        }

        public bool start()
        {
            if (game.canStartGame())
            {
                game.startGame();
                isStarted = true;
                return true;
            }
            return false;
        }
        
        public Game getGame()
        {
            return game;
        }
    }
}