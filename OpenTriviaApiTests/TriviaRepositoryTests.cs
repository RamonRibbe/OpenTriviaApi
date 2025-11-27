using OpenTriviaApi.Repository;

namespace OpenTriviaApiTests;

public class TriviaRepositoryTests
{
    [Test]
    public async Task GetQuestionsAsyncTest()
    {
        ITriviaRepository sut = new TriviaRepository();

        var actual = await sut.GetQuestionsAsync();

        Assert.Pass();
    }

    [Test]
    public async Task GetQuestionsAsyncValidTest()
    {
        ITriviaRepository sut = new TriviaRepository();

        var actual = await sut.GetQuestionsAsync();

        // Each question should have exactly 4 answers
        Assert.True(actual.All(x => x.Answers.Count == 4));

        // The correct answer should be part of the Answers collection
        Assert.True(actual.All(x => x.Answers.Any(a => a.Id == x.CorrectAnswer)));
    }
}