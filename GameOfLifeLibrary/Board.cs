using System.Collections;

namespace GameOfLife.Library
{
    public class Board
    {
        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Rows = new BitArray[height];

            for (int index = 0; index < this.Rows.Length; index++)
            {
                this.Rows[index] = new BitArray(width);
            }
        }

        public int Width { get; }

        public int Height { get; }

#pragma warning disable CA1819 // Properties should not return arrays
        public BitArray[] Rows { get; }
#pragma warning restore CA1819 // Properties should not return arrays
    }
}
