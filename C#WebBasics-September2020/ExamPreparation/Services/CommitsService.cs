using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext database;

        public CommitsService(ApplicationDbContext database)
        {
            this.database = database;
        }
        public void CreateCommit(string repositoryId, string creatorId, string description)
        {
            database.Commits.Add(new Commit
            {
                CreatorId = creatorId,
                RepositoryId = repositoryId,
                Description = description,
                CreatedOn = DateTime.UtcNow,
            });

            database.SaveChanges();
        }

        public void Delete(string commitId, string creatorId)
        {
            var commit = database
                .Commits
                .FirstOrDefault(x => x.Id == commitId && x.CreatorId == creatorId);

            if (commit == null) return;

            database.Commits.Remove(commit);
            database.SaveChanges();
        }

        public IEnumerable<CommitViewModel> GetUserCommits(string userId)
        {
            
            return database
                .Commits
                .Where(x => x.CreatorId == userId)
                .Select(x => new CommitViewModel
                {
                    Id = x.Id,
                    Repository = x.Repository.Name,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                })
                .ToList();
        }
    }
}
