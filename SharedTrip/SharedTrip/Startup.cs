using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SharedTrip.Services;
using SharedTrip.Services.Contracts;
using SIS.HTTP;
using SIS.MvcFramework;

namespace SharedTrip
{
    public class Startup : IMvcApplication
    {
        public void Configure(IList<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
        }
    }
}