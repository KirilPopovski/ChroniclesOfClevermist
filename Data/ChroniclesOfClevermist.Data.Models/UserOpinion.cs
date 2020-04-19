using ChroniclesOfClevermist.Data.Common.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ChroniclesOfClevermist.Data.Models
{
    public class UserOpinion : BaseDeletableModel<string>
    {
        public UserOpinion()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string OpinionId { get; set; }

        public Opinion Opinion { get; set; }

        [Required]
        [MaxLength(500)]
        public string Text { get; set; }
    }
}