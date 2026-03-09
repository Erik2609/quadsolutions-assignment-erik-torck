using QuizRepository;
using QuizServer.Models;

namespace QuizServer.Service
{
    public class CheckAnswersService : ICheckAnswersService
    {
        private readonly IGetQuestionsRepository _getQuestionsRepository;
        public CheckAnswersService(IGetQuestionsRepository getQuestionsRepository)
        {
            _getQuestionsRepository = getQuestionsRepository;
        }

        public async Task<IEnumerable<IsAnswerCorrectModel>> CheckAnswerAsync(IEnumerable<CheckAnswerModel> checkAnswerModel)
        {
            var questions = await _getQuestionsRepository.GetQuestionsAsync();
            if (questions == null)
            {
                throw new Exception("Questions not found");
            }

            if (checkAnswerModel.Count() != questions.Questions.Count())
            {
                throw new Exception("Number of answers does not match number of questions");
            }

            var checkedAnswers = new List<IsAnswerCorrectModel>();
            foreach (var answerToCheck in checkAnswerModel)
            {
                var matchingQuestion = questions.Questions.FirstOrDefault(q => q.Id == answerToCheck.Id);
                if (matchingQuestion == null)
                {
                    throw new Exception($"Question with id {answerToCheck.Id} not found");
                }

                checkedAnswers.Add(new IsAnswerCorrectModel
                {
                    Id = answerToCheck.Id,
                    IsCorrect = answerToCheck.Answer.Equals(matchingQuestion.CorrectAnswer,
                    StringComparison.OrdinalIgnoreCase)
                });
            }

            return checkedAnswers;
        }
    }
}
