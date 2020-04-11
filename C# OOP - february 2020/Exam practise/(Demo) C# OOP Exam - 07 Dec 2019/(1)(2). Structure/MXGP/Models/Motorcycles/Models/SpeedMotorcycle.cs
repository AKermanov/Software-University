using MXGP.Models.Motorcycles.Contracts;
using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public class SpeedMotorcycle : Motorcycle, IMotorcycle
    {
        private const int CUBIC_CENTIMETERS = 125;
        public SpeedMotorcycle(string mode, int hp)
            : base(mode, hp, CUBIC_CENTIMETERS)
        {
            ValidateHorsPower(hp);
        }

        protected override int ValidateHorsPower(int amount)
        {
            if (amount >= 50 && amount <= 69)
            {
                return amount;
            }

            throw new ArgumentException(string.Format(ExceptionMessages.InvalidHorsePower, amount));
        }
    }
}
