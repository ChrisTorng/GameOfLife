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
            Assert.AreEqual(3, board.Rows.Length);
            Assert.AreEqual(2, board.Rows[0].Length);
            Assert.AreEqual(2, board.Rows[1].Length);
            Assert.AreEqual(2, board.Rows[2].Length);
        }
    }
}
