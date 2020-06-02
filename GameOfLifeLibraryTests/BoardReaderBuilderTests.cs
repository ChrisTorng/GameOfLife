using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    [TestClass]
    public class BoardReaderBuilderTests
    {
        [TestMethod]
        public void InvalidType_Test()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BoardReaderBuilder((BoardReaderType)(-1)));

            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                new BoardReaderBuilder(BoardReaderType.Unknown));
        }

        [TestMethod]
        public void PlaintextType_Test()
        {
            var builder = new BoardReaderBuilder(BoardReaderType.Plaintext);

            Assert.AreEqual(BoardReaderType.Plaintext, builder.Type);
        }

        [TestMethod]
        public void PlaintextType_NoSetReader_Test()
        {
            var builder = new BoardReaderBuilder(BoardReaderType.Plaintext);
            var boardReader = builder.Build();

            Assert.IsInstanceOfType(boardReader.Reader, typeof(FileReader));
        }

        [TestMethod]
        public void PlaintextType_SetReaderNull_Test()
        {
            var builder = new BoardReaderBuilder(BoardReaderType.Plaintext);

            Assert.ThrowsException<ArgumentNullException>(() =>
                builder.SetReader(null));
        }

        [TestMethod]
        public void PlaintextType_SetReader_Test()
        {
            var builder = new BoardReaderBuilder(BoardReaderType.Plaintext);

            var mockReader = new MockReader(null);
            builder.SetReader(mockReader);

            var boardReader = builder.Build();

            Assert.IsInstanceOfType(boardReader.Reader, typeof(MockReader));
            Assert.AreEqual(mockReader, boardReader.Reader);
        }
    }
}
