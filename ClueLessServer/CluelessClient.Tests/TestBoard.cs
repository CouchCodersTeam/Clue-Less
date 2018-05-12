using System;
using ClueLessClient.Model.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CluelessClient.Tests
{
    [TestClass]
    public class TestBoard
    {
        [TestMethod]
        public void TestDirectiona1Moves()
        {
            Board board = new Board();
            Location loc = board.GetLocation(0, 0);

            Assert.AreEqual(new Location(0, 0, "Study"), loc);
            Assert.IsNull(board.UpFrom(loc));
            Assert.IsNull(board.LeftFrom(loc));

            Assert.AreEqual(new Location(1,0, "Hallway"), board.RightFrom(loc));
            Assert.AreEqual(new Location(0,1, "Hallway"), board.DownFrom(loc));

            Assert.ReferenceEquals(board.GetLocation(1, 0), board.RightFrom(loc));
            Assert.ReferenceEquals(board.GetLocation(0, 1), board.DownFrom(loc));



            loc = board.GetLocation(2, 3);

            // Every hall way has two connected rooms, and 2 null directions
            Assert.AreEqual(new Location(2, 3, "Hallway"), loc);

            Assert.AreEqual(new Location(2, 2, "Billiard"), board.UpFrom(loc));
            Assert.AreEqual(new Location(2, 4, "Ballroom"), board.DownFrom(loc));
            Assert.IsNull(board.LeftFrom(loc));
            Assert.IsNull(board.RightFrom(loc));


            Assert.ReferenceEquals(board.GetLocation(2, 2), board.UpFrom(loc));
            Assert.ReferenceEquals(board.GetLocation(2, 4), board.DownFrom(loc));


            loc = board.GetLocation(4, 4);

            Assert.AreEqual(new Location(4, 4, "Kitchen"), loc);
            Assert.IsNull(board.DownFrom(loc));
            Assert.IsNull(board.RightFrom(loc));

            Assert.AreEqual(new Location(4, 3, "Hallway"), board.UpFrom(loc));
            Assert.AreEqual(new Location(3, 4, "Hallway"), board.LeftFrom(loc));

            Assert.ReferenceEquals(board.GetLocation(3, 4), board.LeftFrom(loc));
            Assert.ReferenceEquals(board.GetLocation(4, 3), board.UpFrom(loc));
        }
    }
}
