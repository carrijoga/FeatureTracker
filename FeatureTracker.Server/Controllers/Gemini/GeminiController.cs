using FeatureTracker.Server.Services.Gemini;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureTracker.Server.Controllers.Gemini;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class GeminiController : ControllerBase
{
    #region Dependencies
    private readonly GeminiService _geminiService;
    #endregion

    #region Constructor
    public GeminiController(GeminiService geminiService)
    {
        _geminiService = geminiService;
    }
    #endregion

    #region Endpoints
    [HttpPost(nameof(GenerateTitle))]
    public async Task<ActionResult<string>> GenerateTitle([FromBody] string observation)
    {
        ArgumentNullException.ThrowIfNull(observation);

        try
        {
            return Ok(await _geminiService.GenerateTitle(observation));
        }
        catch (Exception ex)
        {
            return BadRequest($"Something went wrong, try again. {ex}");
        }
    }
    #endregion
}
