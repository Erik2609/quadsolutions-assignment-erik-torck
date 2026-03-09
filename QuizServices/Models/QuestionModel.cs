namespace QuizServices.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        public IEnumerable<string> PossibleAnswers { get; set; } = [];
    }
}
