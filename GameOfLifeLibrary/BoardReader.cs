﻿using System;

namespace GameOfLife.Library
{
    public abstract class BoardReader
    {
        public Board Board { get; protected set; }

        public string Content { get; }

        protected BoardReader(string content)
        {
            if (content is null)
            {
                throw new ArgumentNullException(nameof(content));
            }

            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException(
                    $"Parameter {nameof(content)} should have valid content.",
                    nameof(content));
            }

            this.Content = content;
        }

        public Board GetBoardByContent()
        {
            if (!this.Validate())
            {
                return null;
            }

            this.SetBoardSize();
            this.Parse();

            return this.Board;
        }

        public abstract bool Validate();

        internal void SetBoardSize()
        {
            var areaSize = this.GetBoardSize();
            this.Board = new Board(areaSize);
        }

        internal abstract AreaSize GetBoardSize();

        internal abstract void Parse();
    }
}
