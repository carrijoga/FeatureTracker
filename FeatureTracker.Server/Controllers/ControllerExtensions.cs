using FeatureTracker.Domain.Model.Users;
using Microsoft.AspNetCore.Mvc;

namespace FeatureTracker.Server.Controllers;

public static class ControllerExtensions
{
    public static string? GetUserAuthenticatedId(this ControllerBase controller)
    {
        if (controller.User.Identity?.IsAuthenticated == true)
            return controller.User.FindFirst(System.Security.Claims.ClaimTypes.Sid)?.Value;

        return null;
    }
}
