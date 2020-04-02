namespace ChroniclesOfClevermist.Web.ViewModels.News
{
    using System.Collections.Generic;

    public class AllNewsViewModel
    {
        public IEnumerable<NewsDetailsViewModel> News { get; set; }
    }
}
