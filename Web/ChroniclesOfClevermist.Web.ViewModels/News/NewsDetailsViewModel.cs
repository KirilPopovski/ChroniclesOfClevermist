namespace ChroniclesOfClevermist.Web.ViewModels.News
{
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Mapping;

    public class NewsDetailsViewModel : IMapFrom<News>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public byte[] Image { get; set; }
    }
}
