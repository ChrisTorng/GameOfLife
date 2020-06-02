using System;
using System.Linq;

namespace GameOfLife.Library
{
    public class PlaintextBoardReader : BoardReader
    {
        private const char CommentChar = '!';
        private const char AliveChar = 'O';
        private string[] lines;

        internal PlaintextBoardReader()
        {
        }

        internal override void SetContent(string content)
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
                .Where(line => !IsCommentLine(line))
                .Select(line => line.Trim()).ToArray();

            if (this.lines.Length == 0)
            {
                throw new ArgumentException(
                    $"Parameter {nameof(content)} should have valid content except comment line.",
                    nameof(content));
            }
        }

        internal override void SetBoardSize()
        {
            (int width, int height) = this.GetBoardSize();
            this.Board = new Board(width, height);
        }

        private (int Width, int Height) GetBoardSize()
        {
            int width = this.lines.Max(line => line.Length);
            int height = this.lines.Length;

            if (width <= 0)
            {
                throw new InvalidOperationException("No line has valid content while counting width.");
            }

            return (width, height);
        }

        internal override void Parse()
        {
            for (int heightIndex = 0; heightIndex < this.lines.Length; heightIndex++)
            {
                this.ParseLine(heightIndex, this.lines[heightIndex]);
            }
        }

        private void ParseLine(int heightIndex, string line)
        {
            for (int widthIndex = 0; widthIndex < line.Length; widthIndex++)
            {
                if (line[widthIndex] == AliveChar)
                {
                    this.Board.Columns[widthIndex][heightIndex] = true;
                }
            }
        }

        private static bool IsCommentLine(string line)
        {
            return line.Length > 0 && line[0] == CommentChar;
        }
    }
}
