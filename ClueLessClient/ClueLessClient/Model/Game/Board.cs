using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Board
    {
        public Location[,] locations;

        // Initializing the rooms and hallways
        public Board()
        {
            // C#'s unique 2D array initialization
            // If were Location[][], you have to initialize
            // the row array (Location[5][]), then initialize each row with a new
            // Location[5]
            locations = new Location[5,5];

            locations[0, 0] = new Location(0, 0, "Study");
            locations[0, 2] = new Location(0, 2, "Hall");
            locations[0, 4] = new Location(0, 4, "Lounge");
            locations[2, 0] = new Location(2, 0, "Library");
            locations[2, 2] = new Location(2, 2, "Billiard");
            locations[2, 4] = new Location(2, 4, "Dining");
            locations[4, 0] = new Location(4, 0, "Conservatory");
            locations[4, 2] = new Location(4, 2, "Ballroom");
            locations[4, 4] = new Location(4, 4, "Kitchen");

            locations[0, 1] = new Location(0, 1, "Hallway");
            locations[0, 3] = new Location(0, 3, "Hallway");
            locations[2, 1] = new Location(2, 1, "Hallway");
            locations[2, 3] = new Location(2, 3, "Hallway");
            locations[4, 1] = new Location(4, 1, "Hallway");
            locations[4, 3] = new Location(4, 3, "Hallway");
            locations[1, 0] = new Location(1, 0, "Hallway");
            locations[1, 2] = new Location(1, 2, "Hallway");
            locations[1, 4] = new Location(1, 4, "Hallway");
            locations[3, 0] = new Location(3, 0, "Hallway");
            locations[3, 2] = new Location(3, 2, "Hallway");
            locations[3, 4] = new Location(3, 4, "Hallway");
        }
    }

    // Combination of Room and Hallway class in original design
    public class Location
    {
        private int xCoordinate { get; }
        private int yCoordinate { get; }
        private string locationName { get; }
        public List<Player> occupants = new List<Player>();


        public Location(int x, int y, string name)
        {
            xCoordinate = x;
            yCoordinate = y;
            locationName = name;
        }

        public bool addPlayer(Player player)
        {
            if (locationName.Equals("Hallway"))
            {
                if (occupants.Count == 0)
                {
                    occupants.Add(player);
                    return true;
                } else
                {
                    return false;
                }

            } else
            {
                occupants.Add(player);
                return true;
            }

        }

    }
}
