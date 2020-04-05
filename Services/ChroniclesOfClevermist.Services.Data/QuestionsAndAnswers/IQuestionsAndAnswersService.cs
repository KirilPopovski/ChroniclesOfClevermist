namespace ChroniclesOfClevermist.Services.Data.QuestionsAndAnswers
{
    using System.Collections.Generic;

    using ChroniclesOfClevermist.Data.Common.Models;

    public interface IQuestionsAndAnswersService
    {
        IEnumerable<T> GetMostPopularQuestions<T>();

        IEnumerable<T> GetAllAnswers<T>(string id);

        string GetQuestionText(string id);
    }
}
