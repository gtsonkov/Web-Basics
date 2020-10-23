using System.Collections.Generic;
using BattleCards.Data;
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
            
        }
    }
}