namespace ChroniclesOfClevermist.Services.Data.NewsSpace
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface INewsService
    {
        IEnumerable<T> GetAllNews<T>();

        Task CreateAsync(string title, string content, byte[] image);

        Task RemoveAsync(string id);
    }
}
