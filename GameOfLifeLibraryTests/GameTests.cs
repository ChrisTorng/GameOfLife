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
            var board = game.CreateBoard(new AreaSize(2, 3));

            Assert.AreEqual(new AreaSize(2, 3), board.AreaSize);
            Assert.AreEqual(2, board.Columns.Length);
            Assert.AreEqual(3, board.Columns[0].Length);
            Assert.AreEqual(3, board.Columns[1].Length);
        }

        [TestMethod]
        public void NextBoard_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(new AreaSize(2, 3));
            var nextBoard = game.NextBoard();

            Assert.That.BoardsEqual(board, nextBoard);
        }

        [TestMethod]
        public void Board1x1_CurrentState_Test()
        {
            var game = new Game();
            game.CreateBoard(new AreaSize(1, 1));

            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(0, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(0, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, 1)));
            Assert.AreEqual(0, game.GetAliveNeighbors(new AreaPosition(0, 0)));

            game.Board.Columns[0][0] = true;

            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(0, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(-1, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(0, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(0, 0) + new AreaPosition(1, 1)));
            Assert.AreEqual(0, game.GetAliveNeighbors(new AreaPosition(0, 0)));
        }

        [TestMethod]
        public void Board3x3_CurrentState_Test()
        {
            var game = new Game();
            game.CreateBoard(new AreaSize(3, 3));

            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 1)));
            Assert.AreEqual(0, game.GetAliveNeighbors(new AreaPosition(0, 0)));

            game.Board.Columns[1][1] = true;

            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, -1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 0)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, 1)));
            Assert.AreEqual(0, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 1)));
            Assert.AreEqual(0, game.GetAliveNeighbors(new AreaPosition(1, 1)));

            game.Board.Columns[0][0] = true;
            game.Board.Columns[0][1] = true;
            game.Board.Columns[0][2] = true;
            game.Board.Columns[1][0] = true;
            game.Board.Columns[1][2] = true;
            game.Board.Columns[2][0] = true;
            game.Board.Columns[2][1] = true;
            game.Board.Columns[2][2] = true;

            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, -1)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, -1)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, -1)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 0)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 0)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(-1, 1)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(0, 1)));
            Assert.AreEqual(1, game.CurrentState(new AreaPosition(1, 1) + new AreaPosition(1, 1)));
            Assert.AreEqual(8, game.GetAliveNeighbors(new AreaPosition(1, 1)));
        }

        [TestMethod]
        public void Board1x1_NextBoard_Test()
        {
            var game = new Game();
            game.CreateBoard(new AreaSize(1, 1));
            var nextBoard = game.NextBoard();

            var expectedBoard = new Board(new AreaSize(1, 1));
            expectedBoard.Columns[0][0] = false;

            Assert.That.BoardsEqual(expectedBoard, nextBoard);

            game.Board.Columns[0][0] = true;
            nextBoard = game.NextBoard();

            expectedBoard.Columns[0][0] = false;
            Assert.That.BoardsEqual(expectedBoard, nextBoard);
        }

        [TestMethod]
        public void Board3x3_Empty_NextBoard_Test()
        {
            var game = new Game();
            game.CreateBoard(new AreaSize(3, 3));
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
            var board = game.CreateBoard(new AreaSize(3, 3));

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
            var board = game.CreateBoard(new AreaSize(3, 3));

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

            game.CreateBoard(new AreaSize(1, 1));
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
            game.CreateBoard(new AreaSize(1, 1));
            game.Board.Columns[0][0] = true;
            game.Step();

            game.Reset();

            Assert.AreEqual(0, game.Steps);
            Assert.That.BoardsEqual(new Board(new AreaSize(1, 1)), game.Board);
        }

        [TestMethod]
        public void ImportComponent_Test()
        {
            var game = new Game();
            var board = game.CreateBoard(new AreaSize(3, 3));

            board.Columns[0][0] = true;
            board.Columns[1][1] = true;
            board.Columns[2][2] = true;

            var component = new Board(new AreaSize(3, 3));
            component.Columns[0][0] = true;
            component.Columns[0][1] = true;
            component.Columns[0][2] = true;

            Assert.ThrowsException<ArgumentNullException>(() =>
                game.ImportComponent(new AreaPosition(0, 0), null));

            var actual = game.ImportComponent(new AreaPosition(1, 2), component);

            Assert.AreEqual(game.Board, actual);

            var expected = new Board(new AreaSize(3, 3));
            expected.Columns[0][0] = true;
            expected.Columns[1][1] = true;
            expected.Columns[1][2] = true;
            expected.Columns[2][2] = true;
            Assert.That.BoardsEqual(expected, game.Board);
        }
    }
}
