namespace ChroniclesOfClevermist.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data.Models;

    public class QuestionsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Questions.Any())
            {
                return;
            }

            var questions = new List<string>
            {
                "How big is the universe?",
                "Who am I?",
                "Is there any good reason to what I am doing at the moment? An if yes, what is it?",
                "How to play the game?",
            };

            foreach (var question in questions)
            {
                await dbContext.Questions.AddAsync(new Question
                {
                    Text = question,
                    Email = "kipopo@abv.bg",
                });
            }
        }
    }
}
