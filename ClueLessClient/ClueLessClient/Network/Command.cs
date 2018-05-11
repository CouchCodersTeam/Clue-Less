using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Network
{
    public enum CommandType
    {
        TakeTurn,    // instructs player to take their turn

        // informative commands
        MovePlayer,     // informs that other player moved
        SuggestionMade, // informs players of suggestion made and result

        DisproveSuggestion, // instructs player to disprove the suggestion

        AccusationMade,  // informs players of accusation and its result

        TurnEnd,  // Player has finished their turn

    }

    [DataContract]
    public class Command
    {
        public CommandType command { get; set; }
        public object data; // the command determines what this object is
    }
}
