using SULS.Common;
using SULS.Services.Contarcts;
using SULS.ViewModels.Problems;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class ProblemsController : Controller
    {
        private readonly IProblemsService _problemService;

        public ProblemsController(IProblemsService problemService)
        {
            this._problemService = problemService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateProblemViewModel input)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            if (string.IsNullOrEmpty(input.Name) || input.Name.Length < DataRequierments.ProblemNameMinLegth || input.Name.Length > DataRequierments.ProblemNameMaxLegth)
            {
                return this.Error($"The name of the problem shoud be between {DataRequierments.ProblemNameMinLegth} and {DataRequierments.ProblemNameMaxLegth} chars.");
            }

            if (input.Points < DataRequierments.ProblemPointsMin || input.Points > DataRequierments.ProblemPointsMax)
            {
                return this.Error($"Points are required and shoud be between {DataRequierments.ProblemPointsMin} and {DataRequierments.ProblemPointsMax} points.");
            }

            this._problemService.CreateProblem(input);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            var result = this._problemService.GetProblemDetails(id);

           return this.View(result);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }
            var currentProblem = this._problemService.GetProblemBySubmissionId(id);

            this._problemService.DeleteSubmission(id);

            return this.Redirect("/");
        }
    }
}