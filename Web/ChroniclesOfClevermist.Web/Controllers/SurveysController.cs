namespace ChroniclesOfClevermist.Web.Controllers
{
    using System.Collections.Generic;

    using ChroniclesOfClevermist.Common;
    using ChroniclesOfClevermist.Services.Data.Surveys;
    using ChroniclesOfClevermist.Web.ViewModels.Surveys;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SurveysController : Controller
    {
        private readonly ISurveyService surveyService;

        public SurveysController(ISurveyService surveyService)
        {
            this.surveyService = surveyService;
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult All()
        {
            var model = new AllSurveysViewModel { Topics = this.surveyService.GetAll() };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        public IActionResult Fill(string topic)
        {
            var questions = new Dictionary<string, string>();
            foreach (var question in this.surveyService.GetAllQuestions(topic))
            {
                questions[question] = string.Empty;
            }

            var model = new AllQuestionsViewModel { Questions = questions };
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.UserRoleName)]
        [HttpPost]
        public IActionResult Fill(AllQuestionsViewModel input)
        {
            return this.Redirect("/Surveys/All");
        }
    }
}
