using Git.Data;
using Git.ViewModels.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;
        private readonly User user;

        public RepositoriesService(ApplicationDbContext db, User user)
        {
            this.db = db;
            this.user = user;
        }

        public void Add(CreateRepositoryViewModel repo)
        {
            bool isPb = false;

            if (repo.Type == "Public")
            {
                isPb = true;
            }

            var dbRepo = new Repository
            {
                Name = repo.Name,
                CreatedOn = DateTime.Now,
                IsPublic = isPb,
                OwnerId = user.Id
            };

            this.db.Repositories.Add(dbRepo);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            var repositories = this.db.Repositories.Select(x => new RepositoryViewModel
            {
                Name = x.Name,
                Owner = x.Owner.Username,
                CreatedOn = DateTime.Now,
                CommintsCount = x.Commits.Select(y => y.CreatorId == x.Id).Count(),
                Commit = x.Commits.ToList()
            }).ToList();

            return repositories;
        }
    }
}
