using ClueLessClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ClueLessClient.Model.Game
{
    [DataContract]
    public class Game
    {
        // DataContract and DataMember serialize the variables to be
        // sent over the network. Any added variables should be serialized.
        [DataMember]
        private List<Player> players;  // Ryan changed from set to list
        [DataMember]
        private Board board;
        [DataMember]
        private Card[] caseFile;       // Casefile may be its own class, will leave as array for now
        [DataMember]
        private int currentTurnIndex;  // The index in rotationOrders that starts with 0
        [DataMember]
        public List<RealPlayer> rotationOrders;    // Also used for suggestions
        [DataMember]
        public bool ended = false;

        private static readonly int MIN_PLAYERS = 3;
        private static readonly int MAX_PLAYERS = 6;
        
        public Game()
        {
            players = new List<Player>();
            rotationOrders = new List<RealPlayer>();
            currentTurnIndex = 0;
            // initialize these variables in 'startGame'
            board = null;
            caseFile = null;
        }

        // add a player to the game, the game has not started when
        // this function is called.
        public bool addPlayer(string playerName)
        {
            Player player = new RealPlayer(playerName);
            if (players.Count == 6)
            {
                return false;
            }
            else if(!players.Contains(player))
            {
                if (player is RealPlayer) {
                    rotationOrders.Add((RealPlayer) player);
                }
                players.Add(player);
                return true;
            }

            return false;
        }

        public bool removePlayer(string player)
        {
            if (players.Count == 0)
            {
                return false;
            }
            else
            {
                players.Remove(new RealPlayer(player));
                return true;
            }
        }

        public bool containsPlayer(string player)
        {
            return players.Contains(new RealPlayer(player));
        }

        public bool canStartGame()
        {
            return MIN_PLAYERS <= players.Count && players.Count <= MAX_PLAYERS;
        }

        public void startGame()
        {
            board = new Board();
            caseFile = new Card[3];

            // Random selection of card from 3 types for case File
            Array rooms = Enum.GetValues(typeof(Room));
            Room room = (Room) rooms.GetValue(new Random().Next(rooms.Length));
            caseFile[0] = new Card(room);

            Array suspects = Enum.GetValues(typeof(Suspect));
            Suspect suspect = (Suspect) suspects.GetValue(new Random().Next(suspects.Length));
            caseFile[1] = new Card(suspect);

            Array weapons = Enum.GetValues(typeof(Weapon));
            Weapon weapon = (Weapon) weapons.GetValue(new Random().Next(weapons.Length));
            caseFile[2] = new Card(weapon);

            // Distribute cards upon starting the game?
            Card[] deck = Deck.newShuffledDeck();
            // remove caseFile values
            deck = Array.FindAll(deck, card => !inCaseFile(card));

            Card[][] hands = Deck.shuffleDeck(deck, rotationOrders.Count);
            for (int i = 0; i < rotationOrders.Count; i++)
            {
                rotationOrders[i].cards = hands[i];
            }

            if (players[0] != null) 
            {
                board.MovePlayer(players[0], new Location(0,3,"Hallway")); 
                players[0].character = Suspect.Scarlet;
            }
            if (players[1] != null)
            {
                board.MovePlayer(players[1], new Location(1,4,"Hallway")); 
                players[1].character = Suspect.Mustard;
            }
            if (players[2] != null)
            {
                board.MovePlayer(players[2], new Location(4,3,"Hallway")); 
                players[2].character = Suspect.White;
            }
            if (players[3] != null)
            {
                board.MovePlayer(players[3], new Location(4,1,"Hallway")); 
                players[3].character = Suspect.Green;
            }
            if (players[4] != null)
            {
                board.MovePlayer(players[4], new Location(3,0,"Hallway")); 
                players[4].character = Suspect.Peacock;
            }
            if (players[5] != null)
            {
                board.MovePlayer(players[5], new Location(1,0,"Hallway")); 
                players[5].character = Suspect.Plum;
            }
           

        }

        private bool inCaseFile(Card card)
        {
            return Array.Find(caseFile, c => c.Equals(card)) != null;
        }

        // returns the player whose turn it is
        public Player getPlayerTurn()
        {
            return rotationOrders[currentTurnIndex];
        }

        // Move turn pointer to next
        public void nextPlayer() {
            currentTurnIndex = (currentTurnIndex + 1) % rotationOrders.Count;
        }

        // returns the cards for the given player
        public Card[] getPlayerHand(string playerName)
        {
            RealPlayer player = rotationOrders.Find(x => x.name.Equals(playerName));
            if (player != null) {
                return player.cards;
            } else {
                return null;
            }
        }

        public List<Player> getPlayers()
        {
            return players;
        }
        // TODO: getAvailableCharacters(), chooseCharacter()
        // Ryan doesn't care too much for this feature. Characters
        // could be assigned rather than chosen to make the setup
        // less complex. He'll do the work for it if desired.

        public bool movePlayer(string playerName, Location location)
        {
            Player player = players.Find(x => x.name.Equals(playerName));
            if (!player.location.isNextTo(location)) { return false; }

            // Can't move to occupied hallways
            if (location.locationName == "Hallway" && location.occupants.Count != 0) {
                return false;
            } else {
                player.location = location;
                return true;
            }
        }

        public Board getBoard()
        {
            return board;
        }
        // returns the first player after 'player' who can disprove the
        // accusation. If no other player can disprove, null is returned.
        public Player makeSuggestion(string playerName, Accusation accusation)
        {
            foreach (Player player in players)
            {
                if (player is RealPlayer)
                {
                    var realPlayer = (RealPlayer)player;
                    if (realPlayer.hasCardIn(accusation) && !realPlayer.name.Equals(playerName))
                    {
                        return player; // Return first player with a card in the suggestion to disprove
                    }

                }
            }

            return null;
        }

        // Returns true/false if accusation is correct. If false, 'player'
        // has lost the game and cannot make further accusations.
        public bool makeAccusation(string playerName, Accusation accusation)
        {
            // Checking if all cards in accusation are matching case files
            return accusation.room == (Room) Enum.Parse(typeof(Room), caseFile[0].cardValue) &&
                accusation.suspect == (Suspect) Enum.Parse(typeof(Suspect), caseFile[1].cardValue) &&
                accusation.weapon == (Weapon) Enum.Parse(typeof(Weapon), caseFile[2].cardValue);
        }

        // game is notified that game is over
        public void endGame()
        {
            ended = true;
        }

        // This function cannot be called until endGame has been called.
        // null is returned if game is still going on. Otherwise, the
        // caseFile is returned
        public Accusation getSolution()
        {
            if (!ended) {
                return null;
            } else {
                Room room = (Room) Enum.Parse(typeof(Room), caseFile[0].cardValue);
                Suspect suspect = (Suspect) Enum.Parse(typeof(Suspect), caseFile[1].cardValue);
                Weapon weapon = (Weapon) Enum.Parse(typeof(Weapon), caseFile[2].cardValue);
                return new Accusation(room, suspect, weapon);
            }
        }

    }
}
