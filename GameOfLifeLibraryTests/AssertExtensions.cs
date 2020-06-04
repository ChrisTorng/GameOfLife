using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLife.Library.Tests
{
    public static class AssertExtensions
    {
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        internal static void BoardsEqual(this Assert _, Board expected, Board actual)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            Assert.AreEqual(expected.AreaSize, actual.AreaSize);
            Assert.AreEqual(expected.Columns.Length, actual.Columns.Length);

            for (int widthIndex = 0; widthIndex < expected.AreaSize.Width; widthIndex++)
            {
                CollectionAssert.AreEqual(expected.Columns[widthIndex],
                    actual.Columns[widthIndex],
                    $"Not equal on Columns[{widthIndex}].");
            }
        }
    }
}
