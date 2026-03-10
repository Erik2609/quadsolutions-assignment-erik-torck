using FakeItEasy;
using QuizRepository.Models;

namespace QuizRepository.Tests
{
    public class CachedGetQuestionsRepositoryDecoratorTests
    {

        [Test]
        public void CachedRepoDecorator_ShouldCallUnderlyingRepoOnlyOnce()
        {
            // Arrange
            var mockedQuestionsRepo = A.Fake<IGetQuestionsRepository>();
            A.CallTo(() => mockedQuestionsRepo.GetQuestionsAsync())
                .Returns(Task.FromResult(
                    new QuestionsModel()
                    {
                        Questions = [
                            new QuestionModel()
                            {
                                CorrectAnswer = "CorrectAnswer",
                                Id = 123,
                                Question = "Question",
                                SortedPossibleAnswers = ["CorrectAnswer", "WrongAnswer"]
                            }
                        ]
                    }));

            var cachedRepo = new CachedGetQuestionsRepositoryDecorator(mockedQuestionsRepo);

            // Act
            var questions1 = cachedRepo.GetQuestionsAsync().Result;
            var questions2 = cachedRepo.GetQuestionsAsync().Result;

            // Assert
            A.CallTo(() => mockedQuestionsRepo.GetQuestionsAsync())
                .MustHaveHappenedOnceExactly();

            Assert.That(questions1, Is.EqualTo(questions2));
        }
    }
}