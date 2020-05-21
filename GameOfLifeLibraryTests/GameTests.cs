using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void CreateBoard_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(2, 3);

            Assert.AreEqual(2, board.Width);
            Assert.AreEqual(3, board.Height);
            Assert.AreEqual(2, board.Columns.Length);
            Assert.AreEqual(3, board.Columns[0].Length);
            Assert.AreEqual(3, board.Columns[1].Length);
        }

        [TestMethod]
        public void NextBoard_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(2, 3);
            var nextBoard = game.NextBoard();

            BoardTests.BoardsEqual(board, nextBoard);
        }

        [TestMethod]
        public void Board1x1_LeftTop_Test()
        {
            var game = new Game();
            game.CreateBoard(1, 1);

            var leftTopState = game.LeftTopState(0, 0);
            Assert.AreEqual(0, leftTopState);
        }
    }
}
