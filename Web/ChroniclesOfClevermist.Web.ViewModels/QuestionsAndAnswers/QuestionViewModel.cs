namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    using System;

    using ChroniclesOfClevermist.Data.Models;

    using ChroniclesOfClevermist.Services.Mapping;

    public class QuestionViewModel : IMapFrom<Question>
    {
        public string Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
