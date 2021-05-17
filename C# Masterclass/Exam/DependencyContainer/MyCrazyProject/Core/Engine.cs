namespace MyCrazyProject.Core
{
    using Contracts;
    using Models.Contracts;

    public class Engine : IEngine
    {
        private readonly IWriter fileWriter;
        private readonly IReader consoleReader;

        public Engine(IWriter fileWriter, IReader consoleReader)
        {
            this.fileWriter = fileWriter;
            this.consoleReader = consoleReader;
        }

        public void Run()
        {
            string content = consoleReader.Read();
            this.fileWriter.Write(content);
        }
    }
}
