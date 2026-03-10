using QuizServices.Models;

namespace QuizServices.Service
{
    public interface IGetQuestionsService
    {
        Task<IEnumerable<QuestionModel>> GetQuestions();
    }
}
