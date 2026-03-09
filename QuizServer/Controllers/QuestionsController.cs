using Microsoft.AspNetCore.Mvc;
using QuizServices.Models;
using QuizServices.Service;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly IGetQuestionsService _getQuestionsService;

        public QuestionsController(ILogger<QuestionsController> logger, IGetQuestionsService getQuestionsService)
        {
            _logger = logger;
            _getQuestionsService = getQuestionsService;
        }

        [HttpGet(Name = "questions")]
        public async Task<IEnumerable<QuestionModel>> Get()
        {
            return await _getQuestionsService.GetQuestions();
        }
    }
}
