namespace MyCrazyProject.Models
{
    using System.IO;
    using Contracts;

    public class FileWriter : IWriter
    {
        private const string FilePath = "../../../log.txt";

        public void Write(string content)
        {
            File.AppendAllText(FilePath, content);
        }
    }
}
