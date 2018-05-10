using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueLessClient.Model.Game
{
    // TODO: Temporary hack to get code to build. Remove this
    // line once Location is added to the project

    public class Game
    {
        private List<Player> players;  // Ryan changed from set to list
        private Board board;
        private Card[] caseFile;       // Casefile may be its own class, will leave as array for now
        private int currentTurnIndex;  // The index in rotationOrders that starts with 0
        public List<RealPlayer> rotationOrders;    // Also used for suggestions
        public bool ended = false;

        private static readonly int MIN_PLAYERS = 3;
        private static readonly int MAX_PLAYERS = 6;

        public Game()
        {
            players = new List<Player>();
            currentTurnIndex = 0;
            // initialize these variables in 'startGame'
            board = null;
            caseFile = null;
        }

        // add a player to the game, the game has not started when
        // this function is called.
        public bool addPlayer(Player player)
        {
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

        public bool removePlayer(Player player)
        {
            if (players.Count == 0)
            {
                return false;
            }
            else
            {
                players.Remove(player);
                return true;
            }
        }

        public bool containsPlayer(Player player)
        {
            return players.Contains(player);
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
        public Card[] getPlayerHand(Player player)
        {
            if (player is RealPlayer) {
                return player.cards;
            } else {
                return null;
            }
        }

        // TODO: getAvailableCharacters(), chooseCharacter()
        // Ryan doesn't care too much for this feature. Characters
        // could be assigned rather than chosen to make the setup
        // less complex. He'll do the work for it if desired.

        // TODO: serialize() function. This is for the /state API
        // to have the client assure that its Game object is in
        // sync with the server's game object. This function definition
        // probably doesn't belong in 'Game'


        public bool movePlayer(Player player, Location location)
        {
            if (!player.location.isNextTo(location)) { return false; }

            // Can't move to occupied hallways
            if (location.locationName == "Hallway" && location.occupants.Count != 0) {
                return false;
            } else {
                player.location = location;
                return true;
            }
        }

        // returns the first player after 'player' who can disprove the
        // accusation. If no other player can disprove, null is returned.
        public Player makeSuggestion(Player player, Accusation accusation)
        {
            return null;
        }

        // Returns true/false if accusation is correct. If false, 'player'
        // has lost the game and cannot make further accusations.
        public bool makeAccusation(Player player, Accusation accusation)
        {
            // Checking if all cards in accusation are matching case files
            return accusation.room == (Room) Enum.parse(typeof(Room), caseFile[0].cardValue) &&
                accusation.suspect == (Suspect) Enum.parse(typeof(Suspect), caseFile[1].cardValue) &&
                accusation.weapon == (Weapon) Enum.parse(typeof(Weapon), caseFile[2].cardValue);
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
                Room room = (Room) Enum.parse(typeof(Room), caseFile[0].cardValue);
                Suspect suspect = (Suspect) Enum.parse(typeof(Suspect), caseFile[1].cardValue);
                Weapon weapon = (Weapon) Enum.parse(typeof(Weapon), caseFile[2].cardValue);
                return new Accusation(room, suspect, weapon);
            }
        }

    }
}
