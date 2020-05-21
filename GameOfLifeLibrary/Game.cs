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
        }

        internal int GetAliveNeighbors(int widthIndex, int heightIndex)
        {
            int aliveNeighbors = 0;

            aliveNeighbors += this.LeftTopState(widthIndex, heightIndex);

            return aliveNeighbors;
        }

        internal int LeftTopState(int widthIndex, int heightIndex)
        {
            if (widthIndex == 0 || heightIndex == 0)
            {
                return 0;
            }

            return this.Board.Columns[widthIndex - 1][heightIndex - 1] ? 1 : 0;
        }
    }
}
