using System.Text.Json.Serialization;

namespace QuizRepository.OpenTdb.Models
{
    public class GetQuestionsModel
    {
        [JsonPropertyName("response_code")]
        public int ResponseCode { get; set; } = 0;

        [JsonPropertyName("results")]
        public IEnumerable<QuestionModel> Results { get; set; } = [];
    }
}
