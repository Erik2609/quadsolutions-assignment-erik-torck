using QuizServices.Models;

namespace QuizServices.Service
{
    public interface ICheckAnswersService
    {
        Task<IEnumerable<IsAnswerCorrectModel>> CheckAnswerAsync(IEnumerable<CheckAnswerModel> checkAnswerModel);
    }
}
