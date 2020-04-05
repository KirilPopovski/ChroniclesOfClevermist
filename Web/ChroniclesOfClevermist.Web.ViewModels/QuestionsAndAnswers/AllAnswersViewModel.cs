namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    using System.Collections.Generic;

    public class AllAnswersViewModel
    {
        public string QuestionText { get; set; }

        public IEnumerable<AnswerViewModel> Answers { get; set; }
    }
}
