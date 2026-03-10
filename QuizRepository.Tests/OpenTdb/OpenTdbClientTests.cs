using FakeItEasy;
using Microsoft.Extensions.Configuration;
using QuizRepository.OpenTdb;
using System.Text.Json;

namespace QuizRepository.Tests.OpenTdb
{
    public class OpenTdbClientTests
    {
        const string OpenTdbApiEndpoint = "https://local.test/api";
        const string MockResponseContent = @"{
  ""response_code"": 0,
  ""results"": [
    {
      ""type"": ""multiple"",
      ""difficulty"": ""medium"",
      ""category"": ""Entertainment: Musicals &amp; Theatres"",
      ""question"": ""Who is the musical director for the award winning musical &quot;Hamilton&quot;?"",
      ""correct_answer"": ""Alex Lacamoire"",
      ""incorrect_answers"": [
        ""Lin-Manuel Miranda"",
        ""Renee Elise-Goldberry"",
        ""Leslie Odom Jr.""
      ]
    }
  ]
}";
        [Test]
        public void OpenTdbClient_ShouldReturnOpenTdbModels_OnSucces()
        {
            // Arrange
            var mockedIConfig = A.Fake<IConfiguration>();
            A.CallTo(() => mockedIConfig["OpenTdbApiEndpoint"])
                    .Returns(OpenTdbApiEndpoint);

            using var response = new HttpResponseMessage
            {
                Content = new StringContent(MockResponseContent)
            };
            var handler = A.Fake<FakeableHttpMessageHandler>();
            A.CallTo(() => handler.FakeSendAsync(
                    A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
                .Returns(response);
            var httpClient = new HttpClient(handler);

            var openTdbClient = new OpenTdbClient(httpClient, mockedIConfig);

            // Act
            var result = openTdbClient.GetQuestionsAsync().Result;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Results.Count(), Is.EqualTo(1));
            Assert.That(result.Results.First().Question, 
                Is.EqualTo("Who is the musical director for the award winning musical &quot;Hamilton&quot;?"));
        }

        [Test]
        public void OpenTdbClient_ShouldThrowException_WhenResponseIsNotSuccessStatusCode()
        {
            // Arrange
            var mockedIConfig = A.Fake<IConfiguration>();
            A.CallTo(() => mockedIConfig["OpenTdbApiEndpoint"])
                    .Returns(OpenTdbApiEndpoint);

            using var response = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.InternalServerError,
            };
            var handler = A.Fake<FakeableHttpMessageHandler>();
            A.CallTo(() => handler.FakeSendAsync(
                    A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
                .Returns(response);
            var httpClient = new HttpClient(handler);

            var openTdbClient = new OpenTdbClient(httpClient, mockedIConfig);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(openTdbClient.GetQuestionsAsync,
                $"Failed to get questions from OpenTDB API. Status code: {response.StatusCode}");
        }

        [Test]
        public void OpenTdbClient_ShouldThrowJsonException_WhenResponseIsInvalidJson()
        {
            // Arrange
            var mockedIConfig = A.Fake<IConfiguration>();
            A.CallTo(() => mockedIConfig["OpenTdbApiEndpoint"])
                    .Returns(OpenTdbApiEndpoint);

            using var response = new HttpResponseMessage
            {
                Content = new StringContent("Invalid JSON")
            };
            var handler = A.Fake<FakeableHttpMessageHandler>();
            A.CallTo(() => handler.FakeSendAsync(
                    A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
                .Returns(response);
            var httpClient = new HttpClient(handler);

            var openTdbClient = new OpenTdbClient(httpClient, mockedIConfig);

            // Act & Assert
            Assert.ThrowsAsync<JsonException>(openTdbClient.GetQuestionsAsync);
        }

        [Test]
        public void OpenTdbClient_ShouldThrowInvalidDataException_WhenResponseIsEmpty()
        {
            // Arrange
            var mockedIConfig = A.Fake<IConfiguration>();
            A.CallTo(() => mockedIConfig["OpenTdbApiEndpoint"])
                    .Returns(OpenTdbApiEndpoint);

            using var response = new HttpResponseMessage
            {
                Content = new StringContent("{}")
            };
            var handler = A.Fake<FakeableHttpMessageHandler>();
            A.CallTo(() => handler.FakeSendAsync(
                    A<HttpRequestMessage>.Ignored, A<CancellationToken>.Ignored))
                .Returns(response);
            var httpClient = new HttpClient(handler);

            var openTdbClient = new OpenTdbClient(httpClient, mockedIConfig);

            // Act & Assert
            Assert.ThrowsAsync<InvalidDataException>(openTdbClient.GetQuestionsAsync);
        }
    }
}
