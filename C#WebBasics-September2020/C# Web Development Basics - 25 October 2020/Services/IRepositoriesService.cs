using Git.ViewModels.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        void Add(CreateRepositoryViewModel repo);
        IEnumerable<RepositoryViewModel> GetAll();
    }
}
