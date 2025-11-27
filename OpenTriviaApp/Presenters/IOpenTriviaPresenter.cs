using OpenTriviaApp.ViewModels;

namespace OpenTriviaApp.Presenters;

public interface IOpenTriviaPresenter
{
    Task Init();

    void AnswerQuestion(int givenAnswerId);

    void NextQuestion();

    bool HasCurrentQuestion { get; }

    OpenTriviaQuestionViewModel? CurrentQuestion { get; }
 }
