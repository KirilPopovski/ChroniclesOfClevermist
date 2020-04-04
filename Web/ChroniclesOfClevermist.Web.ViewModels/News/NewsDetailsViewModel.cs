namespace ChroniclesOfClevermist.Web.ViewModels.News
{
    using System;

    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Mapping;

    public class NewsDetailsViewModel : IMapFrom<News>
    {
        public bool IsDeleted { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public byte[] Image { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
