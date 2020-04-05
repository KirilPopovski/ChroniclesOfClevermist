namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    using System;

    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Mapping;

    public class AnswerViewModel : IMapFrom<Answer>
    {
        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
