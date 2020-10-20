using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip.App.Controllers
{
    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}