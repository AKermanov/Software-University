using System;

namespace MXGP
{
    using Models.Motorcycles;
    using MXGP.Core.Contracts;
    using MXGP.Core.Models;
    using MXGP.Models.Motorcycles.Contracts;
    using MXGP.Models.Motorcycles.Models;
    using MXGP.Models.Races;
    using MXGP.Models.Races.Models;
    using MXGP.Models.Riders.Contracts;
    using MXGP.Repositories.Models;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            IEngine engine = new Engine();
             engine.Run();

            
        }
    }
}
