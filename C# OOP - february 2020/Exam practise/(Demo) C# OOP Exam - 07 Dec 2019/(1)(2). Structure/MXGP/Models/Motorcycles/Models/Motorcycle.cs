using MXGP.Models.Motorcycles.Contracts;
using MXGP.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace MXGP.Models.Motorcycles.Models
{
    public abstract class Motorcycle : IMotorcycle
    {
        private const int MINIMUM_MODEL_SYMBOLS = 4;
        private string model;
        private int horsePower;

        public Motorcycle(string mode, int hp, double cumCm)
        {
            this.Model = mode;
            this.horsePower = hp;
            this.CubicCentimeters = cumCm;

        }
        public string Model 
        {
            get => this.model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < MINIMUM_MODEL_SYMBOLS)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidModel, value, MINIMUM_MODEL_SYMBOLS));
                }
                this.model = value;
            }
        }

        public int HorsePower
        {
            get => this.horsePower;
            private set
            {
               this.horsePower = value;
            }
        }

        public double CubicCentimeters { get; }

        public double CalculateRacePoints(int laps)
        {
            double tot = CubicCentimeters / HorsePower * laps;
            return tot;
        }

        protected abstract int ValidateHorsPower(int value);
    }
}
