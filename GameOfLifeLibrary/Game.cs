﻿using System;
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

        public Board ImportComponent(int widthOffset, int heightOffset, Board component)
        {
            if (component is null)
            {
                throw new ArgumentNullException(nameof(component));
            }

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
            for (int widthIndex = 0; widthIndex < this.Board.AreaSize.Width; widthIndex++)
            {
                for (int heightIndex = 0; heightIndex < this.Board.AreaSize.Height; heightIndex++)
                {
                    this.ApplyCellRules(widthIndex, heightIndex, nextBoard);
                }
            }
        }

        internal void ApplyCellRules(int widthIndex, int heightIndex, Board nextBoard)
        {
            int aliveNeighbors = this.GetAliveNeighbors(widthIndex, heightIndex);
            if (this.Board.Columns[widthIndex][heightIndex])
            {
                if (aliveNeighbors >= 2 && aliveNeighbors <= 3)
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
            if (left < 0 || left >= this.Board.AreaSize.Width ||
                top < 0 || top >= this.Board.AreaSize.Height)
            {
                return 0;
            }

            return this.Board.Columns[left][top] ? 1 : 0;
        }
    }
}
