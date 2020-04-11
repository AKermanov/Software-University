using MXGP.Models.Motorcycles.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    class MotorcycleRepository : IRepository<IMotorcycle>
    {
        private readonly ICollection<IMotorcycle> models;

        public MotorcycleRepository()
        {
            this.models = new List<IMotorcycle>();
        }

        //public IReadOnlyCollection<IMotorcycle> Models => this.models.ToList().AsReadOnly();
        public void Add(IMotorcycle model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<IMotorcycle> GetAll()
        {
           return this.models.ToList();
        }

        public IMotorcycle GetByName(string name)
        {
            IMotorcycle motorByName = this.models.FirstOrDefault(x => x.Model == name);

            return motorByName;
        }

        public bool Remove(IMotorcycle model)
        {
           return this.models.Remove(model);
        }
    }
}
