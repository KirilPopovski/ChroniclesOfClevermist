namespace ChroniclesOfClevermist.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data;
    using ChroniclesOfClevermist.Services.Data.Surveys;
    using ChroniclesOfClevermist.Services.Messaging;
    using ChroniclesOfClevermist.Web.ViewModels.Administration.Dashboard;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ISurveyService surveyService;
        private readonly IEmailSender emailSender;

        public DashboardController(UserManager<ApplicationUser> userManager, ISurveyService surveyService, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.surveyService = surveyService;
            this.emailSender = emailSender;
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Index()
        {
            var model = new IndexViewModel { CountOfUsers = this.userManager.Users.Count() };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> RemoveUser(string email)
        {
            var user = this.userManager.Users.Where(x => x.Email == email).FirstOrDefault();
            if (user != null)
            {
                user.IsDeleted = true;
                await this.userManager.UpdateAsync(user);
            }

            var model = new IndexViewModel
            {
                CountOfUsers = this.userManager.Users.Count(),
            };
            return this.View("Index", model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AddSurvey()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddSurvey(AddSurveyModel input)
        {
            await this.surveyService.AddSurveyAsync(input.Topic);
            this.TempData["Topic"] = input.Topic;
            return this.Redirect($"/Administration/Dashboard/AddQuestionToSurvey");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AddQuestionToSurvey()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddQuestionToSurvey(AddOpinionModel input)
        {
            await this.surveyService.AddQuestionToSurveyAsync(input.Opinion, input.SurveyTopic);
            this.TempData["Topic"] = input.SurveyTopic;
            return this.Redirect("/Administration/Dashboard/AddQuestionToSurvey");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> CompleteSurvey()
        {
            foreach (var user in this.userManager.GetUsersInRoleAsync(GlobalConstants.UserRoleName).Result)
            {
                var callbackUrl = this.HttpContext.Request.GetEncodedUrl();
                await this.emailSender.SendEmailAsync(
                    GlobalConstants.Email,
                    GlobalConstants.CompanyName,
                    user.Email,
                    "New Survey",
                    $"New survey available! You can complete it on your survays tab!");
            }

            return this.Redirect("/Administration/Dashboard/Index");
        }
    }
}
