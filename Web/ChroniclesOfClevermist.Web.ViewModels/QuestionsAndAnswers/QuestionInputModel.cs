using System.ComponentModel.DataAnnotations;

namespace ChroniclesOfClevermist.Web.ViewModels.QuestionsAndAnswers
{
    public class QuestionInputModel
    {
        [Required]
        [MaxLength(300)]
        public string Text { get; set; }
    }
}
