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
        private List<PlayerModel> players; // TODO: this could be added to Game object

        public bool isStarted { get; }

        public GameModel(string hostname)
        {
            Hostname = hostname;
            Id = -1;
            game = new Game();
            isStarted = false;
            players = new List<PlayerModel>();
        }

        // TODO: these methods can be moved to 'Game'
        public bool addPlayer(PlayerModel player)
        {
            if (!containsPlayer(player))
            {
                players.Add(player);
                return true;
            }
            return false;
        }

        public bool removePlayer(PlayerModel player)
        {
            return players.Remove(player);
        }

        public bool containsPlayer(PlayerModel player)
        {
            return players.Contains(player);
        }

        public bool start()
        {
            return false;
        }
    }
}