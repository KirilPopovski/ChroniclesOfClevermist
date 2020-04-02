namespace ChroniclesOfClevermist.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data.Models;

    public class NewsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.News.Any())
            {
                return;
            }

            var reader = new FileStream(@"C:\Users\Kiril\Desktop\ChroniclesOfClevermist\mainBG.jpg", FileMode.Open);
            byte[] image = new byte[10000];
            reader.Read(image, 0, 10000);

            var news = new List<string> { "Lorem Ipsum is simply dummy text of the printing and typesetting" +
                " industry. Lorem Ipsum has been the industry's standard dummy text ever " +
                "since the 1500s, when an unknown printer took a galley of type and scrambled " +
                "it to make a type specimen book.",
                "It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." +
                "It was popularised in the 1960s with the release of Letraset sheets containing Lorem",
                "Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker " +
                "including versions of Lorem Ipsum.",
                "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece" +
                " of classical Latin literature from 45 BC, making it over 2000 years old. Richard McClintock,",
                "a Latin professor at Hampden-Sydney College in Virginia, looked up one of the more obscure" +
                " Latin words, consectetur, from a Lorem Ipsum",
            };

            foreach (var item in news)
            {
                await dbContext.News.AddAsync(new News
                {
                    Content = item,
                    Image = image,
                    Title = "Test",
                });
            }

        }
    }
}
