﻿namespace ChroniclesOfClevermist.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SteamKit2;

    public class ContactsController : Controller
    {
        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult History()
        {
            return this.View();
        }

        public IActionResult SiteMap()
        {
            return this.View();
        }
    }
}
