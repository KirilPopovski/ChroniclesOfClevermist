namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    using System.Collections.Generic;

    public class AllAnswersViewModel
    {
        public string QuestionId { get; set; }

        public string QuestionText { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
