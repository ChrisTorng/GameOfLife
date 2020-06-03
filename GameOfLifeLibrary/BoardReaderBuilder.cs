using System;

namespace GameOfLife.Library
{
    public class BoardReaderBuilder
    {
        public BoardReaderType Type { get; }

        public string Content { get; private set; }

        public BoardReaderBuilder(BoardReaderType type)
        {
            if (!Enum.IsDefined(typeof(BoardReaderType), type) ||
                type == BoardReaderType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Type = type;
        }

        public BoardReaderBuilder SetContent(string content)
        {
            this.Content = content;
            return this;
        }

        public BoardReader Build()
        {
#pragma warning disable IDE0010 // Add missing cases
            switch (this.Type)
#pragma warning restore IDE0010 // Add missing cases
            {
            case BoardReaderType.Plaintext:
                return new PlaintextBoardReader(this.Content);

            default:
                throw new NotImplementedException();
            }
        }
    }
}
