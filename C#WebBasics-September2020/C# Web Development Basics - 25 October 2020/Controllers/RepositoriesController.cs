using Git.Services;
using Git.ViewModels.Repository;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoryService;

        public RepositoriesController(IRepositoriesService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repos = this.repositoryService.GetAll();
            return this.View(repos);
        }
        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel input)
        {
            
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(input.Name))
            {
                return this.Error("Name is required.");
            }

            if (input.Name.Length < 3 || input.Name.Length > 10)
            {
                return this.Error("Name should be between 3 and 10.");
            }
            
            this.repositoryService.Add(input);
            return this.Redirect("/Repositories/All");
        }
    }
}
