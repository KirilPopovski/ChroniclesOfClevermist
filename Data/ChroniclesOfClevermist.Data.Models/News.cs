namespace ChroniclesOfClevermist.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using ChroniclesOfClevermist.Data.Common.Models;

    public class News : BaseDeletableModel<string>
    {
        public News()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public byte[] Image { get; set; }
    }
}
