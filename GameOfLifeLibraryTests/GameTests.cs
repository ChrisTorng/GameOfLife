﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            game.Board.Columns[1][1] = true;
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

            Assert.AreEqual(false, nextBoard.Columns[0][0]);

            game.Board.Columns[0][0] = true;

            Assert.AreEqual(false, nextBoard.Columns[0][0]);
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
    }
}