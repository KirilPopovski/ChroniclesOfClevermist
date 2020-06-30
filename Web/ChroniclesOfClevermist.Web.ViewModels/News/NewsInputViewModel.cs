namespace ChroniclesOfClevermist.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using Microsoft.AspNetCore.Http;

    public class NewsInputViewModel
    {
        [Required]
        [MaxLength(300)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
