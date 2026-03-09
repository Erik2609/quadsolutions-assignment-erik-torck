using System.Text.Json.Serialization;

namespace QuizRepository.OpenTdb.Models
{
    internal class GetQuestionsModel
    {
        [JsonPropertyName("response_code")]
        internal string ResponseCode { get; set; } = string.Empty;

        [JsonPropertyName("results")]
        internal IEnumerable<QuestionModel> Results { get; set; } = [];
    }
}
