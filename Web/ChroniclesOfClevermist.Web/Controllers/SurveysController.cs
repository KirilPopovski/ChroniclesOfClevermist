namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data.Surveys;
    using ChroniclesOfClevermist.Web.ViewModels.Surveys;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SurveysController : Controller
    {
        private readonly ISurveyService surveyService;
        private readonly UserManager<ApplicationUser> userManager;

        public SurveysController(ISurveyService surveyService, UserManager<ApplicationUser> userManager)
        {
            this.surveyService = surveyService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            var topics = this.surveyService.GetAll(this.userManager.GetUserId(this.User));

            var model = new AllSurveysViewModel { Topics = topics };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult Fill(string topic)
        {
            var questions = new List<QuestionViewModel>();
            foreach (var question in this.surveyService.GetAllQuestions(topic))
            {
                questions.Add(new QuestionViewModel { Question = question, Answer = string.Empty });
            }

            var model = new AllQuestionsViewModel { Questions = questions, Topic = topic };
            this.TempData["topic"] = topic;
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        [HttpPost]
        public async Task<IActionResult> Fill(AllQuestionsViewModel input)
        {
            foreach (var question in input.Questions)
            {
                await this.surveyService.AddOpinionToQuestionAsync(question.Question, question.Answer);
            }

            await this.surveyService.AddSurveyToUser(this.userManager.GetUserId(this.User), input.Topic);
            return this.Redirect("/Surveys/All");
        }
    }
}
