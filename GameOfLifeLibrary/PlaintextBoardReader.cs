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

            this.GetBoardSize();

            return true;
        }

        internal override AreaSize GetBoardSize()
        {
            int width = this.lines.Max(line => line.Length);
            int height = this.lines.Length;

            return new AreaSize(width, height);
        }

        internal override void Parse()
        {
            this.Board.ForEachPosition(position =>
            {
                if (this.lines[position.Y].Length > position.X &&
                    this.lines[position.Y][position.X] == AliveChar)
                {
                    this.Board[position] = true;
                }
            });
        }

        private static bool IsCommentLine(string line)
        {
            return line.Length > 0 && line[0] == CommentChar;
        }
    }
}
