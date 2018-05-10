﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Player
    {
        private string name { get; }
        private Location location { get; }

        public Player(string playerName)
        {
            name = playerName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Player other = (Player)obj;
            return this.name.Equals(other.name);
        }
    }

    public class RealPlayer : Player
    {
        private Card[] cards { set; get; }

        public RealPlayer(string playerName)
         : base(playerName)
        {
        }
            
        public bool Move (Location location)
        {
            // TODO: check if move is valid
            return false;
        }
    }

    public class DummyPlayer : Player
    {
        public DummyPlayer(string playerName)
         : base(playerName)
        {
        }
    }

}
