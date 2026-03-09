using Microsoft.AspNetCore.Mvc;
using QuizServer.Models;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAnswerController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        public CheckAnswerController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "checkanswers")]
        public IEnumerable<IsAnswerCorrectModel> Post(IEnumerable<CheckAnswerModel> model)
        {
            throw new NotImplementedException();
        }
    }
}
