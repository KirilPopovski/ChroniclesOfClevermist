namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data;
    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Data.Repositories;
    using ChroniclesOfClevermist.Services.Data.NewsSpace;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class NewsServiceTests
    {
        [Fact]
        public async Task RemoveShouldRemoveOnlyOneNews()
        {
            var repository = new Mock<IDeletableEntityRepository<News>>();
            repository.Setup(r => r.All()).Returns(new List<News>
            {
            new News { Id = "1" },
            new News { Id = "2" },
            }.AsQueryable());

            var service = new NewsService(repository.Object);
            await service.RemoveAsync("1");
            Assert.Equal(2, service.GetCount());
            repository.Verify(x => x.All(), Times.Exactly(2));
        }

        [Fact]
        public async Task AddNewsShoulsAlwaysAddOneNews()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "NewsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.News.Add(new News());
            dbContext.News.Add(new News());
            dbContext.News.Add(new News());
            await dbContext.SaveChangesAsync();

            var repository = new EfDeletableEntityRepository<News>(dbContext);
            var service = new NewsService(repository);
            Assert.Equal(3, service.GetCount());
        }

        [Fact]
        public async Task CreateAsyncShouldCreateOneObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "NewsTestSecond").Options;
            var dbContext = new ApplicationDbContext(options);
            var repository = new EfDeletableEntityRepository<News>(dbContext);
            var service = new NewsService(repository);
            await service.CreateAsync("test", "test", new byte[100]);

            Assert.Equal(1, service.GetCount());
        }
    }
}
