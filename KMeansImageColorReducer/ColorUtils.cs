namespace KMeansImageColorReducer;

public static class ColorUtils
{
    /// <summary>
    ///     Change a <see cref="Color" /> brightness by the specified percent.
    /// </summary>
    /// <param name="c">The color to modify.</param>
    /// <param name="percent">
    ///     The percent. From <see langword="-1.0" /> to <see langword="0.0" /> darken; From
    ///     <see langword="0.0" /> to <see langword="1.0" /> lighten;
    /// </param>
    /// <returns>The same color with a new brightness value.</returns>
    public static Color ChangeColorBrightness(Color c, float percent)
    {
        Color reference = Color.White;

        int r = (int)Math.Round(c.R + reference.R * percent);
        int g = (int)Math.Round(c.G + reference.G * percent);
        int b = (int)Math.Round(c.B + reference.B * percent);

        r = Math.Min(Math.Max(r, 0), 255);
        g = Math.Min(Math.Max(g, 0), 255);
        b = Math.Min(Math.Max(b, 0), 255);

        return Color.FromArgb(c.A, r, g, b);
    }

    /// <summary>
    ///     Change a <see cref="Color" /> brightness by the specified percent. If the specified color is dark, the new color
    ///     gets lighter; Otherwise, the new color gets darker;
    /// </summary>
    /// <param name="c">The color to modify.</param>
    /// <param name="percent">The percent.</param>
    /// <returns>The same color with a new brightness value.</returns>
    public static Color ChangeColorBrightness2(Color c, float percent) => PerceivedBrightness(c) <= 140 ? ChangeColorBrightness(c, percent) : ChangeColorBrightness(c, -percent);

    /// <summary>
    ///     Get the perceived color brightness to a human.
    /// </summary>
    /// <param name="c">The color to get the brightness from.</param>
    /// <returns>The perceived brightness; <see langword="0" /> black; <see langword="255" /> white;</returns>
    public static int PerceivedBrightness(Color c)
    {
        return (int)Math.Sqrt(
            c.R * c.R * 0.299 +
            c.G * c.G * 0.587 +
            c.B * c.B * 0.114);
    }
}
