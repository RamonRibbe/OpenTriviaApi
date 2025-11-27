namespace OpenTriviaApp.ViewModels;

public record OpenTriviaAnswerViewModel
{
    public required int Id { get; init; }

    public required string Text { get; init; }

    public required string UiClass { get; set; }
}
