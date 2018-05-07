using ClueLessClient.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLessServer.Models
{
    public class PlayerModel
    {
        public string Name { get; set; }

        public PlayerModel(string name)
        {
            Name = name;
        }

        public Player asPlayer()
        {
            return new Player(Name);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            PlayerModel other = (PlayerModel)obj;
            return this.Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }
    }
}