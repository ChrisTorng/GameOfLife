using System;
using System.IO;

namespace GameOfLife.Library
{
    public abstract class BoardReader
    {
        public Board Board { get; protected set; }

        internal static BoardReader GetBoardReader(string path)
        {
            if (Path.GetExtension(path) == ".cells")
            {
                return new PlaintextBoardReader();
            }

            throw new ArgumentOutOfRangeException(nameof(path),
                "Only Plaintext .cells file allowed.");
        }

        public static Board GetBoard(string path)
        {
            BoardReader reader = GetBoardReader(path);
            string content = File.ReadAllText(path);
            return reader.GetBoardByContent(content);
        }

        public Board GetBoardByContent(string content)
        {
            this.SetContent(content);
            this.SetBoardSize();
            this.Parse();

            return this.Board;
        }

        internal abstract void SetContent(string content);

        internal abstract void SetBoardSize();

        internal abstract void Parse();
    }
}
