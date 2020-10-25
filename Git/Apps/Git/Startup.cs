using Git.Data;
using Git.Services.CommitServices;
using Git.Services.Contracts;
using Git.Services.RepositorieServices;
using Git.Services.UserServices;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Collections.Generic;

namespace Git
{
    public class Startup : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UserService>();
            serviceCollection.Add<IRepositoryService, RepositoryService>();
            serviceCollection.Add<ICommitService, CommitService>();
        }
    }
}
