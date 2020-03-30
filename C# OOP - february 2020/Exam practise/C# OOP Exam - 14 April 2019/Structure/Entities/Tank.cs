using MortalEngines.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MortalEngines.Entities
{
    public class Tank : BaseMachine, IMachine, ITank
    {
        private const double INITIAL_HEALTH_POINTS = 100;
        private bool defenseMode;
        private const double TANK_ATTACK_DECREESE_BONUS = 40;
        private const double TANK_DEFENCE_INCREASE_BONUS = 30;
        public bool DefenseMode
        {
            get { return defenseMode; }
            set { defenseMode = value; }
        }

        public Tank(string name, double attackPoints, double defensePoints)
            : base(name, INITIAL_HEALTH_POINTS, attackPoints, defensePoints)
        {
            base.AttackPoints -= TANK_ATTACK_DECREESE_BONUS;
            base.DefensePoints += TANK_DEFENCE_INCREASE_BONUS;
            this.DefenseMode = true;
          
        }

        public void ToggleDefenseMode()
        {
            if (this.DefenseMode == true)
            {
                this.DefenseMode = false;
               base.AttackPoints += 40;
                base.DefensePoints -= 30;
            }
            else if (this.DefenseMode == false)
            {
                this.DefenseMode = true;
                base.AttackPoints -= 40;
                base.DefensePoints += 30;
            }
        }
        public override string ToString()
        {
            var defanceMode = this.DefenseMode == true ? "ON" : "OFF";
            return base.ToString() + $" *Defense: {defanceMode}";       
        }
    }

}
