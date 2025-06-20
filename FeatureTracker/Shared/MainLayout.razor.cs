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
    protected bool _isOpen = true;
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

    protected async Task ToggleMenuBar()
    {
        _isOpen = !_isOpen;
    }

    #endregion

    #region Theme

    protected MudTheme _theme = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = "#377D7D",            // Cor principal (verde petróleo)
            Secondary = "#004647",          // Cor secundária / botões secundários
            Background = "#F8F8FF",         // Fundo geral (quase branco azulado)
            Surface = "#FAFAFA",            // Superfície de cards, inputs etc
            AppbarBackground = "#1E1E1E",   // AppBar escura para contraste
            AppbarText = "#E5E5E5",         // Texto claro na AppBar
            DrawerBackground = "#FAFAFA",   // Menu lateral escuro
            DrawerText = "#100C08",         // Texto claro no Drawer
            DrawerIcon = "#100C08",         // Ícones claros no Drawer

            TextPrimary = "#100C08",        // Cor de texto principal (quase preto)
            TextSecondary = "#A3A3A3",      // Cor de texto secundário (cinza médio)

            ActionDefault = "#737373",      // Cor padrão de ações
            ActionDisabled = "#404040",     // Cor de elementos desativados
            ActionDisabledBackground = "#E0E0E0", // Fundo de elementos desativados

            Divider = "#E0E0E0",            // Linhas divisórias
            LinesDefault = "#E0E0E0",       // Bordas padrão
            LinesInputs = "#D1D5DB",        // Bordas de inputs

            TableLines = "#E5E7EB",         // Linhas de tabelas
            TableStriped = "#F3F4F6",       // Linhas alternadas de tabela

            Success = "#22C55E",            // Verde (sucesso)
            Warning = "#F59E0B",            // Amarelo (aviso)
            Error = "#EF4444",              // Vermelho (erro)
            Info = "#60A5FA",               // Azul (informação)

            OverlayDark = "rgba(33, 33, 33, 0.8)", // Overlay para diálogos, menus, etc.
        },

        PaletteDark = new PaletteDark()
        {
            Primary = "#377D7D",              // Verde petróleo
            Secondary = "#004647",            // Cor secundária mais escura
            Background = "#121212",           // Fundo principal bem escuro
            Surface = "#1E1E1E",              // Cards, inputs, containers
            AppbarBackground = "#1E1E1E",     // AppBar escura
            AppbarText = "#E5E5E5",           // Texto claro na AppBar
            DrawerBackground = "#1E1E1E",     // Drawer alinhado ao restante
            DrawerText = "#E5E5E5",           // Texto do menu
            DrawerIcon = "#E5E5E5",           // Ícones do menu

            TextPrimary = "#E5E5E5",          // Texto principal claro
            TextSecondary = "#A3A3A3",        // Texto secundário (cinza médio)

            ActionDefault = "#A3A3A3",        // Ações padrão (um pouco mais claras que no light)
            ActionDisabled = "#5A5A5A",       // Desabilitado (ajustado p/ melhor contraste)
            ActionDisabledBackground = "#2C2C2C",

            Divider = "#2C2C2C",              // Linhas divisórias discretas
            LinesDefault = "#2C2C2C",         // Bordas padrão
            LinesInputs = "#3A3A3A",          // Bordas de inputs

            TableLines = "#2F2F2F",           // Linhas de tabela discretas
            TableStriped = "#1A1A1A",         // Linhas alternadas para tabelas

            Success = "#22C55E",              // Verde suave
            Warning = "#FBBF24",              // Amarelo ajustado (mais vibrante que no light)
            Error = "#F87171",                // Vermelho mais suave
            Info = "#60A5FA",                 // Azul claro

            OverlayLight = "rgba(250,250,250, 0.05)", // Overlay para diálogos, etc.
        },

        //PaletteDark = new PaletteDark()
        //{
        //    Primary = "#377D7D", // Sua cor base
        //    Secondary = "#004647", // Hover ou botões secundários
        //    Background = "#121212", // Fundo geral (neutral dark)
        //    Surface = "#1E1E1E", // Fundo dos cards/forms (cinza neutro)
        //    AppbarBackground = "#1E1E1E",
        //    DrawerBackground = "#1E1E1E",
        //    DrawerText = "#E5E5E5", // Texto claro
        //    DrawerIcon = "#E5E5E5",

        //    TextPrimary = "#E5E5E5", // Quase branco
        //    TextSecondary = "#A3A3A3", // Cinza médio (neutral-400)
        //    ActionDefault = "#737373", // neutral-500
        //    ActionDisabled = "#404040", // neutral-700

        //    Success = "#22C55E", // verde suave
        //    Warning = "#F59E0B", // amarelo suave
        //    Error = "#EF4444", // vermelho
        //    Info = "#60A5FA", // azul suave
        //},

        Typography = new Typography()
        {
            Default = new DefaultTypography()
            {
                //FontFamily = ["GeistSans", "GeistSans Fallback", "Arial", "sans-serif"],
                FontFamily = ["Inter", "Segoe UI", "Roboto", "Helvetica", "Arial", "sans-serif"],
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
