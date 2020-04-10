using System;
using WildFarm.Core;
using WildFarm.Core.Contracts;

namespace WildFarm
{
   public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine();
            engine.Run();
        }
    }
}
