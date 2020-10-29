using Git.Data;
using Git.Services;
using Git.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{

    public class CommitsController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly ICommitsService commitsService;

        public CommitsController(IRepositoriesService repositoriesService,ICommitsService commitsService)
        {
            this.repositoriesService = repositoriesService;
            this.commitsService = commitsService;
        }
        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            string userId = this.GetUserId();
            var commits = commitsService.GetUserCommits(userId);
            return this.View(commits);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            var model = new CreateCommitViewModel
            {
                Id = id,
                Name = repositoriesService.GetRepoNameById(id),
            };
            return this.View(model);
        }

        [HttpPost]
        public HttpResponse Create(string id,string description) 
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (description.Length < 5 || string.IsNullOrWhiteSpace(description))
            {
                return this.Error("Description can't be less than 5 symbols!");
            }
            var userId = this.GetUserId();
            ;
            commitsService.CreateCommit(id, userId, description);


            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var userId = this.GetUserId();

            commitsService.Delete(id, userId);

            return this.Redirect("/Commits/All");
        }
    }
}
