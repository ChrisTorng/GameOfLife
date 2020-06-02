namespace GameOfLife.Library
{
    public abstract class BoardReader
    {
        public Board Board { get; protected set; }

        public Board GetBoardByContent(string content)
        {
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
