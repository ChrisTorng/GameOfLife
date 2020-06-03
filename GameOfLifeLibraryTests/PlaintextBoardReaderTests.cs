using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class PlaintextBoardReaderTests
    {
        private static PlaintextBoardReader GetPlaintextBoardReader(string content) =>
            new BoardReaderBuilder(BoardReaderType.Plaintext)
                .SetContent(content)
                .Build() as PlaintextBoardReader;

        [TestMethod]
        public void Content_Invalid_Test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => GetPlaintextBoardReader(null));

            Assert.ThrowsException<ArgumentException>(() => GetPlaintextBoardReader(string.Empty));

            Assert.ThrowsException<ArgumentException>(() => GetPlaintextBoardReader(" "));
        }

        [TestMethod]
        [DataRow("!")]
        [DataRow(
@"!
!")]
        [DataRow(
@"!

 
!comment")]
        public void Validate_Invalid_Test(string content)
        {
            var reader = GetPlaintextBoardReader(content);
            Assert.IsFalse(reader.Validate());
        }

        [TestMethod]
        public void GetBoardSize_Invalid_Test()
        {
            var reader = GetPlaintextBoardReader(
@"!

 
.
.O
!comment");
            reader.Validate();

            (int width, int height) = reader.GetBoardSize();

            Assert.AreEqual(2, width);
            Assert.AreEqual(2, height);
        }

        [TestMethod]
        public void SetBoardSize_Test()
        {
            var reader = GetPlaintextBoardReader(".");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader = GetPlaintextBoardReader("..");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader = GetPlaintextBoardReader(
@".
");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader = GetPlaintextBoardReader(
@"
.");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(1, reader.Board.Width);
            Assert.AreEqual(1, reader.Board.Height);

            reader = GetPlaintextBoardReader(
@"..
.");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader = GetPlaintextBoardReader(
@".
..");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);

            reader = GetPlaintextBoardReader(
@"!comment
.
..
!comment");
            reader.Validate();
            reader.SetBoardSize();
            Assert.AreEqual(2, reader.Board.Width);
            Assert.AreEqual(2, reader.Board.Height);
        }

        [TestMethod]
        public void Parse_1x1Dead_Test()
        {
            var reader = GetPlaintextBoardReader(".");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = false;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_1x1Alive_Test()
        {
            var reader = GetPlaintextBoardReader("O");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_WithComment_Test()
        {
            var reader = GetPlaintextBoardReader(
@"!
O");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 1);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_3x1Case1_Test()
        {
            var reader = GetPlaintextBoardReader("OO.");
            reader.Validate();
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
            var reader = GetPlaintextBoardReader(".O.");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(3, 1);
            expectedBoard.Columns[1][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_1x2TwoLines_Test()
        {
            var reader = GetPlaintextBoardReader(
@"O
.");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(1, 2);
            expectedBoard.Columns[0][0] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }

        [TestMethod]
        public void Parse_3x4_Test()
        {
            var reader = GetPlaintextBoardReader(
@".
.
OO.
.");
            reader.Validate();
            reader.SetBoardSize();
            reader.Parse();

            var expectedBoard = new Board(3, 4);
            expectedBoard.Columns[0][2] = true;
            expectedBoard.Columns[1][2] = true;
            Assert.That.BoardsEqual(expectedBoard, reader.Board);
        }
    }
}
