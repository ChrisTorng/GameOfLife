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

        public Board ImportComponent(AreaPosition componentOffset, Board component)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            this.Board.ForEachPositionInComponent(component,
                componentOffset,
                (boardPosition, componentPosition) =>
                {
                    if (component[componentPosition])
                    {
                        this.Board[boardPosition] = true;
                    }
                });

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

            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(-1, -1));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(0, -1));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(1, -1));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(-1, 0));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(1, 0));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(-1, 1));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(0, 1));
            aliveNeighbors += this.CurrentState(currentPosition.GetOffsetPosition(1, 1));

            return aliveNeighbors;
        }

        internal int CurrentState(AreaPosition currentPosition)
        {
            if (!this.Board.AreaSize.InThisArea(currentPosition))
            {
                return 0;
            }

            return this.Board[currentPosition] ? 1 : 0;
        }
    }
}
