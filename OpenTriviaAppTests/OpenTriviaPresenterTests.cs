using Microsoft.Extensions.Logging;
using Moq;
using OpenTriviaApi.Controllers;
using OpenTriviaApp;
using OpenTriviaApp.Presenters;

namespace OpenTriviaAppTests;

public class OpenTriviaPresenterTests
{
    private readonly List<OpenTriviaAppClient.TriviaQuestion> data;

    public OpenTriviaPresenterTests()
    {
        this.data =
        [
            new()
            {
                Id = 1,
                Question = "Question 1",
                Answers =
                [
                    new()
                    {
                        Id = 1,
                        Answer = "Answer 1",
                    },
                    new()
                    {
                        Id = 2,
                        Answer = "Answer 2",
                    },
                    new()
                    {
                        Id = 3,
                        Answer = "Answer 3",
                    },
                    new()
                    {
                        Id = 4,
                        Answer = "Answer 4",
                    },
                ],
                CorrectAnswer = 2,
            },
        ];
    }

    [Test]
    public async Task InitTest()
    {
        var loggerMock = Mock.Of<ILogger<OpenTriviaPresenter>>();

        var apiCaller = new Mock<IApiCaller>();
        apiCaller.Setup(x => x.GetQuestionsAsync())
            .ReturnsAsync(this.data);

        IOpenTriviaPresenter sut = new OpenTriviaPresenter(loggerMock, apiCaller.Object);

        await sut.Init();

        // Check if the currentquestion has been set
        Assert.True(sut.HasCurrentQuestion);
    }

    [Test]
    public async Task InitWithExceptionTest()
    {
        var loggerMock = Mock.Of<ILogger<OpenTriviaPresenter>>();

        var apiCaller = new Mock<IApiCaller>();
        apiCaller.Setup(x => x.GetQuestionsAsync())
            .ThrowsAsync(new Exception());

        IOpenTriviaPresenter sut = new OpenTriviaPresenter(loggerMock, apiCaller.Object);

        await sut.Init();

        Assert.False(sut.HasCurrentQuestion);
    }

    [Test]
    public async Task NextQuestionTest()
    {
        var loggerMock = Mock.Of<ILogger<OpenTriviaPresenter>>();

        var apiCaller = new Mock<IApiCaller>();
        apiCaller.Setup(x => x.GetQuestionsAsync())
            .ReturnsAsync(this.data);

        var sut = new OpenTriviaPresenter(loggerMock, apiCaller.Object);

        await sut.Init();

        sut.NextQuestion();

        // Check if the currentquestion has been set
        Assert.True(sut.HasCurrentQuestion);
    }

    [Test]
    public async Task AnswerQuestionIncorrectTest()
    {
        var loggerMock = Mock.Of<ILogger<OpenTriviaPresenter>>();

        var apiCaller = new Mock<IApiCaller>();
        apiCaller.Setup(x => x.GetQuestionsAsync())
            .ReturnsAsync(this.data);

        var sut = new OpenTriviaPresenter(loggerMock, apiCaller.Object);

        await sut.Init();

        // Check if the currentquestion has been set
        Assert.True(sut.HasCurrentQuestion);

        // Answer incorrect
        sut.AnswerQuestion(1);

        // Check if the answered flag has been set
        Assert.True(sut.CurrentQuestion!.IsAnswered);
        Assert.That(sut.CurrentQuestion!.GivenAnswerId!.Value, Is.EqualTo(1));

        // Check if the correct classes have been set
        foreach (var answer in sut.CurrentQuestion.Answers)
        {
            // Red for incorrect given answer, green for the correct answer, grey for the rest
            if (answer.Id == 1)
            {
                Assert.That(answer.UiClass, Is.EqualTo("red"));
            }
            else if (answer.Id == 2)
            {
                Assert.That(answer.UiClass, Is.EqualTo("green"));
            }
            else
            {
                Assert.That(answer.UiClass, Is.EqualTo("grey"));
            }
        }
    }
}
