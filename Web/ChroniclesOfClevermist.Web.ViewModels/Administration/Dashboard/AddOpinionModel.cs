namespace ChroniclesOfClevermist.Web.ViewModels.Administration.Dashboard
{
    using System.ComponentModel.DataAnnotations;

    public class AddOpinionModel
    {
        [Required]
        public string Opinion { get; set; }

        public string SurveyTopic { get; set; }
    }
}
