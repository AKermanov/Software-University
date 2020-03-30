namespace MortalEngines.Core
{
    using Contracts;
    using MortalEngines.Common;
    using MortalEngines.Entities;
    using MortalEngines.Entities.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class MachinesManager : IMachinesManager
    {
        private readonly ICollection<IPilot> pilots;
        private readonly ICollection<IMachine> machines;
        private readonly ICollection<ITank> tanks;
        private readonly ICollection<IFighter> fighters;

        public MachinesManager()
        {
            this.pilots = new List<IPilot>();
            this.machines = new List<IMachine>();
            this.tanks = new List<ITank>();
            this.fighters = new List<IFighter>();

        }
        public string HirePilot(string name)
        {
            IPilot pilot = new Pilot(name);
            if (!pilots.Contains(pilot))
            {
                pilots.Add(pilot);
                return string.Format(
                OutputMessages.PilotHired,
                name
                );
            }
            return string.Format(OutputMessages.PilotExists, name);
        }

        public string ManufactureTank(string name, double attackPoints, double defensePoints)
        {
            foreach (var item in machines)
            {
                if (item.Name == name)
                {
                    return string.Format(OutputMessages.MachineExists, name);
                }
            }

            ITank tank = new Tank(name, attackPoints, defensePoints);

            machines.Add(tank);
            tanks.Add(tank);
            return string.Format(
           OutputMessages.TankManufactured,
           name, tank.AttackPoints, tank.DefensePoints);
        }

        public string ManufactureFighter(string name, double attackPoints, double defensePoints)
        {
            foreach (var item in fighters)
            {
                if (item.Name == name)
                {
                    return string.Format(OutputMessages.MachineExists,name);
                }
            }
            IFighter fighter = new Fighter(name, attackPoints, defensePoints);

            machines.Add(fighter);
            fighters.Add(fighter);
            return string
                .Format(OutputMessages
                .FighterManufactured, name, fighter.AttackPoints, fighter.DefensePoints, "ON");
        }

        public string EngageMachine(string selectedPilotName, string selectedMachineName)
        {
            IPilot repot = pilots.FirstOrDefault(x => x.Name == selectedPilotName);
            IMachine machine = machines.FirstOrDefault(x => x.Name == selectedMachineName);
            if (repot == null)
            {
                return string.Format(OutputMessages.PilotNotFound, selectedPilotName);
            }

            if (machine== null)
            {
                return string.Format(OutputMessages.MachineNotFound, selectedMachineName);
            }

            if (machine.Pilot != null)
            {
                return string.Format(OutputMessages.MachineHasPilotAlready, selectedMachineName);
            }
            repot.AddMachine(machine);
            machine.Pilot = repot;
            return string.Format(OutputMessages.MachineEngaged, selectedPilotName, selectedMachineName);
        }

        public string AttackMachines(string attackingMachineName, string defendingMachineName)
        {
            IMachine attackMachine = this.machines.FirstOrDefault(x => x.Name == attackingMachineName);
            IMachine deffMachine = this.machines.FirstOrDefault(x => x.Name == defendingMachineName);

            if (attackMachine == null)
            {
                return string.Format(OutputMessages.MachineNotFound, attackingMachineName);

            }
            if (deffMachine == null)
            {
                return string.Format(OutputMessages.MachineNotFound, defendingMachineName);
            }
            if (attackMachine.HealthPoints <= 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack, attackingMachineName);
            }
            if (deffMachine.HealthPoints <= 0)
            {
                return string.Format(OutputMessages.DeadMachineCannotAttack, defendingMachineName);
            }
            attackMachine.Attack(deffMachine);
            return string.Format(OutputMessages.AttackSuccessful, defendingMachineName, attackingMachineName, deffMachine.HealthPoints);
        }


        public string PilotReport(string pilotReporting)
        {
            IPilot repot = pilots.FirstOrDefault(x => x.Name == pilotReporting);
            if (pilots.Contains(repot))
            {
                return repot.Report();
            }
            return string.Format(OutputMessages.PilotNotFound, pilotReporting);
        }

        public string MachineReport(string machineName)
        {
            IMachine repot = machines.FirstOrDefault(x => x.Name == machineName);
            return repot.ToString();
        }

        public string ToggleFighterAggressiveMode(string fighterName)
        {
            IFighter fighter = fighters.FirstOrDefault(x => x.Name == fighterName);
            if (fighter.Name == fighterName)
            {
                fighter.ToggleAggressiveMode();
                return string.Format(OutputMessages.FighterOperationSuccessful, fighterName);
            }

            return string.Format(OutputMessages.MachineNotFound, fighterName);
        }

        public string ToggleTankDefenseMode(string tankName)
        {
            ITank tank = tanks.FirstOrDefault(x => x.Name == tankName);
            if (tank.Name == tankName)
            {
                tank.ToggleDefenseMode();
                return string.Format(OutputMessages.TankOperationSuccessful, tankName);
            }

            return string.Format(OutputMessages.MachineNotFound, tankName);
        }
    }
}