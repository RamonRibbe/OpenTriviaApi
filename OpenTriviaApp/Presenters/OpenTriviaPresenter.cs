using OpenTriviaApp.ViewModels;

namespace OpenTriviaApp.Presenters;

public class OpenTriviaPresenter : IOpenTriviaPresenter
{
    private const string defaultAnswerClass = "grey";
    private const string wrongAnswerClass = "red";
    private const string correctAnswerClass = "green";
    private readonly ILogger<OpenTriviaPresenter> logger;

    // Api related variables
    private readonly IApiCaller apiCaller;
    private List<OpenTriviaAppClient.TriviaQuestion>? apiQuestions;
    private int apiQuestionIndex = -1;
    private OpenTriviaAppClient.TriviaQuestion? apiQuestion => this.apiQuestions?[apiQuestionIndex];

    // Viewmodel related variables
    public OpenTriviaQuestionViewModel? CurrentQuestion { get; private set; }
    public bool HasCurrentQuestion => this.CurrentQuestion is not null;

    public OpenTriviaPresenter(ILogger<OpenTriviaPresenter> logger, IApiCaller apiCaller)
    {
        this.logger = logger;
        this.apiCaller = apiCaller;
    }

    public async Task Init()
    {
        try
        {
            this.apiQuestions = await this.apiCaller.GetQuestionsAsync();

            var rand = new Random();
            this.apiQuestions = this.apiQuestions
                .OrderBy(c => rand.Next()).Select(c => c)
                .ToList();
            this.NextQuestion();
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, ex.Message);
            this.apiQuestions = null;
            this.CurrentQuestion = null;
        }
    }

    public void AnswerQuestion(int givenAnswerId)
    {
        if (this.CurrentQuestion is null
            || this.CurrentQuestion.IsAnswered)
        {
            return;
        }

        this.CurrentQuestion.IsAnswered = true;
        this.CurrentQuestion.GivenAnswerId = givenAnswerId;

        // set the proper UiClass for each answer to be rendered
        foreach(var answer in this.CurrentQuestion.Answers)
        {
            answer.UiClass = this.GetAnswerUiClass(answer, givenAnswerId);
        }
    }

    public void NextQuestion()
    {
        if (this.apiQuestionIndex == this.apiQuestions!.Count - 1)
        {
            this.apiQuestionIndex = 0;
        }
        else
        {
            this.apiQuestionIndex++;
        }

        var answers = this.apiQuestion!.Answers.Select(x => new OpenTriviaAnswerViewModel
        {
            Id = x.Id,
            Text = x.Answer,
            UiClass = defaultAnswerClass,
        }).ToList();

        this.CurrentQuestion = new OpenTriviaQuestionViewModel
        {
            Text = this.apiQuestion.Question,
            Answers = answers,
        };
    }

    private string GetAnswerUiClass(OpenTriviaAnswerViewModel answer, int? givenAnswerId)
    {
        // return green for the correct answer
        if (this.apiQuestion!.CorrectAnswer == answer.Id)
        {
            return correctAnswerClass;
        }

        // return red if the given answer is wrong
        if (givenAnswerId == answer.Id)
        {
            return wrongAnswerClass;
        }

        // return grey for all other answers
        return defaultAnswerClass;
    }
}
