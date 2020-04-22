namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data.NewsSpace;
    using ChroniclesOfClevermist.Services.Messaging;
    using ChroniclesOfClevermist.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly INewsService newsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailSender emailSender;

        public NewsController(INewsService newsService, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            this.newsService = newsService;
            this.userManager = userManager;
            this.emailSender = emailSender;
        }

        public IActionResult All()
        {
            bool isSubscribed = false;
            if (this.User.IsInRole(GlobalConstants.UserRoleName))
            {
                isSubscribed = this.userManager.GetUserAsync(this.User).Result.IsSubscribedToNewsletter;
            }

            var news = this.newsService.GetAllNews<NewsDetailsViewModel>().Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn);
            foreach (var item in news)
            {
                if (item.Content.Length > 50)
                {
                    item.Content = item.Content.Substring(0, 50) + "...";
                }
            }

            var model = new AllNewsViewModel
            {
                News = news,
                IsSubscribed = isSubscribed,
            };
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = this.newsService.GetAllNews<NewsDetailsViewModel>().Where(x => x.Id == id).FirstOrDefault();
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Add()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> Add(NewsInputViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            byte[] image = new byte[input.Image.Length];
            await input.Image.OpenReadStream().ReadAsync(image, 0, image.Length);
            await this.newsService.CreateAsync(input.Title, input.Content, image);
            foreach (var user in this.userManager.GetUsersInRoleAsync(GlobalConstants.UserRoleName).Result.Where(x => x.IsSubscribedToNewsletter == true))
            {
                await this.emailSender.SendEmailAsync(
                    GlobalConstants.Email,
                    GlobalConstants.CompanyName,
                    user.Email,
                    input.Title,
                    input.Content);
            }

            return this.Redirect("/News/All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.newsService.RemoveAsync(id);
            return this.Redirect("/News/All");
        }

        [Authorize]
        public async Task<IActionResult> SubscribeToNewsletter()
        {
            this.userManager.GetUserAsync(this.User).Result.IsSubscribedToNewsletter = !this.userManager.GetUserAsync(this.User).Result.IsSubscribedToNewsletter;
            await this.userManager.UpdateAsync(this.userManager.GetUserAsync(this.User).Result);
            return this.Redirect("/News/All");
        }
    }
}
