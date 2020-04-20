namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    using System.ComponentModel.DataAnnotations;

    public class AnswerInputModel
    {
        public string QuestionId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }
    }
}
