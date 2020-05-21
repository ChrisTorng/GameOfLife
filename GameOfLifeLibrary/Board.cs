using System;
using System.Collections;

namespace GameOfLife.Library
{
    public class Board
    {
        public Board(int width, int height)
        {
            if (width <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(width));
            }

            if (height <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(height));
            }

            this.Width = width;
            this.Height = height;
            this.Columns = new BitArray[width];

            for (int index = 0; index < this.Columns.Length; index++)
            {
                this.Columns[index] = new BitArray(height);
            }
        }

        public int Width { get; }

        public int Height { get; }

#pragma warning disable CA1819 // Properties should not return arrays
        public BitArray[] Columns { get; }
#pragma warning restore CA1819 // Properties should not return arrays

        public bool Flip(int x, int y)
        {
            return this.Columns[x][y] = !this.Columns[x][y];
        }
    }
}
