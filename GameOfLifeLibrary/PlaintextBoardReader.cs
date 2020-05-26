using System;
using System.Linq;

namespace GameOfLife.Library
{
    public class PlaintextBoardReader : BoardReader
    {
        private const char CommentChar = '!';
        private const char AliveChar = 'O';
        private string[] lines;

        public PlaintextBoardReader()
        {
        }

        public Board Board { get; private set; }

        internal void SetContent(string content)
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

            this.lines = content.Split('\n')
                .Select(l => l.Trim()).ToArray();
        }

        internal void SetBoardSize()
        {
            (int width, int height) = this.GetBoardSize();
            this.Board = new Board(width, height);
        }

        private (int width, int height) GetBoardSize()
        {
            int width = this.lines.Max(line => this.ValidLineWidth(line));
            int height = this.lines.Count(line => this.ValidLineWidth(line) >= 0);

            if (width <= 0)
            {
                throw new InvalidOperationException("No line has valid content while counting width.");
            }

            return (width, height);
        }

        private int ValidLineWidth(string line)
        {
            if (IsCommentLine(line))
            {
                return -1;
            }

            return line.Length;
        }

        internal void Parse()
        {
            int heightIndex = 0;
            foreach (string line in this.lines)
            {
                if (this.ParseLine(heightIndex, line))
                {
                    heightIndex++;
                }
            }
        }

        private bool ParseLine(int heightIndex, string line)
        {
            if (IsCommentLine(line))
            {
                return false;
            }

            for (int widthIndex = 0; widthIndex < line.Length; widthIndex++)
            {
                if (line[widthIndex] == AliveChar)
                {
                    this.Board.Columns[widthIndex][heightIndex] = true;
                }
            }

            return true;
        }

        private static bool IsCommentLine(string line)
        {
            return line.Length > 0 && line[0] == CommentChar;
        }
    }
}
