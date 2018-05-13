using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    [DataContract]
    public class Coordinate
    {
        [DataMember]
        public int x;
        [DataMember]
        public int y;

        public Coordinate(int xCoordinate, int yCoordinate)
        {
            x = xCoordinate;
            y = yCoordinate;
        }
    }

    [DataContract]
    public class Board
    {
        // Multidimensional arrays are not serializable, but
        // jagged arrays are
        [DataMember]
        public Location[][] locations;

        // Initializing the rooms and hallways
        public Board()
        {
            // C#'s unique 2D array initialization
            // If were Location[][], you have to initialize
            // the row array (Location[5][]), then initialize each row with a new
            // Location[5]
            // locations = new Location[5,5];
            string[][] roomNames = new string[][]
            {
                new string[]{ "Study",         "Hallway",  "Hall",      "Hallway",  "Lounge"},

                new string[]{ "Hallway",       null,       "Hallway",   null,       "Hallway"},

                new string[]{ "Library",       "Hallway",  "Billiard",  "Hallway",  "Dining"},

                new string[]{ "Hallway",       null,       "Hallway",   null,       "Hallway"},

                new string[]{ "Conservatory",  "Hallway",  "Ballroom",  "Hallway",  "Kitchen"}
            };

            locations = new Location[roomNames.Length][];
            for (int count = 0; count < locations.Length; count++)
            {
                locations[count] = new Location[roomNames[count].Length];
            }

            // rows are 'y', cols are 'x'
            for (int row = 0; row < roomNames.Length; row++)
            {
                string[] rowValues = roomNames[row];
                for (int col = 0; col < rowValues.Length; col++)
                {
                    var roomName = rowValues[col];
                    if (roomName != null)
                        locations[row][col] = new Location(col, row, roomName);
                }
            }
        }

        public bool MovePlayer(Player player, Coordinate loc)
        {
            if (player.location != null)
            {
                // Player should have real location object
                Location playerLocation = GetLocation(player.location);
                playerLocation.occupants.Remove(player);
            }

            Location newLocation = GetLocation(loc);

            newLocation.occupants.Add(player);
            player.location = loc;

            return true;
        }

        public Location GetLocation(Coordinate coord)
        {
            return GetLocation(coord.x, coord.y);
        }

        public Location GetLocation(int x, int y)
        {
            return locations[y][x];
        }

        public Location UpFrom(Location loc)
        {
            if (loc == null || loc.yCoordinate == 0)
                return null;
            return locations[loc.yCoordinate - 1][loc.xCoordinate];
        }

        public Location DownFrom(Location loc)
        {
            if (loc == null || loc.yCoordinate >= locations[loc.xCoordinate].Length - 1)
                return null;
            return locations[loc.yCoordinate + 1][loc.xCoordinate];
        }

        public Location LeftFrom(Location loc)
        {
            if (loc == null || loc.xCoordinate == 0)
                return null;
            return locations[loc.yCoordinate][loc.xCoordinate - 1];
        }

        public Location RightFrom(Location loc)
        {
            if (loc == null || loc.yCoordinate >= locations.Length - 1)
                return null;
            return locations[loc.yCoordinate][loc.xCoordinate + 1];
        }
    }

    // Combination of Room and Hallway class in original design
    [DataContract]
    [KnownType(typeof(RealPlayer))]
    [KnownType(typeof(DummyPlayer))]
    public class Location
    {
        [DataMember]
        public int xCoordinate { get; set; }
        [DataMember]
        public int yCoordinate { get; set; }
        [DataMember]
        public string locationName { get; set; }
        [DataMember]
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
                // Hallway holds up to 1 player only
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

        public Coordinate GetCoordinate()
        {
            return new Coordinate(xCoordinate, yCoordinate);
        }

        // Return true if the location is directly accessible without
        // considering the occupants in hallways.
        public bool isNextTo(Location location) {
            if (location == null || location.isInvalid()) { return false; }

            if (xCoordinate == location.xCoordinate) {
                return Math.Abs(yCoordinate - location.yCoordinate) == 1;
            } else if (yCoordinate == location.yCoordinate) {
                return Math.Abs(xCoordinate - location.xCoordinate) == 1;
            } else if (isSecretPassage() && location.isSecretPassage()) {
                // Must be diagonal --> x's and y's should be different
                return xCoordinate != location.xCoordinate &&
                    yCoordinate != location.yCoordinate;
            } else {
                return false;
            }
        }

        // Return true if location is at 4 corners
        public bool isSecretPassage() {
            return (xCoordinate == 0 && yCoordinate == 0) ||
                (xCoordinate == 0 && yCoordinate == 4) ||
                (xCoordinate == 4 && yCoordinate == 0) ||
                (xCoordinate == 4 && yCoordinate == 4);
        }

        // Return true iff the location is neither hallway nor room
        public bool isInvalid() {
            // these are the locations of the 'null' locations
            return (xCoordinate == 1 && yCoordinate == 1) ||
                (xCoordinate == 1 && yCoordinate == 3) ||
                (xCoordinate == 3 && yCoordinate == 1) ||
                (xCoordinate == 3 && yCoordinate == 3);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Location other = (Location)obj;
            return xCoordinate.Equals(other.xCoordinate) &&
                yCoordinate.Equals(other.yCoordinate) &&
                locationName.Equals(other.locationName);
        }

    }
}
