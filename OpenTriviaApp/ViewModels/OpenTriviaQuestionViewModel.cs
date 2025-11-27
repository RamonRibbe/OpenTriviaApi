namespace OpenTriviaApp.ViewModels;

public record OpenTriviaQuestionViewModel()
{
    public required string Text { get; init; }

    public bool IsAnswered { get; set; } = false;

    public int? GivenAnswerId { get; set; }

    public required List<OpenTriviaAnswerViewModel> Answers { get; init; }
}
