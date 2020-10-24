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

            var currentUserId = this.GetUserId();
            userInput.UserId = currentUserId;
            this._submissionService.AddSubmission(userInput);

            return this.Redirect("/");
        }
    }
}