namespace GameOfLife.Library.Tests
{
    internal class MockImporter : IImporter
    {
        public MockImporter()
        {
        }

        public MockImporter(string content)
        {
            this.Content = content;
        }

        public string Content { get; private set; }

        public void SetContent(string content)
        {
            this.Content = content;
        }

        public string Import()
        {
            return this.Content;
        }
    }
}
