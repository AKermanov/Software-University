using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext database;

        public RepositoriesService(ApplicationDbContext database)
        {
            this.database = database;
        }
        public void Create(string name, string repositoryType, string ownerId)
        {
            database.Repositories.Add(new Repository
            {
                Name = name,
                IsPublic = repositoryType == "Public" ? true : false,
                OwnerId = ownerId,
                CreatedOn = DateTime.UtcNow,
            });

            database.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            return this.database
                .Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn,
                    CommitsCount = x.Commits.Count,
                })
                .ToList();
        }

        public string GetRepoNameById(string id)
        {
            var repo = database.Repositories.FirstOrDefault(x => x.Id == id);

            return repo.Name;
        }
    }
}
