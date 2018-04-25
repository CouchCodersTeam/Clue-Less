using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClueLessServer.Models
{
    public class GameModel
    {
        private static int IdCount = 0;

        public long Id { get; set; }
        public string Name { get; set; }

        public GameModel(string name)
        {
            Name = name;

            // Not thread safe
            Id = ++IdCount;
        }
    }
}