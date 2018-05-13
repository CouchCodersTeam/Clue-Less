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
    [KnownType(typeof(MoveData))]
    public class Command
    {
        [DataMember]
        public CommandType command { get; set; }

        [DataMember]
        public CommandData data { get; set; } // the command determines what data is stored inside

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Command other = (Command)obj;
            if (command == other.command)
            {
                if (data == null)
                    return other.data == null;
                else
                    return data.Equals(other.data); // this isn't working
            }
            return false;
        }
    }

    [DataContract]
    public class CommandData
    {
        // this is a collection of all possible command data
        [DataMember]
        public MoveData moveData { get; set; }
        [DataMember]
        public SuggestionData suggestData { get; set; }
        [DataMember]
        public DisproveData disproveData { get; set; }
        [DataMember]
        public AccusationData accusationData { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            CommandData other = (CommandData)obj;

            if (moveData == null && other.moveData != null)
                return false;
            else if (moveData != null)
                return moveData.Equals(other.moveData);
            else if (suggestData == null && other.suggestData != null)
                return false;
            else if (suggestData != null)
                return suggestData.Equals(other.suggestData);
            else if (disproveData == null && other.disproveData != null)
                return false;
            else if (disproveData != null)
                return disproveData.Equals(other.disproveData);
            else if (accusationData == null && other.accusationData != null)
                return false;
            else if (accusationData != null)
                return accusationData.Equals(other.accusationData);

            return false;
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            MoveData other = (MoveData)obj;
            return playerName.Equals(other.playerName)
                && location.Equals(other.location);
        }
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            SuggestionData other = (SuggestionData)obj;
            return playerName.Equals(other.playerName)
                && accusation.Equals(other.accusation)
                && disprovingPlayer.Equals(other.disprovingPlayer);
        }
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (DisproveData)obj;
            return card.Equals(other.card)
                && disprovingPlayer.Equals(other.disprovingPlayer);
        }

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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var other = (AccusationData)obj;
            return playerName.Equals(other.playerName)
                && accusation.Equals(other.accusation)
                && accusationCorrect.Equals(other.accusationCorrect);
        }

    }
}
