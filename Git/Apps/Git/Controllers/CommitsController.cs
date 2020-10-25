using Git.Common;
using Git.Services.Contracts;
using Git.ViewModels.CommitViewModels;
using SUS.HTTP;
using SUS.MvcFramework;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitService _commitService;

        public CommitsController(ICommitService commitService)
        {
            this._commitService = commitService;
        }

        public HttpResponse All(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var currRepo = this._commitService.GetAllCommits(id, this.GetUserId());

            return this.View(currRepo);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            var currRepoInfo = this._commitService.GetRepositoryInfo(id);

            return this.View(currRepoInfo);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitUserInputViewModel userInput)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }

            if (string.IsNullOrEmpty(userInput.Description) || userInput.Description.Length < DataRequiermentsConst.CommitDescriptionMinLength)
            {
                return this.Error($"Description text is reuired and shoud be minimum {DataRequiermentsConst.CommitDescriptionMinLength} digits");
            }

            var commitData = new CreateCommitViewModel
            {
                UserId = this.GetUserId(),
                Description = userInput.Description,
                RepoId = userInput.Id
            };

            this._commitService.AddCommit(commitData);
            return this.All(userInput.Id);
        }

        public HttpResponse Delete(string id)
        {
            string userId = this.GetUserId();

            this._commitService.DeleteCommit(id, userId);

            return this.Redirect("/");
        }
    }
}