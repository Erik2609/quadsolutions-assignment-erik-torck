using FakeItEasy;
using QuizRepository;
using QuizServices.Models;
using QuizServices.Service;

namespace QuizServices.Tests.Services
{
    public class CheckAnswersServiceTests
    {
        [Test]
        public void CheckAnsersService_ShouldThrowException_WhenRepositoryReturnsNull()
        {
            // Arrange
            var mockedRepo = A.Fake<IGetQuestionsRepository>();
            A.CallTo(() => mockedRepo.GetQuestionsAsync())
                .Returns(Task.FromResult<QuizRepository.Models.QuestionsModel>(null));
            var checkAnswersService = new CheckAnswersService(mockedRepo);
            var answersToCheck = Enumerable.Empty<CheckAnswerModel>();

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() =>
                checkAnswersService.CheckAnswerAsync(answersToCheck), "Questions not found");

        }

        [Test]
        public void CheckAnsersService_ShouldThrowException_WhenAnswersToCheckDontMatchQuestionCount()
        {
            // Arrange
            var questionsModel = new QuizRepository.Models.QuestionsModel()
            {
                Questions = new QuizRepository.Models.QuestionModel[]
                {
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 1,
                        Question = "Question1",
                        CorrectAnswer = "Answer1"
                    },
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 2,
                        Question = "Question2",
                        CorrectAnswer = "Answer2"
                    }
                }
            };

            var mockedRepo = A.Fake<IGetQuestionsRepository>();
            A.CallTo(() => mockedRepo.GetQuestionsAsync())
                .Returns(Task.FromResult(questionsModel));

            var checkAnswersService = new CheckAnswersService(mockedRepo);
            var answersToCheck = new CheckAnswerModel[]
                {
                    new CheckAnswerModel()
                    {
                        Id = 1,
                        Answer = "Answer1"
                    },
                };

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() =>
                checkAnswersService.CheckAnswerAsync(answersToCheck),
                "Number of answers does not match number of questions");

        }

        [Test]
        public void CheckAnsersService_ShouldThrowException_WhenAnswersToCheckDontHaveMatchingId()
        {
            // Arrange
            var questionsModel = new QuizRepository.Models.QuestionsModel()
            {
                Questions = new QuizRepository.Models.QuestionModel[]
                {
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 1,
                        Question = "Question1",
                        CorrectAnswer = "Answer1"
                    },
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 2,
                        Question = "Question2",
                        CorrectAnswer = "Answer2"
                    }
                }
            };

            var mockedRepo = A.Fake<IGetQuestionsRepository>();
            A.CallTo(() => mockedRepo.GetQuestionsAsync())
                .Returns(questionsModel);

            var checkAnswersService = new CheckAnswersService(mockedRepo);
            var answersToCheck = new CheckAnswerModel[]
                {
                    new CheckAnswerModel()
                    {
                        Id = 1,
                        Answer = "Answer1"
                    },
                    new CheckAnswerModel()
                    {
                        Id = 3,
                        Answer = "Answer3"
                    },
                };

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() =>
                checkAnswersService.CheckAnswerAsync(answersToCheck),
                "Question with id 3 not found");

        }

        [Test]
        public void CheckAnsersService_ShouldReturnCorrectResponse()
        {
            // Arrange
            var questionsModel = new QuizRepository.Models.QuestionsModel()
            {
                Questions = new QuizRepository.Models.QuestionModel[]
                {
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 1,
                        Question = "Question1",
                        CorrectAnswer = "Answer1"
                    },
                    new QuizRepository.Models.QuestionModel()
                    {
                        Id = 2,
                        Question = "Question2",
                        CorrectAnswer = "Answer2"
                    }
                }
            };

            var mockedRepo = A.Fake<IGetQuestionsRepository>();
            A.CallTo(() => mockedRepo.GetQuestionsAsync())
                .Returns(Task.FromResult(questionsModel));

            var checkAnswersService = new CheckAnswersService(mockedRepo);
            var answersToCheck = new CheckAnswerModel[]
                {
                    new CheckAnswerModel()
                    {
                        Id = 1,
                        Answer = "WrongAnswer1"
                    },
                    new CheckAnswerModel()
                    {
                        Id = 2,
                        Answer = "Answer2"
                    },
                };

            // Act
            var result = checkAnswersService.CheckAnswerAsync(answersToCheck).Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));

            var firstResult = result.FirstOrDefault(r => r.Id == 1);
            Assert.That(firstResult, Is.Not.Null);
            Assert.That(firstResult.IsCorrect, Is.False);
            Assert.That(firstResult.CorrectAnswer, Is.EqualTo("Answer1"));

            var secondResult = result.FirstOrDefault(r => r.Id == 2);
            Assert.That(secondResult, Is.Not.Null);
            Assert.That(secondResult.IsCorrect, Is.True);
            Assert.That(secondResult.CorrectAnswer, Is.EqualTo("Answer2"));

        }
    }
}
