using FeatureTracker.Client.Services.Authentication;
using FeatureTracker.Shared.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace FeatureTracker.Client.Pages.Authentication;

public class RegisterBase : ComponentBase
{
    #region Inject
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IAuthenticationService authenticationService { get; set; }
    #endregion

    #region Parameters
    [Parameter]
    [SupplyParameterFromQuery]
    public string? Token { get; set; }
    #endregion

    #region Properties
    protected MudForm _form;
    protected bool _isValid;
    protected bool _passwordVisible;
    protected InputType _passwordInput = InputType.Password;
    protected string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected string MessageValidateEmail = "";
    protected bool ErrorValidateEmail;
    protected UserRegisterViewModel ViewModelRegister;
    #endregion

    #region Constructor
    public RegisterBase()
    {
        ViewModelRegister = new();
    }
    #endregion

    #region Methods
    protected override async Task OnInitializedAsync()
    {
        Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;

        await ValidateTokenAsync();
    }

    private async Task ValidateTokenAsync()
    {
        try
        {
            ViewModelRegister = await authenticationService.ValidateTokenAsync(Token);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
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

    protected async Task HandleRegister()
    {
        try
        {
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    protected async Task EnterPressed(KeyboardEventArgs args)
    {
        if (args.Code == "Enter" || args.Code == "NumpadEnter"
            && _form.IsValid)
        {
            _ = HandleRegister();
        }
    }
    #endregion
}
