namespace ChroniclesOfClevermist.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ChroniclesOfClevermist.Data.Common.Models;

    public class Question : BaseDeletableModel<string>
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<Answer> Answers { get; set; }
    }
}
