using OpenTriviaAppClient;

namespace OpenTriviaApp;

public class ApiCaller : IApiCaller
{
    private readonly swaggerClient client;

    public ApiCaller(swaggerClient client)
    {
        this.client = client;
    }

    public async Task<List<OpenTriviaAppClient.TriviaQuestion>> GetQuestionsAsync()
    {
        var items = await this.client.GetTriviaQuestionsAsync();

        return items.ToList();
    }
}
