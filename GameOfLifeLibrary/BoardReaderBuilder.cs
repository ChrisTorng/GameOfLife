using System;

namespace GameOfLife.Library
{
    public class BoardReaderBuilder
    {
        public BoardReaderType Type { get; }

        public IImporter Importer { get; private set; }

        public BoardReaderBuilder(BoardReaderType type)
        {
            if (!Enum.IsDefined(typeof(BoardReaderType), type) ||
                type == BoardReaderType.Unknown)
            {
                throw new ArgumentOutOfRangeException(nameof(type));
            }

            this.Type = type;
        }

        public BoardReaderBuilder SetImporter(IImporter importer)
        {
            if (importer is null)
            {
                throw new ArgumentNullException(nameof(importer));
            }

            this.Importer = importer;
            return this;
        }

        public BoardReader Build()
        {
            if (this.Importer == null)
            {
                throw new InvalidOperationException(
                    $"Call {nameof(this.SetImporter)}() method first.");
            }

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
