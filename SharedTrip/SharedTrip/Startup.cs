using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SharedTrip.Services;
using SharedTrip.Services.Contracts;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SharedTrip
{
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUserService, UserService>();
        }
    }
}