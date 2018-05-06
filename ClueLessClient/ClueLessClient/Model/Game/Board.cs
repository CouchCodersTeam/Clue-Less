using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Board
    {
        int[][] locations = new int [][] {}; //Create array
    }
        public class Location
    {
        private int xCoordinate;
        private int yCoordinate;
        string locations = new string[4, 4];
        string playerLocation = new string[4, 4];
        int hallwayOccupied = 0;

        public Location()
        {
            locations[0, 0] = "Study";
            locations[0, 2] = "Hall";
            locations[0, 4] = "Lounge";
            locations[2, 0] = "Library";
            locations[2, 2] = "Billiard";
            locations[2, 4] = "Dining";
            locations[4, 0] = "Conservatory";
            locations[4, 2] = "Ballroom";
            locations[4, 4] = "Kitchen";

            locations[0, 1] = "Hallway";
            locations[0, 3] = "Hallway";
            locations[2, 1] = "Hallway";
            locations[2, 3] = "Hallway";
            locations[4, 1] = "Hallway";
            locations[4, 3] = "Hallway";
            locations[1, 0] = "Hallway";
            locations[1, 2] = "Hallway";
            locations[1, 4] = "Hallway";
            locations[3, 0] = "Hallway";
            locations[3, 2] = "Hallway";
            locations[3, 4] = "Hallway";


            playerLocation[0, 3] = "Scarlet";
            playerLocation[1, 0] = "Plum";
            playerLocation[1, 4] = "Mustard";
            playerLocation[3, 0] = "Peacock";
            playerLocation[4, 1] = "Green";
            playerLocation[4, 3] = "White";


        }

        public addPlayer()
        {
            if (Location.locations == "Hallway" && hallwayOccupied == 1)
            {
                throw new ArgumentException("Hallway is already occupied.");
            }
            else if (Location.locations == "Hallway" && hallwayOccupied == 0)
            {
                playerLocation = new string[xCoordinate, yCoordinate];
                playerLocation = Console.ReadLine;
            }

        }


    }
}
