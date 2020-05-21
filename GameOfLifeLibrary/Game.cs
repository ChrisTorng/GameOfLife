using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameOfLifeLibraryTests")]

namespace GameOfLife.Library
{
    public class Game
    {
        public int Steps { get; }

        public Board Board { get; private set; }

        public Game()
        {
        }

        public Board CreateBoard(int width, int height)
        {
            this.Board = new Board(width, height);
            return this.Board;
        }

        public Board NextBoard()
        {
            Board nextBoard = new Board(this.Board.Width, this.Board.Height);
            this.ApplyBoardRules(nextBoard);
            return nextBoard;
        }

        private void ApplyBoardRules(Board nextBoard)
        {
            for (int widthIndex = 0; widthIndex < this.Board.Width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < this.Board.Height; heightIndex++)
                {
                    this.ApplyCellRules(widthIndex, heightIndex, nextBoard);
                }
            }
        }

        internal void ApplyCellRules(int widthIndex, int heightIndex, Board nextBoard)
        {
            int aliveNeighbors = this.GetAliveNeighbors(widthIndex, heightIndex);
            if (nextBoard.Columns[widthIndex][heightIndex])
            {
                if (aliveNeighbors >= 2 || aliveNeighbors <= 3)
                {
                    nextBoard.Columns[widthIndex][heightIndex] = true;
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    nextBoard.Columns[widthIndex][heightIndex] = true;
                }
            }
        }

        internal int GetAliveNeighbors(int widthIndex, int heightIndex)
        {
            int aliveNeighbors = 0;

            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, -1, -1);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, 0, -1);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, 1, -1);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, -1, 0);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, 1, 0);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, -1, 1);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, 0, 1);
            aliveNeighbors += this.CurrentState(widthIndex, heightIndex, 1, 1);

            return aliveNeighbors;
        }

        internal int CurrentState(int widthIndex, int heightIndex,
            int leftOffset, int topOffset)
        {
            int left = widthIndex + leftOffset;
            int top = heightIndex + topOffset;
            if (left < 0 || left >= this.Board.Width ||
                top < 0 || top >= this.Board.Height)
            {
                return 0;
            }

            return this.Board.Columns[left][top] ? 1 : 0;
        }
    }
}
