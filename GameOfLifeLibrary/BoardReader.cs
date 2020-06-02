using System;
using System.IO;

namespace GameOfLife.Library
{
    public abstract class BoardReader
    {
        protected BoardReader(IReader reader)
        {
            this.Reader = reader;
        }

        public Board Board { get; protected set; }

        public IReader Reader { get; }

        internal static BoardReader GetBoardReader(string path)
        {
            if (Path.GetExtension(path) == ".cells")
            {
                return new PlaintextBoardReader(new FileReader());
            }

            throw new ArgumentOutOfRangeException(nameof(path),
                "Only Plaintext .cells file allowed.");
        }

        public static Board GetBoard(string path)
        {
            BoardReader reader = GetBoardReader(path);
            string content = reader.Reader.ReadAll(path);
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
