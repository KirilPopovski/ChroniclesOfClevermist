namespace ChroniclesOfClevermist.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChroniclesOfClevermist.Data.Common.Models;

    public class Opinion : BaseDeletableModel<string>
    {
        public Opinion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(300)]
        public string Text { get; set; }

        public IEnumerable<UserOpinion> UserOpinions { get; set; }

        public string SurveyId { get; set; }

        public Survey Survey { get; set; }
    }
}
