using QuizRepository.Models;

namespace QuizRepository
{
    public interface IGetQuestionsRepository
    {
        /// <summary>
        /// Gets a list of questions with possible answers and the correct answer.
        /// </summary>
        Task<QuestionsModel> GetQuestionsAsync();
    }
}
