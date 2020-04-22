namespace ChroniclesOfClevermist.Services.Data.NewsSpace
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task CreateAsync(string title, string content, byte[] image)
        {
            var news = new News
            {
                Title = title,
                Content = content,
                Image = image,
            };
            await this.newsRepo.AddAsync(news);
            await this.newsRepo.SaveChangesAsync();
            return;
        }

        public IEnumerable<T> GetAllNews<T>()
        {
            return this.newsRepo.All().To<T>().ToList();
        }

        public async Task RemoveAsync(string id)
        {
            var news = this.newsRepo.All().Where(x => x.Id == id).FirstOrDefault();
            if (news != null)
            {
                this.newsRepo.Delete(news);
                await this.newsRepo.SaveChangesAsync();
            }

            return;
        }

        public int GetCount()
        {
            return this.newsRepo.All().Count();
        }
    }
}
