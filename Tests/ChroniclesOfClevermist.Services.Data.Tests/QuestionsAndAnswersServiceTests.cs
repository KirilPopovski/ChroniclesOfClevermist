namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Data.Repositories;
    using ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class QuestionsAndAnswersServiceTests
    {
        [Fact]
        public async Task QAndA()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "QAndATest").Options;
            var dbContext = new ApplicationDbContext(options);
            var questionsRepo = new EfDeletableEntityRepository<Question>(dbContext);
            var answersRepo = new EfDeletableEntityRepository<Answer>(dbContext);
            var service = new QuestionsAndAnswersService(questionsRepo, answersRepo);
            await service.AddQuestionAsync("test", "test@softuni.bg");

            Assert.Single(dbContext.Questions);
            dbContext.Questions.Add(new Question { Id = "testTwo", Text = "test", Email = "sth@abv.bg" });
            await service.AddAnswerAsync("testTwo", "firstAnswer");
            Assert.Equal("test", service.GetQuestionText("testTwo"));
        }
    }
}
