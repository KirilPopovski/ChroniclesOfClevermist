namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data;
    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Data.Repositories;
    using ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers;
    using ChroniclesOfClevermist.Web.Controllers;
    using ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class QAndAControllerTests
    {
        public static UserManager<TUser> TestUserManager<TUser>(IUserStore<TUser> store = null)
        where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<TUser>>();
            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());
            var userManager = new UserManager<TUser>(
                store,
                options.Object,
                new PasswordHasher<TUser>(),
                userValidators,
                pwdValidators,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                new Mock<ILogger<UserManager<TUser>>>().Object);
            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();
            return userManager;
        }

        [Fact]
        public async Task TestAddQuestion()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "QAndATest").Options;
            var dbContext = new ApplicationDbContext(options);
            var questionsRepo = new EfDeletableEntityRepository<Question>(dbContext);
            await questionsRepo.AddAsync(new Question { Id = "1", Text = "test" });
            var answersRepo = new EfDeletableEntityRepository<Answer>(dbContext);
            await answersRepo.AddAsync(new Answer { QuestionId = "1", Id = "2", Text = "testanswer" });
            var userManager = TestUserManager<ApplicationUser>();
            var service = new QuestionsAndAnswersService(questionsRepo, answersRepo);
            var controller = new QuestionsAndAnswersController(service, userManager);
            var userMock = new Mock<ClaimsPrincipal>();
            var controllerContextMock = new ControllerContext { HttpContext = new DefaultHttpContext { User = userMock.Object }, };
            controller.ControllerContext = controllerContextMock;

            var result = controller.AddQuestion();
            Assert.IsType<ViewResult>(result);
        }
    }
}
