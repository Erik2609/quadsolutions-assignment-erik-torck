using QuizRepository;
using QuizServices.Mappers;
using QuizServices.Models;

namespace QuizServices.Service
{
    public class GetQuestionsService : IGetQuestionsService
    {
        private readonly IGetQuestionsRepository _getQuestionsRepository;

        public GetQuestionsService(IGetQuestionsRepository getQuestionsRepository)
        {
            _getQuestionsRepository = getQuestionsRepository;
        }
        public async Task<IEnumerable<QuestionModel>> GetQuestions()
        {
            var questions = await _getQuestionsRepository.GetQuestionsAsync();
            if (questions == null)
            {
                throw new Exception("Questions not found");
            }
            return QuestionsModelMapper.MapToGetQuestions(questions);
        }
    }
}
