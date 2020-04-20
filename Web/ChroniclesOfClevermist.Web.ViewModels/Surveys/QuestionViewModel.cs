namespace ChroniclesOfClevermist.Web.ViewModels.Surveys
{
    using System.ComponentModel.DataAnnotations;

    public class QuestionViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Question { get; set; }

        [Required]
        [MaxLength(500)]
        public string Answer { get; set; }
    }
}
