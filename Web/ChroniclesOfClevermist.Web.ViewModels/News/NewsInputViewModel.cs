namespace ChroniclesOfClevermist.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    public class NewsInputViewModel
    {
        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public byte[] Image { get; set; }
    }
}
