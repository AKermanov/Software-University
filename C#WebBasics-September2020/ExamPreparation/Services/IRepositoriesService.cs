using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        public void Create(string name, string repositoryType, string ownerId);

        public IEnumerable<RepositoryViewModel> GetAll();

        public string GetRepoNameById(string id);
        
    }
}
