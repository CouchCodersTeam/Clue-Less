using System;
using ClueLessClient.Model.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CluelessClient.Tests
{
    [TestClass]
    public class TestBoard
    {
        private class Hallway : Location
        {
            public Hallway(int x, int y)
                : base(x, y, "Hallway")
            {

            }
        }

        [TestMethod]
        public void TestDirectiona1Moves()
        {
            Board board = new Board();
            Location loc = board.GetLocation(0, 0);

            Assert.AreEqual(new Location(0, 0, "Study"), loc);
            Assert.IsNull(board.UpFrom(loc));
            Assert.IsNull(board.LeftFrom(loc));

            Assert.AreEqual(new Hallway(1,0), board.RightFrom(loc));
            Assert.AreEqual(new Hallway(0,1), board.DownFrom(loc));

            Assert.ReferenceEquals(board.GetLocation(1, 0), board.RightFrom(loc));
            Assert.ReferenceEquals(board.GetLocation(0, 1), board.DownFrom(loc));



            loc = board.GetLocation(2, 3);

            Assert.AreEqual(new Hallway(2, 3), loc);

//            Assert.AreEqual(new Location(2, 2, "Billiard"), board.UpFrom(loc));
//            Assert.AreEqual(new Location(2, 4, "Dining"), board.DownFrom(loc));
//            Assert.AreEqual(new Location(1, 3, "Billiard"), board.LeftFrom(loc));
//            Assert.AreEqual(new Location(3, 3, "Billiard"), board.RightFrom(loc));


//            Assert.ReferenceEquals(board.GetLocation(1, 0), board.RightFrom(loc));
//            Assert.ReferenceEquals(board.GetLocation(0, 1), board.DownFrom(loc));


            loc = board.GetLocation(4, 4);

            Assert.AreEqual(new Location(4, 4, "Kitchen"), loc);
            Assert.IsNull(board.DownFrom(loc));
            Assert.IsNull(board.RightFrom(loc));

            Assert.AreEqual(new Hallway(4, 3), board.UpFrom(loc));
            Assert.AreEqual(new Hallway(3, 4), board.LeftFrom(loc));

            Assert.ReferenceEquals(board.GetLocation(3, 4), board.LeftFrom(loc));
            Assert.ReferenceEquals(board.GetLocation(4, 3), board.UpFrom(loc));
        }
    }
}
