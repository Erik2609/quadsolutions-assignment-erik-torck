using Microsoft.AspNetCore.Mvc;
using QuizServices.Models;
using QuizServices.Service;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly IGetQuestionsService _getQuestionsService;

        public QuestionsController(IGetQuestionsService getQuestionsService)
        {
            _getQuestionsService = getQuestionsService;
        }

        /// <summary>
        /// Retrieves a list of quiz questions.
        /// </summary>
        /// <returns>A list of questions</returns>
        [HttpGet(Name = "questions")]
        public async Task<IEnumerable<QuestionModel>> Get()
        {
            return await _getQuestionsService.GetQuestions();
        }
    }
}
