using System.Text.Json.Serialization;

namespace QuizRepository.OpenTdb.Models
{
    internal class QuestionModel
    {
        [JsonPropertyName("question")]
        public string Question { get; set; } = string.Empty;

        [JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; } = string.Empty;

        [JsonPropertyName("incorrect_answers")]
        public string[] IncorrectAnswers { get; set; } = [];
    }
}
