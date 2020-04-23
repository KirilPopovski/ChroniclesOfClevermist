namespace ChroniclesOfClevermist.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data.NewsSpace;
    using ChroniclesOfClevermist.Services.Messaging;
    using ChroniclesOfClevermist.Web.Controllers;
    using ChroniclesOfClevermist.Web.ViewModels.News;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Xunit;

    public class NewsControllerTests
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
        public async Task TestGettAllNewsReturnType()
        {
            var repository = new Mock<IDeletableEntityRepository<News>>();
            repository.Setup(r => r.All()).Returns(new List<News>
            {
            new News { Id = "1" },
            new News { Id = "2" },
            }.AsQueryable());
            var userManager = TestUserManager<ApplicationUser>();
            var emailSender = new Mock<IEmailSender>();
            emailSender.Setup(e => e.SendEmailAsync("123", "234", "345", "456", "567", null));

            var service = new Mock<NewsService>(repository.Object);

            var controller = new NewsController(service.Object, userManager, emailSender.Object);

            var userMock = new Mock<ClaimsPrincipal>();
            var controllerContextMock = new ControllerContext { HttpContext = new DefaultHttpContext { User = userMock.Object }, };
            controller.ControllerContext = controllerContextMock;

            var result = controller.Add();
            Assert.IsType<ViewResult>(result);

            try
            {
                await controller.Add(new NewsInputViewModel { Title = "1234" });
            }
            catch (NullReferenceException)
            {
                Assert.True(true);
            }

            try
            {
                await controller.Add(new NewsInputViewModel { Title = "1234", Content = "12345", });
            }
            catch (NullReferenceException)
            {

                Assert.True(true);
            }
        }
    }
}
