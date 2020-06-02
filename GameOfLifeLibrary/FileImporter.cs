using System.IO;

namespace GameOfLife.Library
{
    public class FileImporter : IImporter
    {
        public FileImporter(string path)
        {
            this.Path = path;
        }

        public string Path { get; }

        public string Import()
        {
            return File.ReadAllText(this.Path);
        }
    }
}
