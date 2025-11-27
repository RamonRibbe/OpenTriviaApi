namespace OpenTriviaApi.Models;

public class TriviaQuestion
{
    public required int Id { get; init; }

    public required string Question { get; init; }

    public required List<TriviaAnswer> Answers { get; init; }

    public required int CorrectAnswer { get; init; }
}
