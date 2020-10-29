using Git.Services;
using Git.ViewModels.InputModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }
        public HttpResponse All()
        {
            var repositories = repositoriesService.GetAll();
            return this.View(repositories);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            return this.View();
        }
        [HttpPost]
        public HttpResponse Create(RepositoryInputModel repositoryInput)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (repositoryInput.Name.Length < 3 || repositoryInput.Name.Length > 10 || string.IsNullOrWhiteSpace(repositoryInput.Name))
            {
                return this.Error("Repository name should be between 3 and 10 symbols!");
            }

            if (string.IsNullOrWhiteSpace(repositoryInput.RepositoryType))
            {
                return this.Error("Repository type is required!");
            }

            repositoriesService.Create(repositoryInput.Name, repositoryInput.RepositoryType, this.GetUserId());

            return this.Redirect("/Repositories/All");
        }


    }
}
