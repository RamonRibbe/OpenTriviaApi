using OpenTriviaApi.Models;

namespace OpenTriviaApi.Repository;

public interface ITriviaRepository
{
    Task<List<TriviaQuestion>> GetQuestionsAsync();
}
