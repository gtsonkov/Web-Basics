using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;

namespace SULS.App
{
    public class StartUp : IMvcApplication
    {
        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }
    }
}