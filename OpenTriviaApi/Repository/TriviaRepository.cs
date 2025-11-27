using OpenTriviaApi.Models;

namespace OpenTriviaApi.Repository;

public class TriviaRepository : ITriviaRepository
{
    async Task<List<TriviaQuestion>> ITriviaRepository.GetQuestionsAsync()
    {
        return await Task.Run(() => triviaQuestions);
    }

    private static readonly List<TriviaQuestion> triviaQuestions =
    [
        new()
        {
            Id = 1,
            Question = "What is the capital of the Netherlands?",
            Answers =
            [
                new()
                {
                    Id = 1,
                    Answer = "London",
                },
                new()
                {
                    Id = 2,
                    Answer = "Amsterdam",
                },
                new()
                {
                    Id = 3,
                    Answer = "Dublin",
                },
                new()
                {
                    Id = 4,
                    Answer = "Rotterdam",
                },
            ],
            CorrectAnswer = 2,
        },
        new()
        {
            Id = 2,
            Question = "Which color is grass?",
            Answers =
            [
                new()
                {
                    Id = 1,
                    Answer = "Green",
                },
                new()
                {
                    Id = 2,
                    Answer = "Blue",
                },
                new()
                {
                    Id = 3,
                    Answer = "Purple",
                },
                new()
                {
                    Id = 4,
                    Answer = "White",
                },
            ],
            CorrectAnswer = 1,
        },
        new()
        {
            Id = 3,
            Question = "What is the best programming language?",
            Answers =
            [
                new()
                {
                    Id = 1,
                    Answer = "Python",
                },
                new()
                {
                    Id = 2,
                    Answer = "Javascript",
                },
                new()
                {
                    Id = 3,
                    Answer = "Csharp",
                },
                new()
                {
                    Id = 4,
                    Answer = "Java",
                },
            ],
            CorrectAnswer = 3,
        },
    ];
}
