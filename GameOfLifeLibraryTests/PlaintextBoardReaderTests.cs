using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class PlaintextBoardReaderTests
    {
        [TestMethod]
        public void SetContent_Invalid_Test()
        {
            var reader = new PlaintextBoardReader();

            Assert.ThrowsException<ArgumentNullException>(() => reader.SetContent(null));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent(string.Empty));

            Assert.ThrowsException<ArgumentException>(() => reader.SetContent(" "));
        }

        [TestMethod]
        public void SetBoardSize_Invalid_Test()
        {
            var reader = new PlaintextBoardReader();

            reader.SetContent("!comment");
            Assert.ThrowsException<InvalidOperationException>(() => reader.SetBoardSize());

            reader.SetContent(
@"!


!comment");
            Assert.ThrowsException<InvalidOperationException>(() => reader.SetBoardSize());
        }

        [TestMethod]
        public void SetBoardSize_Test()
        {
            var reader = new PlaintextBoardReader();

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
        public void Parse_Test()
        {
            var reader = new PlaintextBoardReader();

            reader.SetContent(".");
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = false;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);

            reader.SetContent("O");
            reader.SetBoardSize();
            reader.Parse();

            expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = true;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);

            reader.SetContent("OO.");
            reader.SetBoardSize();
            reader.Parse();

            expectedBoard = new Board(3, 1);
            expectedBoard.Columns[0][0] = true;
            expectedBoard.Columns[1][0] = true;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);

            reader.SetContent(".O.");
            reader.SetBoardSize();
            reader.Parse();

            expectedBoard = new Board(3, 1);
            expectedBoard.Columns[1][0] = true;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);

            reader.SetContent(
@"O
");
            reader.SetBoardSize();
            reader.Parse();

            expectedBoard = new Board(1, 2);
            expectedBoard.Columns[0][0] = true;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);

            reader.SetContent(
@"

OO.
");
            reader.SetBoardSize();
            reader.Parse();

            expectedBoard = new Board(3, 4);
            expectedBoard.Columns[0][2] = true;
            expectedBoard.Columns[1][2] = true;
            BoardTests.BoardsEqual(expectedBoard, reader.Board);
        }
    }
}
