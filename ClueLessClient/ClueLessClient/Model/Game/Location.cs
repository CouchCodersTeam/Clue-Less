using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Location
    {
        private int xCoordinate;
        private int yCoordinate;
        string playerLocation = new string[4, 4];

        public Location()
        {
            playerLocation[0, 3] = "Scarlet";
            playerLocation[1, 0] = "Plum";
            playerLocation[1, 4] = "Mustard";
            playerLocation[3, 0] = "Peacock";
            playerLocation[4, 1] = "Green";
            playerLocation[4, 3] = "White";

        }

    }
}
