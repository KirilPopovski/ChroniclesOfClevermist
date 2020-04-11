namespace ChroniclesOfClevermist.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

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
