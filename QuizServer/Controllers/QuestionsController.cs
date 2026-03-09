using Microsoft.AspNetCore.Mvc;
using QuizServer.Models;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly ILogger<QuestionsController> _logger;
        public QuestionsController(ILogger<QuestionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "questions")]
        public IEnumerable<QuestionModel> Get()
        {
            throw new NotImplementedException();
        }
    }
}
