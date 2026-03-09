using QuizRepository.OpenTdb.Models;
using System.Net.Http.Json;

namespace QuizRepository.OpenTdb
{
    internal class OpenTdbClient
    {
        // TODO move endpoint to appsettings
        internal async Task<GetQuestionsModel> GetQuestionsAsync(string apiEndpoint = "https://opentdb.com/api.php?amount=10&category=13")
        {
            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(apiEndpoint);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to get questions from OpenTDB API. Status code: {response.StatusCode}");
            }

            var questionsModel = await response.Content.ReadFromJsonAsync<GetQuestionsModel>();
            if (questionsModel == null)
            {
                throw new Exception("Failed to deserialize questions from OpenTDB API.");
            }

            return questionsModel;
        }
    }
}
