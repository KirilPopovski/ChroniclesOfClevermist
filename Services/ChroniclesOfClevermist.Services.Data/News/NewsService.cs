namespace ChroniclesOfClevermist.Services.Data.News
{
    using System.Collections.Generic;
    using System.Linq;

    using ChroniclesOfClevermist.Data.Common.Repositories;

    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Mapping;

    public class NewsService : INewsService
    {
        private readonly IDeletableEntityRepository<News> newsRepo;

        public NewsService(IDeletableEntityRepository<News> newsRepo)
        {
            this.newsRepo = newsRepo;
        }

        public IEnumerable<T> GetAllNews<T>()
        {
            return this.newsRepo.All().To<T>().ToList();
        }
    }
}
