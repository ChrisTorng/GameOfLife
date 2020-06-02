namespace GameOfLife.Library.Tests
{
    internal class MockReader : IReader
    {
        public MockReader(string content)
        {
            this.Content = content;
        }

        public string Content { get; }

        public string ReadAll(string path)
        {
            return this.Content;
        }
    }
}
