using QuizRepository.Models;
using QuizRepository.OpenTdb.Mappers;

namespace QuizRepository.OpenTdb
{
    public class OpenTdbRepository : IGetQuestionsRepository
    {
        public async Task<QuestionsModel> GetQuestionsAsync()
        {
            var client = new OpenTdbClient();
            var openTdbModel = await client.GetQuestionsAsync();
            return QuestionMapper.Map(openTdbModel);
        }
    }
}
