namespace ChroniclesOfClevermist.Services.Data.News
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface INewsService
    {
        IEnumerable<T> GetAllNews<T>();
    }
}
