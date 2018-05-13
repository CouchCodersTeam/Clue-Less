using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    [DataContract]
    [KnownType(typeof(RealPlayer))]
    [KnownType(typeof(DummyPlayer))]
    public class Player
    {
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public Coordinate location { get; set; }
        [DataMember]
        public Suspect character { get; set; }

        public Player(string playerName)
        {
            name = playerName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Player other = (Player)obj;
            return this.name.Equals(other.name);
        }

    }

    [DataContract]
    public class RealPlayer : Player
    {
        [DataMember]
        public Card[] cards { set; get; }

        public RealPlayer(string playerName)
         : base(playerName)
        {
        }

        public bool hasCardIn(Accusation accusation)
        {
            return cards.Contains(new Card(accusation.suspect))
                || cards.Contains(new Card(accusation.weapon))
                || cards.Contains(new Card(accusation.room));
        }

        public bool Move (Location location)
        {
            // TODO: check if move is valid
            return false;
        }
    }

    [DataContract]
    public class DummyPlayer : Player
    {
        public DummyPlayer(string playerName)
         : base(playerName)
        {
        }
    }

}
