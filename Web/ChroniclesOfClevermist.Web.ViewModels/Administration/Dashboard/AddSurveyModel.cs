namespace ChroniclesOfClevermist.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class AddSurveyModel
    {
        [Required]
        [MaxLength(500)]
        public string Topic { get; set; }
    }
}
