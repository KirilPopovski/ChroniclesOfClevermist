namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Services.Data.News;
    using ChroniclesOfClevermist.Web.ViewModels.News;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class NewsController : Controller
    {
        private readonly INewsService newsService;

        public NewsController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IActionResult All()
        {
            var news = this.newsService.GetAllNews<NewsDetailsViewModel>().Where(x => x.IsDeleted == false).OrderByDescending(x => x.CreatedOn);
            foreach (var item in news)
            {
                if (item.Content.Length > 50)
                {
                    item.Content = item.Content.Substring(0, 50) + "...";
                }
            }

            var model = new AllNewsViewModel { News = news };
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
            return this.Redirect("/News/All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.newsService.RemoveAsync(id);
            return this.Redirect("/News/All");
        }
    }
}
