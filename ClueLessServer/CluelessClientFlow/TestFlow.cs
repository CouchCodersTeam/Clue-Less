using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClueLessClient.Network;

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
                var lobby = connect.Lobbies.CreateLobby();
                lobbies = connect.Lobbies.GetLobbies();

                // Harry is host, he is already in game
                Assert.IsFalse(connect.Lobbies.JoinLobby(lobby));
            }

            // Other player actions
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
            }

            // Have a third person join
            Assert.IsTrue(connect.registerAsPlayer("Hermione"));
            {
                // Success
                var lobbies = connect.Lobbies.GetLobbies();
                var lobby = lobbies[lobbies.Count - 1];

                Assert.IsTrue(connect.Lobbies.JoinLobby(lobby));

                connect.registerToGame(lobby);

                //if (!connect.Lobbies.WaitForGameStart())
                //    throw new Exception("Unsuccessful Wait for game start");
            }

            // Host can start game
            Assert.IsTrue(connect.registerAsPlayer("Harry Potter"));
            {
                Assert.IsTrue(connect.Lobbies.StartGame());
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
        }
    }
}
