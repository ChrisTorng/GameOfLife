using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class BoardReaderTests
    {
        private static PlaintextBoardReader GetPlaintextBoardReader(string content) =>
            new BoardReaderBuilder(BoardReaderType.Plaintext)
                .SetContent(content)
                .Build() as PlaintextBoardReader;

        [TestMethod]
        public void Plaintext_GetBoardByContent_Test()
        {
            var board = GetPlaintextBoardReader(
@".O.
O.O")
                .GetBoardByContent();

            var expected = new Board(new AreaSize(3, 2));
            expected.Columns[1][0] = true;
            expected.Columns[0][1] = true;
            expected.Columns[2][1] = true;
            Assert.That.BoardsEqual(expected, board);

            board = GetPlaintextBoardReader(
@"!Name: 1 beacon
!Approximately the 32nd-most common oscillator.
!www.conwaylife.com/wiki/index.php?title=1_beacon
..OO
.O.O
O..O.OO
OO.O..O
.O.O
.O..O
..OO")
                .GetBoardByContent();

            expected = new Board(new AreaSize(7, 7));
            expected.Columns[2][0] = true;
            expected.Columns[3][0] = true;
            expected.Columns[1][1] = true;
            expected.Columns[3][1] = true;
            expected.Columns[0][2] = true;
            expected.Columns[3][2] = true;
            expected.Columns[5][2] = true;
            expected.Columns[6][2] = true;
            expected.Columns[0][3] = true;
            expected.Columns[1][3] = true;
            expected.Columns[3][3] = true;
            expected.Columns[6][3] = true;
            expected.Columns[1][4] = true;
            expected.Columns[3][4] = true;
            expected.Columns[1][5] = true;
            expected.Columns[4][5] = true;
            expected.Columns[2][6] = true;
            expected.Columns[3][6] = true;
            Assert.That.BoardsEqual(expected, board);
        }
    }
}
