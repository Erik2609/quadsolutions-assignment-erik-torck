using QuizRepository.Models;

namespace QuizRepository
{
    public class CachedGetQuestionsRepositoryDecorator : IGetQuestionsRepository
    {
        private readonly IGetQuestionsRepository getQuestionsRepository;
        private QuestionsModel? cachedQuestions;
        public CachedGetQuestionsRepositoryDecorator(IGetQuestionsRepository getQuestionsRepository)
        {
            this.getQuestionsRepository = getQuestionsRepository;
        }

        public async Task<QuestionsModel> GetQuestionsAsync()
        {
            if (cachedQuestions == null)
            {
                cachedQuestions = await getQuestionsRepository.GetQuestionsAsync();
            }
            return cachedQuestions;
        }
    }
}
