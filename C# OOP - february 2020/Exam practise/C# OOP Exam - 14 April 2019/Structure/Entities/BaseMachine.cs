using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public abstract class BaseMachine : IMachine
    {
        private string name;
        private IPilot pilot;

        protected BaseMachine(string name, double healthPoints,
            double attackPoints, double defensePoints)
        {
            this.Name = name;
            this.AttackPoints = attackPoints;
            this.DefensePoints = defensePoints;
            this.HealthPoints = healthPoints;
            this.Targets = new List<string>();
        }
       
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Machine name cannot be null or empty.");
                }
                this.name = value;
            }
        }
        public double HealthPoints  { get; set; }
        public double AttackPoints { get; protected set; } 

        public double DefensePoints { get; protected set; }
        public IPilot Pilot
        {
            get => this.pilot;
           set
            {
                this.pilot = value ?? throw new NullReferenceException("Pilot cannot be null.");
            }
        }
        public IList<string> Targets { get; }

        public void Attack(IMachine target)
        {
            if (target == null)
            {
                throw new NullReferenceException("Target cannot be null");
            }
            var decreseHealth = this.AttackPoints - target.DefensePoints;
            if (decreseHealth > 0)
            {
                target.HealthPoints -= decreseHealth;
            }

            if (target.HealthPoints < 0)
            {
                target.HealthPoints = 0;
            }

            this.Targets.Add(target.Name);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"- {this.Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Health: {this.HealthPoints:f2}");
            sb.AppendLine($" *Attack: {this.AttackPoints:f2}");
            sb.AppendLine($" *Defense: {this.DefensePoints:f2}");
            var targets = Targets.Count == 0 ? "None" : string.Join(",", this.Targets);
            sb.AppendLine($" *Targets: {targets}");
            return sb.ToString()+Environment.NewLine.TrimEnd(); 
        }
    }
}
