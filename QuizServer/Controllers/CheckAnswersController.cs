using Microsoft.AspNetCore.Mvc;
using QuizServices.Models;
using QuizServices.Service;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAnswersController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        private readonly ICheckAnswersService _checkAnswersService;

        public CheckAnswersController(ILogger<QuestionsController> logger, ICheckAnswersService checkAnswersService)
        {
            _logger = logger;
            _checkAnswersService = checkAnswersService;
        }

        [HttpPost(Name = "checkanswers")]
        public async Task<IEnumerable<IsAnswerCorrectModel>> Post([FromBody] IEnumerable<CheckAnswerModel> model)
        {
            return await _checkAnswersService.CheckAnswerAsync(model);
        }
    }
}
