using MortalEngines.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Core
{
    public class Engine : IEngine
    {
        public void Run()
        {
            var machinesMenager = new MachinesManager();
            string command = null;
            while((command = Console.ReadLine()) != "Quit")
            {
                var cmdArgs = command.Split();
                var operation = cmdArgs[0];
                var name = cmdArgs[1];
                var commandMachineManager = string.Empty;
                try
                {
                    if (operation == "HirePilot")
                    {
                        commandMachineManager= machinesMenager.HirePilot(name);
                    }
                    else if (operation == "PilotReport")
                    {
                        commandMachineManager= machinesMenager.PilotReport(name);
                    }
                    else if (operation == "ManufactureTank")
                    {
                        var attack = double.Parse(cmdArgs[2]);
                        var defence = double.Parse(cmdArgs[3]);
                        commandMachineManager= machinesMenager.ManufactureTank(name, attack, defence);
                    }
                    else if (operation == "ManufactureFighter")
                    {
                        var attack = double.Parse(cmdArgs[2]);
                        var defence = double.Parse(cmdArgs[3]);
                        commandMachineManager= machinesMenager.ManufactureFighter(name, attack, defence);
                    }
                    else if (operation == "MachineReport")
                    {
                        commandMachineManager= machinesMenager.MachineReport(name);
                    }
                    else if (operation == "AggressiveMode")
                    {
                        commandMachineManager=machinesMenager.ToggleFighterAggressiveMode(name);
                      
                    }
                    else if (operation == "DefenseMode")
                    {
                        commandMachineManager= machinesMenager.ToggleTankDefenseMode(name);
                    }
                    else if (operation == "Engage")
                    {
                        var machineName = cmdArgs[2];
                        commandMachineManager= machinesMenager.EngageMachine(name, machineName);
                    }
                    else if (operation == "Attack")
                    {
                        var deffMachineName = cmdArgs[2];
                        commandMachineManager= machinesMenager.AttackMachines(name, deffMachineName);
                    }
               }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
                Console.WriteLine(commandMachineManager);
            }
        }
    }
}
