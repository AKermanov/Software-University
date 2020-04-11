using MXGP.Core.Contracts;
using MXGP.Models.Motorcycles.Contracts;
using MXGP.Models.Races.Contracts;
using MXGP.Models.Races.Models;
using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Contracts;
using MXGP.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MXGP.Utilities.Messages;
using MXGP.Models.Motorcycles.Models;
using MXGP.Models.Races;

namespace MXGP.Core.Models
{
  public class ChampionshipController : IChampionshipController
    {
        private IRepository<IRider> riderRepository;
        private IRepository<IRace> raceRepository;
        private IRepository<IMotorcycle> motoRepository;

        public ChampionshipController()
        {
            this.riderRepository = new RiderRepository();
            this.raceRepository = new RaceRepository();
            this.motoRepository = new MotorcycleRepository();
        }
        public string AddMotorcycleToRider(string riderName, string motorcycleModel)
        {
            var rider = this.riderRepository.GetByName(riderName);
            if (rider == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RiderNotFound, riderName));
            }

            var moto = this.motoRepository.GetByName(motorcycleModel);
            if (moto == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.MotorcycleNotFound, motorcycleModel));
            }

            rider.AddMotorcycle(moto);

            return string.Format(OutputMessages.MotorcycleAdded, riderName, motorcycleModel);
        }

        public string AddRiderToRace(string raceName, string riderName)
        {
            var race = this.raceRepository.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            var rider = this.riderRepository.GetByName(riderName);
            if (rider == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RiderNotFound, riderName));
            }

            race.AddRider(rider);

            return string.Format(OutputMessages.RiderAdded, riderName, raceName);
        }

        public string CreateMotorcycle(string type, string model, int horsePower)
        {
            IMotorcycle moto = null;
            if (type == "Speed")
            {
                moto = new SpeedMotorcycle(model, horsePower);
            }
            else if (type == "Power")
            {
                moto = new PowerMotorcycle(model, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.MotorcycleInvalid);
            }

            var getAll = this.motoRepository.GetByName(model);

            if (getAll != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.MotorcycleExists, model));
            }

            this.motoRepository.Add(moto);
            return string.Format(OutputMessages.MotorcycleCreated, moto.GetType().Name, model);
        }

        public string CreateRace(string name, int laps)
        {
            IRace race = new Race(name, laps);
            var exist = this.raceRepository.GetByName(name);

            if (exist != null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, name));
            }

            this.raceRepository.Add(race);

            return string.Format(OutputMessages.RaceCreated, name);
        }

        public string CreateRider(string riderName)
        {
            IRider rider = new Rider(riderName);

            var riderExist = this.riderRepository.GetByName(riderName);
            if (riderExist != null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RiderExists, riderName));
            }

            this.riderRepository.Add(rider);
            return string.Format(OutputMessages.RiderCreated, riderName);
        }

        public string StartRace(string raceName)
        {
            var race = this.raceRepository.GetByName(raceName);
            if (race == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceNotFound, raceName));
            }

            if (race.Riders.Count< 3)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceInvalid, raceName, 3));
            }

            var ridersInDescending = race.Riders.ToList();
            var sortedRider = new Dictionary<double, IRider>();

            foreach (var rider in ridersInDescending)
            {
               double sum= rider.Motorcycle.CalculateRacePoints(race.Laps);
               sortedRider[sum] = rider;
            }

            var sb = new StringBuilder();
            int count = 0;
            foreach (var item in sortedRider.OrderByDescending(x => x.Key))
            {
                if (count == 0)
                {
                    sb.AppendLine(string.Format(OutputMessages.RiderFirstPosition, item.Value.Name, race.Name));
                    item.Value.WinRace();
                }
                else if (count == 1)
                {
                    sb.AppendLine(string.Format(OutputMessages.RiderSecondPosition, item.Value.Name, race.Name));
                }

                else if (count == 2)
                {
                    sb.AppendLine(string.Format(OutputMessages.RiderThirdPosition, item.Value.Name, race.Name));
                    break;
                }
                count++;
            }

            this.raceRepository.Remove(race);
            return sb.ToString().TrimEnd();
        }
    }
}
