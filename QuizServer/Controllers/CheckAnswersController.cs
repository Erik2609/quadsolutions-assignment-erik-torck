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

        /// <summary>
        /// Checks answers submitted by the client by ID comparing them to the questions in the cache.
        /// </summary>
        /// <param name="model">The submitted answers</param>
        /// <returns>A list of answers with id, 'is correct' and 'correct answer'</returns>
        [HttpPost(Name = "checkanswers")]
        public async Task<IEnumerable<IsAnswerCorrectModel>> Post([FromBody] IEnumerable<CheckAnswerModel> model)
        {
            return await _checkAnswersService.CheckAnswerAsync(model);
        }
    }
}
