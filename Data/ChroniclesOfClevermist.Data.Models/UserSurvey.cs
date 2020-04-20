namespace ChroniclesOfClevermist.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class UserSurvey
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string SurveyId { get; set; }

        public Survey Survey { get; set; }
    }
}
