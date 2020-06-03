using System.Linq;

namespace GameOfLife.Library
{
    public class PlaintextBoardReader : BoardReader
    {
        private const char CommentChar = '!';
        private const char AliveChar = 'O';
        private string[] lines;

        public PlaintextBoardReader(string content)
            : base(content)
        {
        }

        public override bool Validate()
        {
            this.lines = this.Content.Split('\n')
                .Where(line => line.Trim().Length > 0 && !IsCommentLine(line))
                .Select(line => line.Trim()).ToArray();

            if (this.lines.Length == 0)
            {
                return false;
            }

            (int width, int height) = this.GetBoardSize();
            if (width <= 0 || height <= 0)
            {
                return false;
            }

            return true;
        }

        internal override (int Width, int Height) GetBoardSize()
        {
            int width = this.lines.Max(line => line.Length);
            int height = this.lines.Length;

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
