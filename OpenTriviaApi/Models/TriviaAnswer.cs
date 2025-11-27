namespace OpenTriviaApi.Models;

public record TriviaAnswer
{
    public required int Id { get; init; }

    public required string Answer { get; init; }
}
