namespace OpenTriviaApp;

public interface IApiCaller
{
    Task<List<OpenTriviaAppClient.TriviaQuestion>> GetQuestionsAsync();
}
