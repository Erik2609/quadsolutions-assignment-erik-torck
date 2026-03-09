using QuizServer.Models;

namespace QuizServer.Service
{
    public interface ICheckAnswersService
    {
        Task<IEnumerable<IsAnswerCorrectModel>> CheckAnswerAsync(IEnumerable<CheckAnswerModel> checkAnswerModel);
    }
}
