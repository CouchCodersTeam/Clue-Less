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
        public RealPlayer currentTurn;

        public Game()
        {
            players = new List<Player>();

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
            else
            {
                players.Add(player);
                return true;
            }

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

        public void startGame()
        {
            board = new Board();
        }

        // returns the player whose turn it is
        public Player getPlayerTurn()
        {
            return null;
        }

        // returns the cards for the given player
        public Card[] getPlayerHand(Player player)
        {
            return null;
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
            return false;
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
            return false;
        }

        // game is notified that game is over
        public bool endGame()
        {
            return false;
        }

        // This function cannot be called until endGame has been called.
        // null is returned if game is still going on. Otherwise, the
        // caseFile is returned
        public Accusation getSolution()
        {
            return null;
        }

    }
}
