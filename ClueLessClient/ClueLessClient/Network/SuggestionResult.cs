using ClueLessClient.Model.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    [DataContract]
    public class SuggestionResult
    {
        // This is the card that 'playerName' chose to reveal
        // to the accusing player
        [DataMember]
        public Card card { get; set; }

        // This is the name of the player who made the
        // suggestion
        [DataMember]
        public string playerName { get; set; }
    }
}
