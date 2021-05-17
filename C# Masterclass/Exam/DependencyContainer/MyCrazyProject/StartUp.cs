namespace MyCrazyProject
{
    using Core;
    using Core.Contracts;
    using MasterInjection;
    using Models;
    using Models.Contracts;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();

            var engine = serviceProvider.CreateInstance<Engine>();
            //var engine = serviceProvider.CreateInstance(typeof(Engine)) as Engine;
            engine.Run();
        }

        private static IMyServiceProvider ConfigureServices()
        {
            var serviceProvider = new MyServiceProvider();

            serviceProvider.Add<IEngine, Engine>();
            serviceProvider.Add<IWriter, FileWriter>();
            serviceProvider.Add<IReader, ConsoleReader>();

            return serviceProvider;
        }
    }
}
