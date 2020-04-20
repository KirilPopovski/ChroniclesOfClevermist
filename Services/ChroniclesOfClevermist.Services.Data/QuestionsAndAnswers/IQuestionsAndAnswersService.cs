namespace ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ChroniclesOfClevermist.Data.Common.Models;

    public interface IQuestionsAndAnswersService
    {
        IEnumerable<T> GetMostPopularQuestions<T>();

        IEnumerable<T> GetAllAnswers<T>(string id);

        string GetQuestionText(string id);

        Task AddQuestionAsync(string text, string email);
        Task AddAnswerAsync(string questionId, string text);
    }
}
