using Git.Common;
using Git.Services.Contracts;
using Git.ViewModels.RepositoryViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class RepositoriesController : Controller

    {
        private readonly IRepositoryService _repoService;

        public RepositoriesController(IRepositoryService repoService)
        {
            this._repoService = repoService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var listOfPublicRepost = this._repoService.GetAllRepositories();

            return this.View(listOfPublicRepost);
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
        public HttpResponse Create(string name, string repositoryType)
        {
            var userInput = new RepositoryCreateViewModel
            {
                Name = name,
                RepositoryType = repositoryType,
                OwnerId = this.GetUserId()
            };

            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(userInput.Name) || userInput.Name.Length < DataRequiermentsConst.RepositoryNameMinLength || userInput.Name.Length > DataRequiermentsConst.RepositoryNameMaxLength)
            {
                return this.Error($"Name is required and shoud be between {DataRequiermentsConst.RepositoryNameMinLength} and {DataRequiermentsConst.RepositoryNameMaxLength} chars.");
            }

            if (string.IsNullOrEmpty(userInput.RepositoryType))
            {
                return this.Error(ErrorMessages.EmptyRepoType);
            }

            this._repoService.CreateRepository(userInput);

            return this.Redirect("/");
        }
    }
}