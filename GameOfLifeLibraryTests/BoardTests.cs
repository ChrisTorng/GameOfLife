using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void Constructor_Test()
        {
            var board = new Board(2, 3);

            Assert.AreEqual(2, board.Width);
            Assert.AreEqual(3, board.Height);
            Assert.AreEqual(2, board.Columns.Length);
            Assert.AreEqual(3, board.Columns[0].Length);
            Assert.AreEqual(3, board.Columns[1].Length);
        }

        [TestMethod]
        public void Flip_Test()
        {
            var board = new Board(2, 3);

            Assert.AreEqual(true, board.Flip(0, 0));
            Assert.AreEqual(false, board.Flip(0, 0));
            Assert.AreEqual(true, board.Flip(1, 2));
            Assert.AreEqual(false, board.Flip(1, 2));
        }
    }
}
