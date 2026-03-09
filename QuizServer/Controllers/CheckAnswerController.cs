using Microsoft.AspNetCore.Mvc;
using QuizServer.Models;
using QuizServer.Service;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAnswerController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly ICheckAnswersService _checkAnswersService;

        public CheckAnswerController(ILogger<QuestionsController> logger, ICheckAnswersService checkAnswersService)
        {
            _logger = logger;
            _checkAnswersService = checkAnswersService;
        }

        [HttpPost(Name = "checkanswers")]
        public async Task<IEnumerable<IsAnswerCorrectModel>> Post(IEnumerable<CheckAnswerModel> model)
        {
            return await _checkAnswersService.CheckAnswerAsync(model);
        }
    }
}
