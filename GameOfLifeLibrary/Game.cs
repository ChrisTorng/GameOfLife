using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GameOfLifeLibraryTests")]

namespace GameOfLife.Library
{
    public class Game
    {
        public int Steps { get; private set; }

        public Board Board { get; private set; }

        public Game()
        {
        }

        public Board CreateBoard(AreaSize areaSize)
        {
            this.Board = new Board(areaSize);
            return this.Board;
        }

        public Board Reset()
        {
            this.Steps = 0;
            return this.CreateBoard(this.Board.AreaSize);
        }

        public Board ImportComponent(AreaPosition areaPosition, Board component)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            int widthOffset = areaPosition.X;
            int heightOffset = areaPosition.Y;
            for (int widthIndex = 0;
                widthIndex < component.AreaSize.Width &&
                widthIndex + widthOffset < this.Board.AreaSize.Width;
                widthIndex++)
            {
                for (int heightIndex = 0;
                    heightIndex < component.AreaSize.Height &&
                    heightIndex + heightOffset < this.Board.AreaSize.Height;
                    heightIndex++)
                {
                    if (component.Columns[widthIndex][heightIndex])
                    {
                        this.Board.Columns[widthIndex + widthOffset][heightIndex + heightOffset] = true;
                    }
                }
            }

            return this.Board;
        }

        public Board NextBoard()
        {
            Board nextBoard = new Board(this.Board.AreaSize);
            this.ApplyBoardRules(nextBoard);
            return nextBoard;
        }

        public Board Step()
        {
            this.Steps++;
            return this.Board = this.NextBoard();
        }

        private void ApplyBoardRules(Board nextBoard)
        {
            this.Board.ForEachPosition(areaPosition =>
                this.ApplyCellRules(areaPosition, nextBoard));
        }

        internal void ApplyCellRules(AreaPosition areaPosition, Board nextBoard)
        {
            int aliveNeighbors = this.GetAliveNeighbors(areaPosition);
            if (this.Board[areaPosition])
            {
                if (aliveNeighbors >= 2 && aliveNeighbors <= 3)
                {
                    nextBoard[areaPosition] = true;
                }
            }
            else
            {
                if (aliveNeighbors == 3)
                {
                    nextBoard[areaPosition] = true;
                }
            }
        }

        internal int GetAliveNeighbors(AreaPosition currentPosition)
        {
            int aliveNeighbors = 0;

            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(-1, -1));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(0, -1));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(1, -1));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(-1, 0));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(1, 0));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(-1, 1));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(0, 1));
            aliveNeighbors += this.CurrentState(currentPosition + new AreaPosition(1, 1));

            return aliveNeighbors;
        }

        internal int CurrentState(AreaPosition currentPosition)
        {
            if (currentPosition.X < 0 || currentPosition.X >= this.Board.AreaSize.Width ||
                currentPosition.Y < 0 || currentPosition.Y >= this.Board.AreaSize.Height)
            {
                return 0;
            }

            return this.Board[currentPosition] ? 1 : 0;
        }
    }
}
