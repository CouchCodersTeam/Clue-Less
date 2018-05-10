using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    class RequestModels
    {
        [DataContract]
        public class PlayerIdentityModel
        {
            [DataMember]
            public string PlayerName { get; set; }
            [DataMember]
            public string AuthCode { get; set; }
        }

        [DataContract]
        public class Lobby
        {
            [DataMember]
            public long Id { get; set; }
            [DataMember]
            public string Hostname { get; set; }
        }
    }
}
