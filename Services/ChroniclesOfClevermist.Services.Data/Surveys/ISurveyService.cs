namespace ChroniclesOfClevermist.Services.Data.Surveys
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISurveyService
    {
        Task AddSurveyAsync(string topic);

        Task AddQuestionToSurveyAsync(string question, string surveyTopic);

        List<string> GetAll();

        List<string> GetAllQuestions(string topic);
    }
}
