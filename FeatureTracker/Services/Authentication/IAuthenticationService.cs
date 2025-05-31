using FeatureTracker.Shared.Account;

namespace FeatureTracker.Client.Services.Authentication;

public interface IAuthenticationService
{
    Task<UserAuthInfo> Login(UserAuth userAuth);

    Task<bool> Register(UserRegister userRegister);
}
