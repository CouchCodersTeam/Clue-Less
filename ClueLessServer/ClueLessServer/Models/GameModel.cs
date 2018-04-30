using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ClueLessServer.Models
{
    [DataContract]
    public class GameModel
    {
        private static int IdCount = 0;

        [DataMember]
        public long Id { get; set; }
        [DataMember]
        public string Hostname { get; set; }

        public GameModel(string hostname)
        {
            Hostname = hostname;

            // Not thread safe
            Id = ++IdCount;
        }
    }
}