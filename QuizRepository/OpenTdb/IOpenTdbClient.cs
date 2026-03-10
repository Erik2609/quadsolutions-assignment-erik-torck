using QuizRepository.OpenTdb.Models;

namespace QuizRepository.OpenTdb
{
    public interface IOpenTdbClient
    {
        Task<GetQuestionsModel> GetQuestionsAsync();
    }
}
