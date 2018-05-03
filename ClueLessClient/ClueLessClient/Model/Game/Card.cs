using System;
using System.Collections.Generic;
using System.Linq;
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

    public class Card
    {
        private CardType cardType;
        private String cardValue;

        // Constructors
        // Usage:
        // Card card = new Card(Study);
        public Card(Room room)
        {
            cardType = CardType.Room;
            cardValue = room.ToString();
        }

        // Usage:
        // Card card = new Card(Mustard);
        public Card(Suspect suspect)
        {
            cardType = CardType.Suspect;
            cardValue = suspect.ToString();
        }

        // Usage:
        // Card card = new Card(Rope);
        public Card(Weapon weapon)
        {
            cardType = CardType.Weapon;
            cardValue = weapon.ToString();
        }


    }
    
}
