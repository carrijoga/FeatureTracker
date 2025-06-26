using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using FeatureTracker.Client.Utils;
using FeatureTracker.Shared.Account;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;

namespace FeatureTracker.Client.Services.Authentication;

public class AuthenticationService : AuthenticationStateProvider, IAuthenticationService
{
    #region Properties

    private readonly IJSRuntime _js;
    public readonly HttpClient httpClient;
    private const string authToken = nameof(authToken);

    private static AuthenticationState NotAuthenticate =>
        new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

    #endregion

    #region Constructor

    public AuthenticationService(IJSRuntime js, HttpClient httpClient)
    {
        this._js = js;
        this.httpClient = httpClient;
    }
    #endregion

    #region Methods
    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _js.GetFromLocalStorage(authToken);

        if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
        {
            await Logout();
            return NotAuthenticate;
        }

        return await AuthenticateUser(token);
    }

    private async Task<AuthenticationState> AuthenticateUser(string token)
    {
        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        keyValuePairs.TryGetValue("role", out object roles);

        if (roles != null)
        {
            if (roles.ToString().Trim().StartsWith("["))
            {
                var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                foreach (var parsedRole in parsedRoles)
                    claims.Add(new Claim(ClaimTypes.Role, parsedRole));
            }
            else
                claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));

            keyValuePairs.Remove(ClaimTypes.Role);
        }

        //claims = MontarTelasRoles(claims);

        claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

        return claims;
    }

    private byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }

    private bool IsTokenExpired(string token)
    {
        var payload = token.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs.TryGetValue("exp", out var expValue))
        {
            long exp;
            if (expValue is JsonElement jsonElement)
            {
                if (jsonElement.ValueKind == JsonValueKind.Number && jsonElement.TryGetInt64(out exp))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                    return expDate < DateTime.UtcNow;
                }
                else if (jsonElement.ValueKind == JsonValueKind.String && long.TryParse(jsonElement.GetString(), out exp))
                {
                    var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                    return expDate < DateTime.UtcNow;
                }
            }
            else if (expValue is long l)
            {
                exp = l;
                var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                return expDate < DateTime.UtcNow;
            }
            else if (expValue is string s && long.TryParse(s, out exp))
            {
                var expDate = DateTimeOffset.FromUnixTimeSeconds(exp).UtcDateTime;
                return expDate < DateTime.UtcNow;
            }
        }
        return true; // Se não encontrar o campo exp, considera expirado
    }

    public async Task<UserAuthInfo> Login(UserAuth userAuth)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/v1/Auth/Login", userAuth);

        if (Equals(httpResponse.StatusCode, HttpStatusCode.OK))
        {
            var userAuthInfo = await httpResponse.Content.ReadFromJsonAsync<UserAuthInfo>();

            if (userAuthInfo is not null)
            {
                await _js.SetInLocalStorage(authToken, userAuthInfo.Token);
                NotifyAuthenticationStateChanged(Task.FromResult(await AuthenticateUser(userAuthInfo.Token)));

                return userAuthInfo;
            }

            return new UserAuthInfo();
        }
        else
        {
            var msgError = await httpResponse.Content.ReadAsStringAsync();

            throw new Exception(msgError);
        }
    }

    public async Task<bool> Register(UserRegister userRegister)
    {
        var httpResponse = await httpClient.PostAsJsonAsync("api/v1/Auth/Register", userRegister);

        if (Equals(httpResponse.StatusCode, HttpStatusCode.OK))
            return await httpResponse.Content.ReadFromJsonAsync<bool>();
        else
        {
            var msgError = string.Empty;

            if (Equals(httpResponse.StatusCode, HttpStatusCode.BadRequest))
                msgError = await httpResponse.Content.ReadAsStringAsync();

            throw new Exception(msgError);
        }
    }

    public async Task Logout()
    {
        await _js.RemoveItem(authToken);

        httpClient.DefaultRequestHeaders.Authorization = null;
        NotifyAuthenticationStateChanged(Task.FromResult(NotAuthenticate));
    }
    #endregion
}
