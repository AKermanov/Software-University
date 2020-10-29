using Git.Data;
using Git.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        public void CreateCommit(string repositoryId,string creatorId,string description);

        public IEnumerable<CommitViewModel> GetUserCommits(string creatorId);

        public void Delete(string commitId,string creatorId);
    }
}
