namespace ChroniclesOfClevermist.Web.Controllers
{
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers;
    using ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers;
    using Microsoft.AspNetCore.Mvc;

    public class QuestionsAndAnswersController : Controller
    {
        private readonly IQuestionsAndAnswersService service;

        public QuestionsAndAnswersController(IQuestionsAndAnswersService service)
        {
            this.service = service;
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
                QuestionText = question,
                Answers = answers,
            };
            return this.View(model);
        }
    }
}
