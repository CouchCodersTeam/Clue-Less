using ClueLessClient.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ClueLessServer.Models
{
    public class LocationModel
    {
        [DataMember]
        public int xCoordinate { get; set; }
        [DataMember]
        public int yCoordinate { get; set; }

        public LocationModel()
        {
            xCoordinate = 0;
            yCoordinate = 0;
        }

        public LocationModel(Location loc)
        {
            // TODO
        }

        public Location asLocation()
        {
            // TODO
            return new Location(0, 0, "");
        }
    }
}