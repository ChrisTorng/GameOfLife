using System;

namespace GameOfLife.Library
{
    public class BoardReaderBuilder
    {
        public BoardReaderType Type { get; }

        public BoardReaderBuilder(BoardReaderType type)
        {
            if (!Enum.IsDefined(typeof(BoardReaderType), type) ||
                type == BoardReaderType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Type = type;
        }

        public BoardReader Build()
        {
#pragma warning disable IDE0010 // Add missing cases
            switch (this.Type)
#pragma warning restore IDE0010 // Add missing cases
            {
            case BoardReaderType.Plaintext:
                return new PlaintextBoardReader();
            default:
                throw new NotImplementedException();
            }
        }
    }
}
