namespace QuizServices.Models
{
    public class IsAnswerCorrectModel
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string CorrectAnswer { get; set; }  = string.Empty;
    }
}
