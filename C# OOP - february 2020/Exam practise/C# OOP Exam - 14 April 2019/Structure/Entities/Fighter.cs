using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Fighter : BaseMachine, IMachine, IFighter
    {
        private bool agressiveMode;
        private const double INITIAL_HEALTH_POINTS = 200;
        private const double BONUS_AGGRESSIVE_POINTS = 50;
        private const double BONUS_DEFENCE = 25;
        public Fighter(string name, double attackPoints, double defensePoints)
            : base(name, INITIAL_HEALTH_POINTS, attackPoints, defensePoints)
        {
            this.AttackPoints += BONUS_AGGRESSIVE_POINTS;
            this.DefensePoints -= BONUS_DEFENCE;
            this.AggressiveMode = true;

        }
        public bool AggressiveMode
        {
            get => this.agressiveMode;
            private set
            {
                this.agressiveMode = value;
            }
        }
        public void ToggleAggressiveMode()
        {
            if (this.AggressiveMode == true)
            {
                this.AggressiveMode = false;
                this.AttackPoints -= 50;
                this.DefensePoints += 25;
            }
            else if (this.AggressiveMode == false)
            {
                this.AggressiveMode = true;
                this.AttackPoints += 50;
                this.DefensePoints -= 25;
            }
        }

        public override string ToString()
        {
            var aggroMode = this.AggressiveMode == true ? "ON" : "OFF";
            return base.ToString() + $" *Aggressive: {aggroMode}";
        }
    }
}
