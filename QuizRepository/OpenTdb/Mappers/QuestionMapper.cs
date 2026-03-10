using QuizRepository.OpenTdb.Models;

namespace QuizRepository.OpenTdb.Mappers
{
    public static class QuestionMapper
    {
        public static QuizRepository.Models.QuestionModel Map(QuestionModel question, int id)
        {
            return new QuizRepository.Models.QuestionModel
            {
                Question = question.Question,
                CorrectAnswer = question.CorrectAnswer,
                SortedPossibleAnswers = question.IncorrectAnswers.Append(question.CorrectAnswer).Order(),
                Id = id
            };
        }

        public static QuizRepository.Models.QuestionsModel Map(GetQuestionsModel questions)
        {
            var model = new QuizRepository.Models.QuestionsModel();

            var mappedQuestions = new List<QuizRepository.Models.QuestionModel>();
            int i = 1;
            foreach (var question in questions.Results)
            {
                mappedQuestions.Add(Map(question, i));
                i++;
            }
            model.Questions = mappedQuestions;

            return model;
        }
    }
}
