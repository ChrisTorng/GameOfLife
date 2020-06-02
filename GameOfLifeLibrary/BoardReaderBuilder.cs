using System;

namespace GameOfLife.Library
{
    public class BoardReaderBuilder
    {
        private IReader reader;

        public BoardReaderType Type { get; }

        public IReader Reader
        {
            get
            {
                if (this.reader == null)
                {
                    this.reader = new FileReader();
                }

                return this.reader;
            }

            private set
            {
                this.reader = value;
            }
        }

        public BoardReaderBuilder(BoardReaderType type)
        {
            if (!Enum.IsDefined(typeof(BoardReaderType), type) ||
                type == BoardReaderType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Type = type;
        }

        public BoardReaderBuilder SetReader(IReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            this.Reader = reader;
            return this;
        }

        public BoardReader Build()
        {
#pragma warning disable IDE0010 // Add missing cases
            switch (this.Type)
#pragma warning restore IDE0010 // Add missing cases
            {
            case BoardReaderType.Plaintext:
                return new PlaintextBoardReader(this.Reader);
            default:
                throw new NotImplementedException();
            }
        }
    }
}
