namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers;
    using ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionsAndAnswersController : Controller
    {
        private readonly IQuestionsAndAnswersService service;
        private readonly UserManager<ApplicationUser> userManager;

        public QuestionsAndAnswersController(IQuestionsAndAnswersService service, UserManager<ApplicationUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var questions = this.service.GetMostPopularQuestions<QuestionViewModel>();
            foreach (var question in questions)
            {
                if (question.Text.Length > 50)
                {
                    question.Text = question.Text.Substring(0, 50) + "...";
                }
            }

            var model = new AllQuestionsViewModel { Questions = questions };
            return this.View(model);
        }

        public IActionResult Details(string id)
        {
            var question = this.service.GetQuestionText(id);
            var answers = this.service.GetAllAnswers<AnswerViewModel>(id);
            var model = new AllAnswersViewModel
            {
                QuestionId = id,
                QuestionText = question,
                Answers = answers,
            };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AddQuestion()
        {
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddQuestion(QuestionInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var email = user.Email;
            await this.service.AddQuestionAsync(input.Text, email);
            return this.Redirect("/QuestionsAndAnswers/All");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult AddAnswer(string id)
        {
            this.TempData["QuestionId"] = id;
            return this.View();
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        [HttpPost]
        public async Task<IActionResult> AddAnswer(AnswerInputModel input)
        {
            await this.service.AddAnswerAsync(input.QuestionId, input.Text);
            return this.Redirect("/QuestionsAndAnswers/All");
        }
    }
}
