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
    }
}