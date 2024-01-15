using Dark.Net;

namespace KMeansImageColorReducer;

/// <summary>
///     ColorReducerSettings-Dialog
/// </summary>
public partial class ColorReducerDialog : Form
{
    /// <summary>
    ///     Create a new instance of <see cref="ColorReducerDialog" />.
    /// </summary>
    public ColorReducerDialog()
    {
        InitializeComponent();

        // Initialize theming engine
        DarkNet.Instance.SetWindowThemeForms(this, Program.Theme, Program.ThemeOptions);
        DarkNet.Instance.EffectiveCurrentProcessThemeIsDarkChanged += (_, isDarkTheme) => RenderTheme(isDarkTheme);
        RenderTheme(DarkNet.Instance.EffectiveCurrentProcessThemeIsDark);
    }

    /// <summary>
    ///     Render the specified theme.
    /// </summary>
    /// <param name="isDarkTheme">Whether the theme should be dark or light.</param>
    private void RenderTheme(bool isDarkTheme)
    {
        BackColor = isDarkTheme ? Color.FromArgb(19, 19, 19) : Color.White;
        ForeColor = isDarkTheme ? Color.White : Color.Black;

        okButton.BackColor = isDarkTheme ? Color.FromArgb(32, 32, 32) : Color.FromArgb(243, 243, 243);
        okButton.ForeColor = ForeColor;
        okButton.FlatAppearance.BorderColor = okButton.BackColor;

        numColors.BackColor = okButton.BackColor;
        numColors.ForeColor = ForeColor;

        numCycles.BackColor = okButton.BackColor;
        numCycles.ForeColor = ForeColor;
    }

    /// <summary>
    ///     Event that's raised when the OK-Button was clicked.
    /// </summary>
    private void OkButton_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;
        Close();
    }
}