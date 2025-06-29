using FeatureTracker.Application.Requests;
using FeatureTracker.Domain.View.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureTracker.Server.Controllers.Request
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        #region Dependencies
        private readonly RequestApplication _requestApplication;
        #endregion

        #region Constructor
        public RequestController(RequestApplication requestApplication)
        {
            _requestApplication = requestApplication;
        }
        #endregion

        #region Endpoints
        [HttpGet(nameof(GetRequestAsync))]
        public async Task<ActionResult<RequestView>> GetRequestAsync([FromQuery] int requestId)
        {
            try
            {
                return Ok(await _requestApplication.GetRequestAsync(requestId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
