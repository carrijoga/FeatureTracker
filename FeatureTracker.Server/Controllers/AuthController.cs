using FeatureTracker.Application.Authentication;
using FeatureTracker.Shared.Account;
using FeatureTracker.Shared.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = FeatureTracker.Shared.Security.ApplicationException;

namespace FeatureTracker.Server.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    #region Proprieties

    private readonly AuthApplication _authApplication;

    #endregion

    #region Constructor

    public AuthController(AuthApplication authApplication) =>
        _authApplication = authApplication;

    #endregion

    #region Endpoints

    /// <summary>
    ///     Authenticates a user and returns user authentication information
    /// </summary>
    /// <param name="userAuth">User credentials containing email/username and password</param>
    /// <returns>User authentication information including token</returns>
    /// <response code="200">Returns the user's authentication information</response>
    /// <response code="400">If the credentials are invalid</response>
    [HttpPost(nameof(Login))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserAuthInfo), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserAuthInfo>> Login([FromBody] UserAuth userAuth)
    {
        ArgumentNullException.ThrowIfNull(userAuth);

        try
        {
            return Ok(await _authApplication.LoginAsync(
                userAuth.Email,
                userAuth.Username,
                userAuth.Password));
        }
        catch (ApplicationException ex)
        {
            return StatusCode(ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred."); 
        }
    }

    /// <summary>
    ///     Registers a new user in the system.
    ///     Required Authorization.
    /// </summary>
    /// <param name="userRegister">User registration information</param>
    /// <returns>True if registration is successful</returns>
    /// <response code="200">Returns true if registration is successful</response>
    /// <response code="400">If the registration fails</response>
    [HttpPost(nameof(Register))]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<bool>> Register([FromBody] UserRegisterViewModel userRegister)
    {
        try
        {
            return Ok(await _authApplication.RegisterAsync(userRegister));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Initiates password recovery process for a user
    /// </summary>
    /// <param name="passwordRecoveryDto">Email address for password recovery</param>
    /// <returns>True if recovery email was sent successfully</returns>
    /// <response code="200">Returns true if recovery email was sent</response>
    /// <response code="400">If the recovery process fails</response>
    [HttpPost(nameof(RecoverPassword))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RecoverPassword([FromQuery] string email)
    {
        try
        {
            return Ok(await _authApplication.RecoverPasswordAsync(email).ConfigureAwait(false));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Checks if a username is available
    /// </summary>
    /// <param name="username">Username to check</param>
    /// <returns>True if username is available</returns>
    /// <response code="200">Returns true if username is available</response>
    /// <response code="400">If the check fails</response>
    [HttpGet(nameof(CheckUsername))]
    [AllowAnonymous]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckUsername([FromQuery] string username)
    {
        try
        {
            return Ok(await _authApplication.CheckUsername(username));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    ///     Validates if a token is still valid
    /// </summary>
    /// <param name="token">Token to validate</param>
    /// <returns>True if token is valid</returns>
    /// <response code="200">Returns true if token is valid</response>
    /// <response code="400">If validation fails</response>
    //[HttpPost(nameof(ValidateToken))]
    //[AllowAnonymous]
    //[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    //public ActionResult<bool> ValidateToken([FromBody] string token)
    //{
    //    try
    //    {
    //        return Ok(_authApplication.Validate(token));
    //    }
    //    catch (Exception ex)
    //    {
    //        return BadRequest(ex.Message);
    //    }
    //}

    #endregion
}
