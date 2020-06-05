using System;
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

#pragma warning disable CA1043 // Use Integral Or String Argument For Indexers
        public bool this[AreaPosition areaPosition]
#pragma warning restore CA1043 // Use Integral Or String Argument For Indexers
        {
            get => this.Columns[areaPosition.X][areaPosition.Y];
            set => this.Columns[areaPosition.X][areaPosition.Y] = value;
        }

        public void ForEachPosition(Action<AreaPosition> action)
            => this.AreaSize.ForEachPosition(action);

        public void ForEachPositionInComponent(Board component,
            AreaPosition componentOffset,
            Action<AreaPosition, AreaPosition> action)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (action is null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            component.ForEachPosition(componentPosition =>
            {
                var boardPosition = componentOffset + componentPosition;
                if (this.AreaSize.InThisArea(boardPosition))
                {
                    action(boardPosition, componentPosition);
                }
            });
        }

        public bool Flip(int x, int y)
        {
            return this.Columns[x][y] = !this.Columns[x][y];
        }
    }
}
