using System.Collections;

namespace GameOfLife.Library
{
    public class Board
    {
        public Board(AreaSize areaSize)
        {
            this.Columns = new BitArray[areaSize.Width];

            for (int index = 0; index < this.Columns.Length; index++)
            {
                this.Columns[index] = new BitArray(areaSize.Height);
            }

            this.AreaSize = areaSize;
        }

        public AreaSize AreaSize { get; }

#pragma warning disable CA1819 // Properties should not return arrays
        public BitArray[] Columns { get; }
#pragma warning restore CA1819 // Properties should not return arrays

        public bool Flip(int x, int y)
        {
            return this.Columns[x][y] = !this.Columns[x][y];
        }
    }
}
