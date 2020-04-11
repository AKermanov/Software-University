using MXGP.Core.Contracts;
using MXGP.IO;
using MXGP.IO.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Core.Models
{
    public class Engine : IEngine
    {
        private IChampionshipController controller;
        private IReader reader;
        private IWriter writer;

        public Engine()
        {
            this.controller = new ChampionshipController();
            this.reader = new ConsoleReader();
            this.writer = new ConsoleWriter();
        }
        public void Run()
        {
            var command = reader.ReadLine();

            while (command != "End")
            {
                var cmdArgs = command.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                var type = cmdArgs[0];
                string result = null;
                try
                {
                    if (type == "CreateRider")
                    {
                        result = this.controller.CreateRider(cmdArgs[1]);
                    }
                    else if (type == "CreateMotorcycle")
                    {
                        result = this.controller.CreateMotorcycle(cmdArgs[1], cmdArgs[2], int.Parse(cmdArgs[3]));
                    }
                    else if (type == "AddMotorcycleToRider")
                    {
                        result = this.controller.AddMotorcycleToRider(cmdArgs[1], cmdArgs[2]);
                    }
                    else if (type == "AddRiderToRace")
                    {
                        result = this.controller.AddRiderToRace(cmdArgs[1], cmdArgs[2]);
                    }
                    else if (type == "CreateRace")
                    {
                        result = this.controller.CreateRace(cmdArgs[1], int.Parse(cmdArgs[2]));
                    }
                    else if (type == "StartRace")
                    {
                        result = this.controller.StartRace(cmdArgs[1]);
                    }
                    writer.WriteLine(result);
                }
                catch (Exception ex)
                {

                    writer.WriteLine(ex.Message);
                }

                command = reader.ReadLine();
            }
        }
    }
}
