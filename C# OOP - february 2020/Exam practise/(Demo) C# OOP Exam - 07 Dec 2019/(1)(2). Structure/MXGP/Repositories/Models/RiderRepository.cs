using MXGP.Models.Riders.Contracts;
using MXGP.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MXGP.Repositories.Models
{
    class RiderRepository : IRepository<IRider>
    {
        private readonly ICollection<IRider> models;

        public RiderRepository()
        {
            this.models = new List<IRider>();
        }

        //public IReadOnlyCollection<IMotorcycle> Models => this.models.ToList().AsReadOnly();
        public void Add(IRider model)
        {
            this.models.Add(model);
        }

        public IReadOnlyCollection<IRider> GetAll()
        {
            IReadOnlyCollection<IRider> collection = this.models.ToList();
            return collection;
        }

        public IRider GetByName(string name)
        {
            IRider motorByName = this.models.FirstOrDefault(x => x.Name == name);

            return motorByName;
        }

        public bool Remove(IRider model)
        {
            return this.models.Remove(model);
        }
    }
}
