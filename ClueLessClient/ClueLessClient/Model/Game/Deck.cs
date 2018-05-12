using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    public class Deck
    {
        private readonly static Card[] allCards =
        {
            new Card(Room.Study),
            new Card(Room.Kitchen),
            new Card(Room.Ballroom),
            new Card(Room.Conservatory),
            new Card(Room.Billiard),
            new Card(Room.Library),
            new Card(Room.Hall),
            new Card(Room.Lounge),
            new Card(Room.Dining),

            new Card(Suspect.Scarlet),
            new Card(Suspect.Mustard),
            new Card(Suspect.White),
            new Card(Suspect.Green),
            new Card(Suspect.Peacock),
            new Card(Suspect.Plum),

            new Card(Weapon.Candlestick),
            new Card(Weapon.Revolver),
            new Card(Weapon.Knife),
            new Card(Weapon.Pipe),
            new Card(Weapon.Rope),
            new Card(Weapon.Wrench)
        };

        public static Card[] newShuffledDeck()
        {
            Random random = new Random();
            return allCards.OrderBy(x => random.Next()).ToArray();
        }

        public static Card[][] newShuffledDeck(int numHands)
        {
            return shuffleDeck(newShuffledDeck(), numHands);
        }
        
        public static Card[][] shuffleDeck(Card[] deck, int numHands)
        {
            List<Card> newDeck = new List<Card>(deck);

            // split into as even sized groups as possible
            int minHandSize = newDeck.Count / numHands;
            int extraCards = newDeck.Count % numHands;

            List<Card>[] newHands = new List<Card>[numHands];
            for (int i = 0; i < numHands; i++)
            {
                List<Card> hand = newDeck.GetRange(i * minHandSize, minHandSize);
                newHands[i] = hand;
            }

            // Spread remaining cards to players (not everyone will get one)
            int cardIndex = minHandSize * numHands;
            for (int i = 0; i < extraCards; i++)
            {
                newHands[i].Add(newDeck[cardIndex++]);
            }

            // Convert to Card[][]
            Card[][] result = new Card[numHands][];
            for (int i = 0; i < newHands.Length; i++)
            {
                result[i] = newHands[i].ToArray();
            }

            return result;
        }
    };

}
