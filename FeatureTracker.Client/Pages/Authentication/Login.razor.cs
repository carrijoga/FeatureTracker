using FeatureTracker.Shared.Account;
using FeatureTracker.Client.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.Web;

namespace FeatureTracker.Client.Pages.Authentication;

public class LoginBase : ComponentBase
{
    #region Inject
    [Inject] public IAuthenticationService authenticationService { get; set; }
    [Inject] private HttpClient Http { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; }
    [Inject] private IJSRuntime JSRuntime { get; set; }
    #endregion

    #region Properties
    protected MudForm _form;
    protected bool _isValid;
    protected bool _OnLoading;
    protected UserAuth _userAuth = new();
    protected bool _passwordVisible;
    protected InputType _passwordInput = InputType.Password;
    protected string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string MessageValidateEmail = "";
    protected bool ErrorValidateEmail;
    private readonly string regexPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
    #endregion

    #region Methods
    protected override void OnInitialized() =>
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;

    protected async Task HandleLogin()
    {
        OnLoading(true);

        try
        {
            UserAuthInfo result = new();

            if (!ErrorValidateEmail)
                result = await authenticationService.Login(_userAuth);

            if (result.Token is not null)
                NavigationManager.NavigateTo("/");
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
            OnLoading(false);
        }

        OnLoading(false);
    }

    protected async Task ValidateEmail()
    {
        if (!string.IsNullOrEmpty(_userAuth.Email))
        {
            if (!Regex.IsMatch(_userAuth.Email, regexPattern))
            {
                ErrorValidateEmail = true;
                MessageValidateEmail = "Please enter a valid email address.";

                StateHasChanged();
            }
            else
            {
                ErrorValidateEmail = false;
                MessageValidateEmail = string.Empty;

                StateHasChanged();
            }
        }
        else
        {
            ErrorValidateEmail = true;
            MessageValidateEmail = "Email field cannot be empty.";

            StateHasChanged();
        }
    }

    protected void TogglePasswordVisibility()
    {
        if (_passwordVisible)
        {
            _passwordVisible = false;
            _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
            _passwordInput = InputType.Password;
        }
        else
        {
            _passwordVisible = true;
            _passwordInputIcon = Icons.Material.Filled.Visibility;
            _passwordInput = InputType.Text;
        }
    }

    protected async Task EnterPressed(KeyboardEventArgs args)
    {
        if (args.Code == "Enter" || args.Code == "NumpadEnter"
            && _form.IsValid)
        {
            _ = HandleLogin();
        }
    }

    private void OnLoading(bool v) =>
        _OnLoading = v;
    #endregion
}
