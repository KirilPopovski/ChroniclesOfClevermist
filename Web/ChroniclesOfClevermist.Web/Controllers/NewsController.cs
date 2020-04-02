namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Linq;

    using ChroniclesOfClevermist.Services.Data.News;
    using ChroniclesOfClevermist.Web.ViewModels.News;
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
            var news = this.newsService.GetAllNews<NewsDetailsViewModel>();
            var model = new AllNewsViewModel { News = news };
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var model = this.newsService.GetAllNews<NewsDetailsViewModel>().Where(x => x.Id == id).FirstOrDefault();
            return this.View(model);
        }
    }
}
