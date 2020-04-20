namespace ChroniclesOfClevermist.Services.Data.Surveys
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISurveyService
    {
        Task AddSurveyAsync(string topic);

        Task AddQuestionToSurveyAsync(string question, string surveyTopic);

        List<string> GetAll(string userId);

        List<string> GetAllQuestions(string topic);

        Task AddOpinionToQuestionAsync(string questionText, string answer);

        Task AddSurveyToUser(string userId, string topic);
    }
}
