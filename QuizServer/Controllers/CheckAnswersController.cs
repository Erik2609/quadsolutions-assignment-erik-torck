using Microsoft.AspNetCore.Mvc;
using QuizServices.Models;
using QuizServices.Service;

namespace QuizServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAnswersController : ControllerBase
    {
        private readonly ICheckAnswersService _checkAnswersService;

        public CheckAnswersController(ICheckAnswersService checkAnswersService)
        {
            _checkAnswersService = checkAnswersService;
        }

        [HttpPost(Name = "checkanswers")]
        public async Task<IEnumerable<IsAnswerCorrectModel>> Post([FromBody] IEnumerable<CheckAnswerModel> model)
        {
            return await _checkAnswersService.CheckAnswerAsync(model);
        }
    }
}
