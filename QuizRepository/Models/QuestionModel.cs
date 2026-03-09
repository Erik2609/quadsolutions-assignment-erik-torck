namespace QuizRepository.Models
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; } = string.Empty;
        /// <summary>
        /// The answers are sorted by alphabet ascending.
        /// </summary>
        public IEnumerable<string> SortedPossibleAnswers { get; set; } = [];
        public string CorrectAnswer { get; set; } = string.Empty;
    }
}
