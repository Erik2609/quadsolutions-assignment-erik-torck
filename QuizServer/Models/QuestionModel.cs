namespace QuizServer.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public string[] PossibleAnswers { get; set; } = [];
    }
}
