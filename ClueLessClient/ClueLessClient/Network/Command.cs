using ClueLessClient.Model.Game;
using System.Runtime.Serialization;

namespace ClueLessClient.Network
{
    public enum CommandType
    {
        GameStart,   // instructs player that game has started
        Wait,        // instructs client to wait and ask again
        TakeTurn,    // instructs player to take their turn

        // informative commands
        MovePlayer,     // informs that other player moved
        SuggestionMade, // informs players of suggestion made, data is 'SuggestionData' object

        DisproveSuggestion, // instructs player to disprove the suggestion, data is 'SuggestionData' object
        DisproveResult,    // data is 'DisproveData' object

        AccusationMade,  // informs players of accusation, data is 'AccusationData' object

        TurnEnd,  // Player has finished their turn

    }

    [DataContract]
    public class Command
    {
        [DataMember]
        public CommandType command { get; set; }

        [DataMember]
        public object data { get; set; } // the command determines what this object is

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Command other = (Command)obj;
            return command == other.command && data.Equals(other.data);
        }
    }

    // Data associated with the 'MovePlayer' command
    [DataContract]
    public class MoveData
    {
        [DataMember]
        public string playerName { get; set; }
        [DataMember]
        public Location location { get; set; }
    }

    public class SuggestionData
    {
        [DataMember]
        public string playerName { get; set; }
        [DataMember]
        public Accusation accusation { get; set; }

        // this is the name of the player who can disprove 'playerName's
        // accusation, returns null if no one can disprove
        [DataMember]
        public string disprovingPlayer { get; set; }
    }

    [DataContract]
    public class DisproveData
    {
        // This is the card that 'playerName' chose to reveal
        // to the accusing player
        [DataMember]
        public Card card { get; set; }
        [DataMember]
        public string disprovingPlayer { get; set; }
    }

    [DataContract]
    public class AccusationData
    {
        // This is the card that 'playerName' chose to reveal
        // to the accusing player
        [DataMember]
        public Accusation accusation { get; set; }
        [DataMember]
        public string playerName { get; set; }
        [DataMember]
        public bool accusationCorrect { get; set; }
    }
}
