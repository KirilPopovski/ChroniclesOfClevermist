namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data;
    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Data.Repositories;
    using ChroniclesOfClevermist.Services.Data.Surveys;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class SurveyServiceTests
    {
        [Fact]
        public async Task AddSurveyShouldAddOneObject()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SurveyTest").Options;
            var dbContext = new ApplicationDbContext(options);
            var surveyRepo = new EfDeletableEntityRepository<Survey>(dbContext);
            var opinionsRepo = new EfDeletableEntityRepository<Opinion>(dbContext);
            var userOpRepo = new EfDeletableEntityRepository<UserOpinion>(dbContext);
            var usersSurveys = new EfRepository<UserSurvey>(dbContext);
            var service = new SurveyService(surveyRepo, opinionsRepo, userOpRepo, usersSurveys);
            dbContext.Users.Add(new ApplicationUser { Id = "123" });
            await service.AddSurveyAsync("test");
            await service.AddQuestionToSurveyAsync("testQuestion", "test");
            await service.AddOpinionToQuestionAsync("testQuestion", "testAnswer");

            Assert.Equal(1, await dbContext.Surveys.CountAsync());
            Assert.Single(service.GetAllQuestions("test"));
            Assert.Equal(1, await dbContext.UserOpinions.CountAsync());
            Assert.Single(service.GetAll("123"));
            await service.AddSurveyToUser("123", "test");
            Assert.Empty(service.GetAll("123"));
        }
    }
}
