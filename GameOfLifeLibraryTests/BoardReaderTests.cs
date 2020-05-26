using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class BoardReaderTests
    {
        [TestMethod]
        public void GetBoardReader_Test()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                BoardReader.GetBoardReader("a.b"));

            var boardReader = BoardReader.GetBoardReader("a.cells");
            Assert.IsInstanceOfType(boardReader, typeof(PlaintextBoardReader));
        }

        [TestMethod]
        public void GetPlaintextBoardTest()
        {
            var board = BoardReader.GetPlaintextBoard(
@".O.
O.O");

            var expected = new Board(3, 2);
            expected.Columns[1][0] = true;
            expected.Columns[0][1] = true;
            expected.Columns[2][1] = true;
            BoardTests.BoardsEqual(expected, board);
        }
    }
}
