namespace ChroniclesOfClevermist.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ChroniclesOfClevermist.Data.Common.Models;

    public class Survey : BaseDeletableModel<string>
    {
        public Survey()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(300)]
        public string Topic { get; set; }

        [Required]
        public DateTime ExpiresOn { get; set; }

        public IEnumerable<Opinion> Opinions { get; set; }
    }
}
