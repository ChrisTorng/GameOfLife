﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class PlaintextBoardReaderTests
    {
        private static PlaintextBoardReader GetPlaintextBoardReader() =>
            new BoardReaderBuilder(BoardReaderType.Plaintext)
                .Build() as PlaintextBoardReader;

        [TestMethod]
        public void SetContent_Invalid_Test()
        {
            var reader = GetPlaintextBoardReader();

            Assert.ThrowsException<ArgumentNullException>(() => reader.SetContent(null));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent(string.Empty));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent(" "));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent("!"));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent(
@"!
!"));
        }

        [TestMethod]
        public void SetBoardSize_Invalid_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(
@"!


!comment");
            Assert.ThrowsException<InvalidOperationException>(() => reader.SetBoardSize());
        }

        [TestMethod]
        public void SetBoardSize_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(".");
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader.SetContent("..");
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader.SetContent(
@".
");
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader.SetContent(
@"
.");
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader.SetContent(
@"..
.");
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader.SetContent(
@".
..");
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader.SetContent(
@"!comment
.
..
!comment");
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);
        }

        [TestMethod]
        public void Parse_1x1Dead_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(".");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = false;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_1x1Alive_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent("O");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_WithComment_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(
@"!
O");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_3x1Case1_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent("OO.");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(3, 1);
            expectedBoard.Columns[0][0] = true;
            expectedBoard.Columns[1][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_3x1Case2_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(".O.");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(3, 1);
            expectedBoard.Columns[1][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_1x2TwoLines_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(
@"O
");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 2);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_3x4_Test()
        {
            var reader = GetPlaintextBoardReader();

            reader.SetContent(
@"

OO.
");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(3, 4);
            expectedBoard.Columns[0][2] = true;
            expectedBoard.Columns[1][2] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }
    }
}
