namespace KMeansImageColorReducer;

/// <summary>
///     Custom Windows progress bar with color support.
/// </summary>
public class CustomProgressBar : ProgressBar
{
    /// <summary>
    ///     Create a new instance of <see cref="CustomProgressBar" />.
    /// </summary>
    public CustomProgressBar()
    {
        // Enable double buffering
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
    }

    /// <summary>
    ///     Event that's raised when this control should be drawn to the screen.
    /// </summary>
    protected override void OnPaint(PaintEventArgs e)
    {
        // Background
        e.Graphics.FillRectangle(new SolidBrush(BackColor), e.ClipRectangle);

        // Calculate progress
        double progress = (double)(Value - Minimum) / (Maximum - Minimum);
        progress = Math.Min(Math.Max(progress, 0), 1);

        // Visualize progress
        e.Graphics.FillRectangle(new SolidBrush(ForeColor), 0, 0, (int)(progress * e.ClipRectangle.Width),
            e.ClipRectangle.Height);
    }
}