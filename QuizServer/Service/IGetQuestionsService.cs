using QuizServer.Models;

namespace QuizServer.Service
{
    public interface IGetQuestionsService
    {
        Task<IEnumerable<QuestionModel>> GetQuestions();
    }
}
