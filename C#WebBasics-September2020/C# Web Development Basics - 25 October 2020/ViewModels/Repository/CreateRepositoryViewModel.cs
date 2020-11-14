using Git.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.ViewModels.Repository
{
   public class CreateRepositoryViewModel
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Owner { get; set; }
    }
}
