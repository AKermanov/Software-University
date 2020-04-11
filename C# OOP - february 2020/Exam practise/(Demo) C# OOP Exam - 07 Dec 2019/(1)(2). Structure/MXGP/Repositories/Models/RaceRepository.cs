using MXGP.Models.Races.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    class RaceRepository : IRepository<IRace>
    {
        private readonly ICollection<IRace> models;

        public RaceRepository()
        {
            this.models = new List<IRace>();
        }

        //public IReadOnlyCollection<IMotorcycle> Models => this.models.ToList().AsReadOnly();
        public void Add(IRace model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.models.ToList();
        }

        public IRace GetByName(string name)
        {
            IRace motorByName = this.models.FirstOrDefault(x => x.Name == name);

            return motorByName;
        }

        public bool Remove(IRace model)
        {
            return this.models.Remove(model);
        }
    }
}
