using System.Collections.Generic;
using BattleCards.Data;
using BattleCards.Services.Contracts;
using BattleCards.Services.Users;
using Microsoft.EntityFrameworkCore;
using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards
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