namespace ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers
{
    using System.Collections.Generic;
    using System.Linq;

    using ChroniclesOfClevermist.Data.Common.Repositories;
    using ChroniclesOfClevermist.Data.Models;
    using ChroniclesOfClevermist.Services.Mapping;

    public class QuestionsAndAnswersService : IQuestionsAndAnswersService
    {
        private readonly IDeletableEntityRepository<Question> questionsRepo;
        private readonly IDeletableEntityRepository<Answer> answersRepo;

        public QuestionsAndAnswersService(IDeletableEntityRepository<Question> questionsRepo, IDeletableEntityRepository<Answer> answersRepo)
        {
            this.questionsRepo = questionsRepo;
            this.answersRepo = answersRepo;
        }

        public IEnumerable<T> GetMostPopularQuestions<T>()
        {
            return this.questionsRepo.All() /*.Where(x => x.Answers.Count() > 2)*/.To<T>().ToList();
        }

        public IEnumerable<T> GetAllAnswers<T>(string id)
        {
            return this.answersRepo.All().Where(x => x.QuestionId == id).To<T>().ToList();
        }

        public string GetQuestionText(string id)
        {
            return this.questionsRepo.All().Where(x => x.Id == id).FirstOrDefault().Text;
        }
    }
}
