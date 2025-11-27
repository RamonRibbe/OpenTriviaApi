using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using OpenTriviaApi.Controllers;
using OpenTriviaApi.Repository;

namespace OpenTriviaApiTests;

public class TriviaQuestionsControllerTests
{
    [Test]
    public async Task GetTest()
    {
        var expected = new List<OpenTriviaApi.Models.TriviaQuestion>();

        var loggerMock = Mock.Of<ILogger<TriviaQuestionsController>>();
        var repositoryMock = new Mock<ITriviaRepository>();
        repositoryMock.Setup(x => x.GetQuestionsAsync())
            .ReturnsAsync(expected);

        var sut = new TriviaQuestionsController(loggerMock, repositoryMock.Object);
        var actual = (await sut.Get() as OkObjectResult);

        Assert.That(actual?.Value, Is.EqualTo(expected));
    }
}
