using System;
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
        public void Board1x1_CurrentState_Test()
        {
            var game = new Game();
            game.CreateBoard(1, 1);

            Assert.AreEqual(0, game.CurrentState(0, 0, -1, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 0, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, -1, 0));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, 0));
            Assert.AreEqual(0, game.CurrentState(0, 0, -1, 1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 0, 1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, 1));
            Assert.AreEqual(0, game.GetAliveNeighbors(0, 0));

            game.Board.Columns[0][0] = true;

            Assert.AreEqual(0, game.CurrentState(0, 0, -1, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 0, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, -1));
            Assert.AreEqual(0, game.CurrentState(0, 0, -1, 0));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, 0));
            Assert.AreEqual(0, game.CurrentState(0, 0, -1, 1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 0, 1));
            Assert.AreEqual(0, game.CurrentState(0, 0, 1, 1));
            Assert.AreEqual(0, game.GetAliveNeighbors(0, 0));
        }

        [TestMethod]
        public void Board3x3_CurrentState_Test()
        {
            var game = new Game();
            game.CreateBoard(3, 3);

            Assert.AreEqual(0, game.CurrentState(1, 1, -1, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 0, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, -1, 0));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, 0));
            Assert.AreEqual(0, game.CurrentState(1, 1, -1, 1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 0, 1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, 1));
            Assert.AreEqual(0, game.GetAliveNeighbors(0, 0));

            game.Board.Columns[1][1] = true;

            Assert.AreEqual(0, game.CurrentState(1, 1, -1, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 0, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, -1));
            Assert.AreEqual(0, game.CurrentState(1, 1, -1, 0));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, 0));
            Assert.AreEqual(0, game.CurrentState(1, 1, -1, 1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 0, 1));
            Assert.AreEqual(0, game.CurrentState(1, 1, 1, 1));
            Assert.AreEqual(0, game.GetAliveNeighbors(1, 1));

            game.Board.Columns[0][0] = true;
            game.Board.Columns[0][1] = true;
            game.Board.Columns[0][2] = true;
            game.Board.Columns[1][0] = true;
            game.Board.Columns[1][2] = true;
            game.Board.Columns[2][0] = true;
            game.Board.Columns[2][1] = true;
            game.Board.Columns[2][2] = true;

            Assert.AreEqual(1, game.CurrentState(1, 1, -1, -1));
            Assert.AreEqual(1, game.CurrentState(1, 1, 0, -1));
            Assert.AreEqual(1, game.CurrentState(1, 1, 1, -1));
            Assert.AreEqual(1, game.CurrentState(1, 1, -1, 0));
            Assert.AreEqual(1, game.CurrentState(1, 1, 1, 0));
            Assert.AreEqual(1, game.CurrentState(1, 1, -1, 1));
            Assert.AreEqual(1, game.CurrentState(1, 1, 0, 1));
            Assert.AreEqual(1, game.CurrentState(1, 1, 1, 1));
            Assert.AreEqual(8, game.GetAliveNeighbors(1, 1));
        }

        [TestMethod]
        public void Board1x1_NextBoard_Test()
        {
            var game = new Game();
            game.CreateBoard(1, 1);
            var nextBoard = game.NextBoard();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = false;

            BoardTests.BoardsEqual(expectedBoard, nextBoard);

            game.Board.Columns[0][0] = true;
            nextBoard = game.NextBoard();

            expectedBoard.Columns[0][0] = false;
            BoardTests.BoardsEqual(expectedBoard, nextBoard);
        }

        [TestMethod]
        public void Board3x3_Empty_NextBoard_Test()
        {
            var game = new Game();
            game.CreateBoard(3, 3);
            var nextBoard = game.NextBoard();

            Assert.AreEqual(false, nextBoard.Columns[0][0]);
            Assert.AreEqual(false, nextBoard.Columns[1][0]);
            Assert.AreEqual(false, nextBoard.Columns[2][0]);
            Assert.AreEqual(false, nextBoard.Columns[0][1]);
            Assert.AreEqual(false, nextBoard.Columns[1][1]);
            Assert.AreEqual(false, nextBoard.Columns[2][1]);
            Assert.AreEqual(false, nextBoard.Columns[0][2]);
            Assert.AreEqual(false, nextBoard.Columns[1][2]);
            Assert.AreEqual(false, nextBoard.Columns[2][2]);
        }

        [TestMethod]
        public void Board3x3_3NeighborsBorn_NextBoard_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(3, 3);

            board.Columns[0][1] = true;
            board.Columns[1][1] = true;
            board.Columns[2][1] = true;

            var nextBoard = game.NextBoard();

            Assert.AreEqual(false, nextBoard.Columns[0][0]);
            Assert.AreEqual(true, nextBoard.Columns[1][0]);
            Assert.AreEqual(false, nextBoard.Columns[2][0]);
            Assert.AreEqual(false, nextBoard.Columns[0][1]);
            Assert.AreEqual(true, nextBoard.Columns[1][1]);
            Assert.AreEqual(false, nextBoard.Columns[2][1]);
            Assert.AreEqual(false, nextBoard.Columns[0][2]);
            Assert.AreEqual(true, nextBoard.Columns[1][2]);
            Assert.AreEqual(false, nextBoard.Columns[2][2]);
        }

        [TestMethod]
        public void Board3x3_Full_NextBoard_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(3, 3);

            board.Columns[0][0] = true;
            board.Columns[1][0] = true;
            board.Columns[2][0] = true;
            board.Columns[0][1] = true;
            board.Columns[1][1] = true;
            board.Columns[2][1] = true;
            board.Columns[0][2] = true;
            board.Columns[1][2] = true;
            board.Columns[2][2] = true;

            var nextBoard = game.NextBoard();

            Assert.AreEqual(true, nextBoard.Columns[0][0]);
            Assert.AreEqual(false, nextBoard.Columns[1][0]);
            Assert.AreEqual(true, nextBoard.Columns[2][0]);
            Assert.AreEqual(false, nextBoard.Columns[0][1]);
            Assert.AreEqual(false, nextBoard.Columns[1][1]);
            Assert.AreEqual(false, nextBoard.Columns[2][1]);
            Assert.AreEqual(true, nextBoard.Columns[0][2]);
            Assert.AreEqual(false, nextBoard.Columns[1][2]);
            Assert.AreEqual(true, nextBoard.Columns[2][2]);
        }

        [TestMethod]
        public void Steps_Test()
        {
            var game = new Game();
            Assert.AreEqual(0, game.Steps);

            game.CreateBoard(1, 1);
            Assert.AreEqual(0, game.Steps);
            game.NextBoard();
            Assert.AreEqual(0, game.Steps);
            game.Step();
            Assert.AreEqual(1, game.Steps);
            game.Step();
            Assert.AreEqual(2, game.Steps);
        }

        [TestMethod]
        public void Reset_Test()
        {
            var game = new Game();
            game.CreateBoard(1, 1);
            game.Board.Columns[0][0] = true;
            game.Step();

            game.Reset();

            Assert.AreEqual(0, game.Steps);
            BoardTests.BoardsEqual(new Board(1, 1), game.Board);
        }

        [TestMethod]
        public void ImportComponent_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(3, 3);

            board.Columns[0][0] = true;
            board.Columns[1][1] = true;
            board.Columns[2][2] = true;

            var component = new Board(3, 3);
            component.Columns[0][0] = true;
            component.Columns[0][1] = true;
            component.Columns[0][2] = true;

            Assert.ThrowsException<ArgumentNullException>(() =>
                game.ImportComponent(0, 0, null));

            var actual = game.ImportComponent(1, 2, component);

            Assert.AreEqual(game.Board, actual);

            var expected = new Board(3, 3);
            expected.Columns[0][0] = true;
            expected.Columns[1][1] = true;
            expected.Columns[1][2] = true;
            expected.Columns[2][2] = true;
            BoardTests.BoardsEqual(expected, game.Board);
        }
    }
}
