namespace GameOfLife.Library
{
    public abstract class BoardReader
    {
        protected BoardReader(IImporter importer)
        {
            this.Importer = importer;
        }

        public Board Board { get; protected set; }

        public IImporter Importer { get; }

        public Board GetBoardByContent(string content)
        {
            this.SetContent(content);
            this.SetBoardSize();
            this.Parse();

            return this.Board;
        }

        public Board GetBoardByImporter()
        {
            var content = this.Importer.Import();
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
