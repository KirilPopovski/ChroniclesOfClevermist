namespace ChroniclesOfClevermist.Services.Data.Surveys
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;

    public class SurveyService : ISurveyService
    {
        private readonly IDeletableEntityRepository<Survey> surveysRepo;
        private readonly IDeletableEntityRepository<Opinion> opinionsRepo;

        public SurveyService(IDeletableEntityRepository<Survey> surveysRepo, IDeletableEntityRepository<Opinion> opinionsRepo)
        {
            this.surveysRepo = surveysRepo;
            this.opinionsRepo = opinionsRepo;
        }

        public async Task AddSurveyAsync(string topic)
        {
            var expiration = DateTime.UtcNow.AddDays(7);
            await this.surveysRepo.AddAsync(new Survey { Topic = topic, ExpiresOn = expiration });
            await this.surveysRepo.SaveChangesAsync();
        }

        public async Task AddQuestionToSurveyAsync(string question, string surveyTopic)
        {
            var survey = this.surveysRepo.All().Where(x => x.Topic == surveyTopic).FirstOrDefault();
            await this.opinionsRepo.AddAsync(new Opinion
            {
                Text = question,
                Survey = survey,
                SurveyId = survey.Id,
            });
            await this.opinionsRepo.SaveChangesAsync();
        }

        public List<string> GetAll()
        {
            return this.surveysRepo.All().Where(x => x.ExpiresOn > DateTime.Now).Select(x => x.Topic).ToList();
        }

        public List<string> GetAllQuestions(string topic)
        {
            var survey = this.surveysRepo.All().Where(x => x.Topic == topic).FirstOrDefault();
            var questions = this.opinionsRepo.All().Where(x => x.SurveyId == survey.Id).Select(x => x.Text).ToList();
            return questions;
        }
    }
}
