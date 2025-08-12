using FeatureTracker.Shared.Account;
using FeatureTracker.Shared.ViewModel;

namespace FeatureTracker.Client.Services.Authentication;

public interface IAuthenticationService
{
    Task<UserAuthInfo> Login(UserAuth userAuth);

    Task<bool> Register(UserRegister userRegister);

    Task<UserRegisterViewModel> ValidateTokenAsync(string token);
}
