using QuizRepository.Models;

namespace QuizRepository.OpenTdb
{
    public class CachedGetQuestionsRepositoryWrapper : IGetQuestionsRepository
    {
        private readonly IGetQuestionsRepository getQuestionsRepository;
        private QuestionsModel? cachedQuestions;
        public CachedGetQuestionsRepositoryWrapper(IGetQuestionsRepository getQuestionsRepository)
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
