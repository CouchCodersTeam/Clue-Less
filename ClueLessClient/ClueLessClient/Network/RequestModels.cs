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
        public class PlayerNameModel
        {
            [DataMember]
            public string PlayerName { get; set; }
            [DataMember]
            public string AuthCode { get; set; }
        }
    }
}
