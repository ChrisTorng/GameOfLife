using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    public static class AssertExtensions
    {
        internal static void BoardsEqual(this Assert _, Board expected, Board actual)
        {
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);
            Assert.AreEqual(expected.Columns.Length, actual.Columns.Length);

            for (int widthIndex = 0; widthIndex < expected.Width; widthIndex++)
            {
                CollectionAssert.AreEqual(expected.Columns[widthIndex],
                    actual.Columns[widthIndex],
                    $"Not equal on Columns[{widthIndex}].");
            }
        }
    }
}
