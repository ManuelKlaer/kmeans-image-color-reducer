using Dark.Net;

// ReSharper disable MemberCanBePrivate.Global

namespace KMeansImageColorReducer;

/// <summary>
///     Entrypoint for this application and settings.
/// </summary>
internal static class Program
{
    // Application windows
    public static readonly KMeansImageColorReducer MainForm = new();
    public static readonly ColorReducerDialog ColorReducerDialog = new();

    // Application theme settings
    public static readonly Theme Theme = Theme.Auto;
    public static readonly ThemeOptions ThemeOptions = new() { WindowBorderColor = ThemeOptions.NoWindowBorder };

    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // Initialize forms application
        ApplicationConfiguration.Initialize();

        // Initialize theming engine
        DarkNet.Instance.SetCurrentProcessTheme(Theme, ThemeOptions);

        // Set the theme to use
        DarkNet.Instance.SetWindowThemeForms(MainForm, Theme, ThemeOptions);
        DarkNet.Instance.SetWindowThemeForms(ColorReducerDialog, Theme, ThemeOptions);

        // Show the main form
        Application.Run(MainForm);
    }
}