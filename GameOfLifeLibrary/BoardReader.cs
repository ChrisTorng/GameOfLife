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
            return GetBoardByContent(reader, content);
        }

        internal static Board GetPlaintextBoard(string content)
        {
            return GetBoardByContent(new PlaintextBoardReader(), content);
        }

        private static Board GetBoardByContent(BoardReader reader, string content)
        {
            reader.SetContent(content);
            reader.SetBoardSize();
            reader.Parse();

            return reader.Board;
        }

        internal abstract void SetContent(string content);

        internal abstract void SetBoardSize();

        internal abstract void Parse();
    }
}
