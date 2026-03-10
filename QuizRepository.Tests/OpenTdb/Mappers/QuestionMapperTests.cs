using QuizRepository.OpenTdb.Mappers;
using QuizRepository.OpenTdb.Models;

namespace QuizRepository.Tests.OpenTdb.Mappers
{
    public class QuestionMapperTests
    {
        [Test]
        public void QuestionMapper_ShouldMapCorrectly()
        {
            // arrange
            var modelToMap = new GetQuestionsModel
            {
                Results = [
                    new QuestionModel
                    {
                        Question = "What is the capital of France?",
                        CorrectAnswer = "Paris",
                        IncorrectAnswers = ["London", "Berlin", "Madrid"],
                    },
                    new QuestionModel
                    {
                        Question = "What is the capital of the Netherlands?",
                        CorrectAnswer = "Amsterdam",
                        IncorrectAnswers = ["The Hague", "Berlin", "Brussels"],
                    }]
            };

            // act
            var mappedModel = QuestionMapper.Map(modelToMap);

            // assert
            Assert.That(mappedModel.Questions.Count, Is.EqualTo(2));

            var firstQuestion = mappedModel.Questions.First();
            Assert.That(firstQuestion.Id, Is.EqualTo(1));
            Assert.That(firstQuestion.Question, Is.EqualTo("What is the capital of France?"));
            Assert.That(firstQuestion.CorrectAnswer, Is.EqualTo("Paris"));
            Assert.That(firstQuestion.SortedPossibleAnswers, 
                Is.EquivalentTo(new string[] { "Berlin", "London",  "Madrid", "Paris" }));

            var secondQuestion = mappedModel.Questions.Last();
            Assert.That(secondQuestion.Id, Is.EqualTo(2));
            Assert.That(secondQuestion.Question, Is.EqualTo("What is the capital of the Netherlands?"));
            Assert.That(secondQuestion.CorrectAnswer, Is.EqualTo("Amsterdam"));
            Assert.That(secondQuestion.SortedPossibleAnswers,
                Is.EquivalentTo(new string[] { "Amsterdam", "Berlin", "Brussels", "The Hague" }));



        }
    }
}
