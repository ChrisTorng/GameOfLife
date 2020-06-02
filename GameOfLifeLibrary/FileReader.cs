using System.IO;

namespace GameOfLife.Library
{
    public class FileReader : IReader
    {
        public string ReadAll(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
