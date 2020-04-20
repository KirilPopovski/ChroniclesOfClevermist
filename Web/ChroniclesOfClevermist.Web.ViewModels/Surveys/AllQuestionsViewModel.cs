namespace ChroniclesOfClevermist.Web.ViewModels.Surveys
{
    using System.Collections.Generic;

    public class AllQuestionsViewModel
    {
        public string Topic { get; set; }

        public List<QuestionViewModel> Questions { get; set; }
    }
}
