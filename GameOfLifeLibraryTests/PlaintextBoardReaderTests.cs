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
        }
    }
}
