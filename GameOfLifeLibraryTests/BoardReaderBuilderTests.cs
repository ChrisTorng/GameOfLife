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
    }
}
