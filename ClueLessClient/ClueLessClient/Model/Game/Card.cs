using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public enum CardType
    {
        Room,
        Suspect,
        Weapon
    }

    public enum Room
    {
        Study,
        Kitchen,
        Ballroom,
        Conservatory,
        Billiard,
        Library,
        Hall,
        Lounge,
        Dining
    }

    public enum Suspect
    {
        Scarlet,
        Mustard,
        White,
        Green,
        Peacock,
        Plum
    }

    public enum Weapon
    {
        Candlestick,
        Revolver,
        Knife,
        Pipe,
        Rope,
        Wrench
    }

    [DataContract]
    public class Card
    {
        // Instance variables
        [DataMember]
        public CardType cardType { get; set; }
        [DataMember]
        public String cardValue { get; set; }

        // Constructors
        // Usage:
        // Card card = new Card(Room.Study);
        public Card(Room room)
        {
            cardType = CardType.Room;
            cardValue = room.ToString();
        }

        // Usage:
        // Card card = new Card(Suspect.Mustard);
        public Card(Suspect suspect)
        {
            cardType = CardType.Suspect;
            cardValue = suspect.ToString();
        }

        // Usage:
        // Card card = new Card(Weapon.Rope);
        public Card(Weapon weapon)
        {
            cardType = CardType.Weapon;
            cardValue = weapon.ToString();
        }

        // Usage:
        // Card card = new Card(...);
        // CardType type = card.GetCardType();
        public CardType GetCardType()
        {
            return cardType;
        }

        // Usage:
        // Card card = new Card(...);
        // String value = card.GetCardValue();
        public String GetCardValue()
        {
            return cardValue;
        }

        // Usage:
        // Weapon weapon = Card.GetWeaponFromString("Rope")
        public static Weapon GetWeaponFromString(string value)
        {
            if (value.Equals("Candlestick"))
                return Weapon.Candlestick;
            else if (value.Equals("Revolver"))
                return Weapon.Revolver;
            else if (value.Equals("Knife"))
                return Weapon.Knife;
            else if (value.Equals("Pipe"))
                return Weapon.Pipe;
            else if (value.Equals("Rope"))
                return Weapon.Rope;
            else if (value.Equals("Wrench"))
                return Weapon.Wrench;
            else
            {
                throw new ArgumentException("Unknown Weapon: " + value);
            }

        }

        // Usage:
        // Suspect suspect = Card.GetSuspectFromString("Peacock")
        public static Suspect GetSuspectFromString(string value)
        {
            if (value.Equals("Scarlet"))
                return Suspect.Scarlet;
            else if (value.Equals("Mustard"))
                return Suspect.Mustard;
            else if (value.Equals("White"))
                return Suspect.White;
            else if (value.Equals("Green"))
                return Suspect.Green;
            else if (value.Equals("Peacock"))
                return Suspect.Peacock;
            else if (value.Equals("Plum"))
                return Suspect.Plum;
            else
            {
                throw new ArgumentException("Unknown Suspect: " + value);
            }

        }

        // Usage:
        // Room room = Card.GetRoomFromString("Conservatory")
        public static Room GetRoomFromString(string value)
        {
            if (value.Equals("Study"))
                return Room.Study;
            else if (value.Equals("Kitchen"))
                return Room.Kitchen;
            else if (value.Equals("Ballroom"))
                return Room.Ballroom;
            else if (value.Equals("Conservatory"))
                return Room.Conservatory;
            else if (value.Equals("Billiard"))
                return Room.Billiard;
            else if (value.Equals("Library"))
                return Room.Library;
            else if (value.Equals("Hall"))
                return Room.Hall;
            else if (value.Equals("Lounge"))
                return Room.Lounge;
            else if (value.Equals("Dining"))
                return Room.Dining;
            else
            {
                throw new ArgumentException("Unknown Room: " + value);
            }

        }

    }
    
}
