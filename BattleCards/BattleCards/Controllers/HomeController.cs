﻿using SIS.HTTP;
using SIS.MvcFramework;

namespace BattleCards.Controllers
{
    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                return this.Redirect("Cards/All");
            }

            return this.View();
        }
    }
}