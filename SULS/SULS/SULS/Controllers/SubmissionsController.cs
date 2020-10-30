using SULS.Common;
using SULS.Services.Contarcts;
using SULS.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(ISubmissionService submissionService)
        {
            this._submissionService = submissionService;
        }

        public HttpResponse Create(string Id)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }
            var problem = this._submissionService.GetProblemForSubmitting(Id);
            return this.View(problem);
        }

        [HttpPost]
        public HttpResponse Create(CreateSubmissonViewModel userInput)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            if (string.IsNullOrEmpty(userInput.Code) || userInput.Code.Length < DataRequierments.CodeMinLength || userInput.Code.Length > DataRequierments.CodeMaxLength)
            {
                return this.Error($"Code is reuired and shoud be between {DataRequierments.CodeMinLength} and {DataRequierments.CodeMaxLength} chars.");
            }

            var currentUserId = this.GetUserId();
            userInput.UserId = currentUserId;
            this._submissionService.AddSubmission(userInput);

            return this.Redirect("/");
        }
    }
}