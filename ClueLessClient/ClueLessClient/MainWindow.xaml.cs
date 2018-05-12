using ClueLessClient.Model.Game;
using ClueLessClient.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClueLessClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            CluelessServerConnection connect = CluelessServerConnection.getConnection(
    "localhost", 50351);

            // Host actions
            if (connect.registerAsPlayer("Harry Potter"))
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = connect.Lobbies.CreateLobby();
                lobbies = connect.Lobbies.GetLobbies();
            }

            // Other player actions
            if (connect.registerAsPlayer("Ron Weasley"))
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = lobbies[lobbies.Count - 1];

                // Ron is not host, he can join
                connect.Lobbies.JoinLobby(lobby);

                connect.registerToGame(lobby);

                // Ron cannot start game because he is not the host
                connect.Lobbies.StartGame();
            }

            // Have a third person join
            if (connect.registerAsPlayer("Hermione"))
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = lobbies[lobbies.Count - 1];

                connect.Lobbies.JoinLobby(lobby);

                connect.registerToGame(lobby);

                //if (!connect.Lobbies.WaitForGameStart())
                //    throw new Exception("Unsuccessful Wait for game start");
            }

            // Host can start game
            if (connect.registerAsPlayer("Harry Potter"))
            {
                connect.Lobbies.StartGame();
            }

            // other players 'WaitForGameStart()' will now return true
            if (connect.registerAsPlayer("Ron Weasley"))
            {
                bool x = connect.Lobbies.WaitForGameStart();
            }

            connect.registerAsPlayer("Hermione");
            {
                bool x = connect.Lobbies.WaitForGameStart();
            }

            // Play the game now

            // Get cards
            String[] players = { "Harry Potter", "Ron Weasley", "Hermione" };
            List<Card> previousPlayersCards = null;
            foreach (var playerName in players)
            {
                // verify that each player has a hand of cards
                connect.registerAsPlayer(playerName);
                var cards = connect.Gameplay.GetPlayerHand();

                // TODO: this is not implemented in Game.cs yet
                //cards);
                //Assert.AreNotEqual(0, cards.Count);

                //// Make sure each hand is unique
                //Assert.AreNotEqual(previousPlayersCards, cards);
                //previousPlayersCards = cards;
            }

            // take turn
            if (connect.registerAsPlayer("Harry Potter"))
            {
                // Get game state to verify correct information
                Game game = connect.Gameplay.GetState();

                // API not complete
                Command command = connect.Gameplay.WaitForCommand().Result;
                // It is Harry's turn, this returns a 'Take turn' command
//                Assert.AreEqual(CommandType.TakeTurn, command.command);

                bool successful = connect.Gameplay.MovePlayerTo(new Location(0, 0, "Boardroom"));

                DisproveData result = connect.Gameplay.MakeSuggestion(new Accusation(Room.Ballroom, Suspect.Mustard, Weapon.Pipe));
                // if null, no one could disprove
                // Otherwise, result.card is the proof, and result.playerName is the owner of 'card'

                // This is called by the 'other' player, not by Harry
                successful = connect.Gameplay.DisproveSuggestion(new Card(Weapon.Pipe));

                AccusationData data = connect.Gameplay.MakeAccusation(new Accusation(Room.Ballroom, Suspect.Mustard, Weapon.Pipe));

                successful = connect.Gameplay.EndTurn();

                Accusation solution = connect.Gameplay.GetSolution();
            }
        }
    }
}
