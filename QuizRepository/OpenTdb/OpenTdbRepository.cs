using QuizRepository.Models;
using QuizRepository.OpenTdb.Mappers;

namespace QuizRepository.OpenTdb
{
    public class OpenTdbRepository : IGetQuestionsRepository
    {
        private readonly IOpenTdbClient _openTdbClient;
        public OpenTdbRepository(IOpenTdbClient openTdbClient)
        {
            _openTdbClient = openTdbClient;
        }

        public async Task<QuestionsModel> GetQuestionsAsync()
        {
            var openTdbModel = await _openTdbClient.GetQuestionsAsync();
            return QuestionMapper.Map(openTdbModel);
        }
    }
}
