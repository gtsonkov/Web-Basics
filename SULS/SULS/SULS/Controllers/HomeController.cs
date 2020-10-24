using SULS.Services.Contarcts;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProblemsService _problemService;

        public HomeController(IProblemsService problemService)
        {
            this._problemService = problemService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.IndexLoggedIn();
            }
            return this.View();
        }

        public HttpResponse IndexLoggedIn()
        {
            if (!this.IsUserSignedIn())
            {
                this.Redirect("/");
            }

            var listProblems = this._problemService.GetAllProblems();

            return this.View(listProblems);
        }
    }
}