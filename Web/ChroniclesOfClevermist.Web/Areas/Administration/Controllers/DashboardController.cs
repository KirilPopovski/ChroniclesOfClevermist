namespace ChroniclesOfClevermist.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data;
    using ChroniclesOfClevermist.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;

        public DashboardController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Index()
        {
            var model = new IndexViewModel { CountOfUsers = this.userManager.Users.Count() };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> RemoveUser(string email)
        {
            var user = this.userManager.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                user.IsDeleted = true;
                await this.userManager.UpdateAsync(user);
            }

            var model = new IndexViewModel
            {
                CountOfUsers = this.userManager.Users.Count(),
            };
            return this.View("Index", model);
        }
    }
}
