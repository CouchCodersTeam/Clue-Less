﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClueLessClient.Network;
using ClueLessClient.Model.Game;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CluelessClientFlow
{
    // The server needs to be running at the IP and port specified
    // in the test below
    [TestClass]
    public class TestClientServerCommunicationFlow
    {
        [TestMethod]
        public void TestCreateJoinAndStartGame()
        {
            CluelessServerConnection connect = CluelessServerConnection.getConnection(
                "localhost", 50351);

            // Host actions
            Assert.IsTrue(connect.registerAsPlayer("Harry Potter"));
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                Assert.IsNotNull(lobbies);
                var lobby = connect.Lobbies.CreateLobby();
                Assert.IsNotNull(lobby);
                lobbies = connect.Lobbies.GetLobbies();
                Assert.IsNotNull(lobbies);
                Assert.AreNotEqual(0, lobbies.Count);

                // Harry is host, he is already in game
                Assert.IsFalse(connect.Lobbies.JoinLobby(lobby));
            }

            // Other player actions
            Task<bool> ronWaitForGame = null;
            Assert.IsTrue(connect.registerAsPlayer("Ron Weasley"));
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = lobbies[lobbies.Count - 1];

                // Ron is not host, he can join
                Assert.IsTrue(connect.Lobbies.JoinLobby(lobby));

                connect.registerToGame(lobby);

                // registerToGame makes it so 'lobby' isn't needed
                // as a parameter for further API calls

                // This function is a blocking call if game has not started.
                // The client should call this to wait for the game to begin.
                // There currently isn't a way to cancel a wait. Ryan can
                // implement that later if desired
                //                if (!connect.Lobbies.WaitForGameStart())
                //                    throw new Exception("Unsuccessful Wait for game start");

                // Ron cannot start game because he is not the host
                Assert.IsFalse(connect.Lobbies.StartGame());

                // Async call is used for testing, WaitForGameStart() is a blocking call
                // that waits until host player starts the game
                ronWaitForGame = connect.Lobbies.WaitForGameStartAsync();
                Assert.IsFalse(ronWaitForGame.IsCompleted);
            }

            // Have a third person join
            Task<bool> hermioneWaitForGame = null; 
            Assert.IsTrue(connect.registerAsPlayer("Hermione"));
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = lobbies[lobbies.Count - 1];

                Assert.IsTrue(connect.Lobbies.JoinLobby(lobby));

                connect.registerToGame(lobby);

                hermioneWaitForGame = connect.Lobbies.WaitForGameStartAsync();
                Assert.IsFalse(hermioneWaitForGame.IsCompleted);
            }

            // Host can start game
            Assert.IsTrue(connect.registerAsPlayer("Harry Potter"));
            {
                Assert.IsTrue(connect.Lobbies.StartGame());

                // Allow call to be sent to Hermione and Ron
                Thread.Sleep(3100);
                Assert.IsTrue(ronWaitForGame.IsCompleted); // response has come back
                Assert.IsTrue(ronWaitForGame.Result); // game has started
                Assert.IsTrue(hermioneWaitForGame.IsCompleted);
                Assert.IsTrue(hermioneWaitForGame.Result);
            }

            // other players 'WaitForGameStart()' will now return true
            Assert.IsTrue(connect.registerAsPlayer("Ron Weasley"));
            {
                Assert.IsTrue(connect.Lobbies.WaitForGameStart());
            }

            Assert.IsTrue(connect.registerAsPlayer("Hermione"));
            {
                Assert.IsTrue(connect.Lobbies.WaitForGameStart());
            }

            // Play the game now

            // Get cards
            String[] players = { "Harry Potter", "Ron Weasley", "Hermione" };
            List<Card> previousPlayersCards = null;
            foreach (var playerName in players)
            {
                // verify that each player has a hand of cards
                Assert.IsTrue(connect.registerAsPlayer(playerName));

                if (!playerName.Equals("Harry Potter")) // Harry will get message 'Take Turn'
                {
                    // WaitForCommand should return immediately and not wait here
                    Command command = connect.Gameplay.WaitForCommand();
                    Assert.AreEqual(CommandType.GameStart, command.command);
                    // remove last seen command so that hermione won't block
                    connect.Gameplay.TestOnlySetLastSeenCommand(null);
                }

                var cards = connect.Gameplay.GetPlayerHand();

                Assert.IsNotNull(cards);
                Assert.AreNotEqual(0, cards.Count);

                //// Make sure each hand is unique
                Assert.AreNotEqual(previousPlayersCards, cards);
                previousPlayersCards = cards;
            }

            // take turn
            Assert.IsTrue(connect.registerAsPlayer("Harry Potter"));
            {
                // Get game state to verify correct information
                Game game = connect.Gameplay.GetState();
                Assert.IsNotNull(game);

                Command command = connect.Gameplay.WaitForCommand();
                Assert.AreEqual(CommandType.TakeTurn, command.command);

                bool successful = connect.Gameplay.MovePlayerTo(new Location(0, 0, "Boardroom"));
                // Assert.IsTrue(successful);

                DisproveData result = connect.Gameplay.MakeSuggestion(new Accusation(Room.Ballroom, Suspect.Mustard, Weapon.Pipe));
                // if null, no one could disprove
                // Otherwise, result.card is the proof, and result.playerName is the owner of 'card'

                // This is called by the 'other' player, not by Harry
                successful = connect.Gameplay.DisproveSuggestion(new Card(Weapon.Pipe));

                AccusationData data = connect.Gameplay.MakeAccusation(new Accusation(Room.Ballroom, Suspect.Mustard, Weapon.Pipe));

                successful = connect.Gameplay.EndTurn();
                Assert.IsTrue(successful);

                Accusation solution = connect.Gameplay.GetSolution();
                Assert.IsNull(solution); // game is not finished. Solution not available until then
            }
        }
    }
}
