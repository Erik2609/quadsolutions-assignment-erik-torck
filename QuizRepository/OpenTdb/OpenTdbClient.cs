using Microsoft.Extensions.Configuration;
using QuizRepository.OpenTdb.Models;
using System.Net.Http.Json;

namespace QuizRepository.OpenTdb
{
    public class OpenTdbClient : IOpenTdbClient
    {
        private readonly string _apiEndpoint;
        private readonly HttpClient _httpClient;
        public OpenTdbClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiEndpoint = configuration["OpenTdbApiEndpoint"] ?? throw new Exception("OpenTdbApiEndpoint is not configured in appsettings.json");
        }

        public async Task<GetQuestionsModel> GetQuestionsAsync()
        {

            var response = await _httpClient.GetAsync(_apiEndpoint);
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
