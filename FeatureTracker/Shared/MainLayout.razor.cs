using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;

namespace FeatureTracker.Client.Shared;

public partial class MainLayoutBase : LayoutComponentBase
{
    #region Inject
    [Inject] protected IJSRuntime Js { get; set; }
    #endregion

    #region Properties
    protected bool _isDarkMode;
    protected bool _isLoading = true;
    #endregion

    #region Methods
    protected override async Task OnInitializedAsync()
    {
        var isDarkModeString = await Js.InvokeAsync<string>("localStorage.getItem", "isDarkMode");

        if (bool.TryParse(isDarkModeString, out var isDarkMode))
            _isDarkMode = isDarkMode;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        _isLoading = false;
    }

    protected async Task ToggleDarkMode()
    {
        _isDarkMode = !_isDarkMode;
        await Js.InvokeVoidAsync("localStorage.setItem", "isDarkMode", _isDarkMode.ToString());
    }

    #endregion

    #region Theme

    protected MudTheme _theme = new()
    {
        PaletteDark = new PaletteDark()
        {
            Primary = "#377D7D", // Sua cor base
            Secondary = "#004647", // Hover ou botões secundários
            Background = "#121212", // Fundo geral (neutral dark)
            Surface = "#1E1E1E", // Fundo dos cards/forms (cinza neutro)
            AppbarBackground = "#1E1E1E",
            DrawerBackground = "#1E1E1E",
            DrawerText = "#E5E5E5", // Texto claro
            DrawerIcon = "#E5E5E5",

            TextPrimary = "#E5E5E5", // Quase branco
            TextSecondary = "#A3A3A3", // Cinza médio (neutral-400)
            ActionDefault = "#737373", // neutral-500
            ActionDisabled = "#404040", // neutral-700

            Success = "#22C55E", // verde suave
            Warning = "#F59E0B", // amarelo suave
            Error = "#EF4444", // vermelho
            Info = "#60A5FA", // azul suave
        },

        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                FontFamily = new[] { "Segoe UI", "Roboto", "Helvetica", "Arial", "sans-serif" },
            }
        },

        LayoutProperties = new LayoutProperties()
        {
            DefaultBorderRadius = "8px"
        }
    };

    MudTheme _defaultTheme = new()
    {
        PaletteLight = new PaletteLight
        {
            AppbarBackground = "#004647",
            AppbarText = Colors.Shades.White,

            Primary = Colors.Cyan.Darken4,
            Secondary = Colors.Cyan.Darken1,

            Tertiary = "#377D7D",
            Surface = "#FFFFFF"
        }
    };

    #endregion
}
