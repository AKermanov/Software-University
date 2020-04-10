using System;

namespace Vehicles.Core
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            engine.Run();
        }
    }
}
