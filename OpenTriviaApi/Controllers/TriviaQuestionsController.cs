using Microsoft.AspNetCore.Mvc;
using OpenTriviaApi.Models;
using OpenTriviaApi.Repository;
using System.Net;

namespace OpenTriviaApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TriviaQuestionsController : ControllerBase
{
    private readonly ILogger<TriviaQuestionsController> logger;
    private readonly ITriviaRepository triviaRepository;

    public TriviaQuestionsController(
        ILogger<TriviaQuestionsController> logger,
        ITriviaRepository triviaRepository)
    {
        this.logger = logger;
        this.triviaRepository = triviaRepository;
    }

    [HttpGet(Name = "GetTriviaQuestions")]
    [ProducesResponseType(typeof(TriviaQuestion[]), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        try
        {
            this.logger.LogDebug("GetTriviaQuestions");

            var result = await this.triviaRepository.GetQuestionsAsync();

            return Ok(result.ToArray());
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, ex.Message);

            return BadRequest(ex.Message);
        }
    }
}
