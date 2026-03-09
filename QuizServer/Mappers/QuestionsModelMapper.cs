using QuizServer.Models;

namespace QuizServer.Mappers
{
    public static class QuestionsModelMapper
    {
        public static IEnumerable<QuestionModel> MapToGetQuestions(QuizRepository.Models.QuestionsModel questionsModel)
        {
            return questionsModel.Questions.Select(q => new QuestionModel
            {
                Id = q.Id,
                Question = q.Question,
                PossibleAnswers = q.SortedPossibleAnswers
            });
        }
    }
}
