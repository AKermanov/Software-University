using MXGP.Models.Motorcycles.Contracts;
using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class PowerMotorcycle : Motorcycle, IMotorcycle
    {
        private const int CUBIC_CENTIMETERS = 450;
        public PowerMotorcycle(string mode, int hp) 
            : base(mode, hp, CUBIC_CENTIMETERS)
        {
            ValidateHorsPower(hp);
        }

        protected override int ValidateHorsPower(int amount)
        {
            if (amount >= 70 && amount <= 100)
            {
                return amount;
            }

           throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, amount));
        }
    }
}
