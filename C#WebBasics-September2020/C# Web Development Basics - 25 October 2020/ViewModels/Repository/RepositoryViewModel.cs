using Git.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Repository
{
   public class RepositoryViewModel
    {
        public string Name { get; set; }

        public string Owner { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommintsCount { get; set; }

        public List<Commit> Commit { get; set; }
    }
}
