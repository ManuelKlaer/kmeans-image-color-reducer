using System.ComponentModel;
using _42Entwickler.ImageLib;
using Dark.Net;

namespace KMeansImageColorReducer;

/// <summary>
///     Main-Window
/// </summary>
public partial class KMeansImageColorReducer : Form
{
    // Declaration of a CustomProgressBar
    private readonly CustomProgressBar _progressBar = new();
    private Image? _originalImage;

    // Variables to store the original image
    private Pixel[,]? _originalImagePixels;
    private Image? _reducedImage;

    // Variables to store the color-reduced image
    private Pixel[,]? _reducedImagePixels;

    /// <summary>
    ///     Create a new instance of <see cref="KMeansImageColorReducer" />.
    /// </summary>
    public KMeansImageColorReducer()
    {
        InitializeComponent();

        // Enable double buffering
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);

        // Add the custom progress bar to the main table layout
        _progressBar.Dock = DockStyle.Fill;
        _progressBar.Margin = Padding.Empty;
        mainTableLayoutPanel.Controls.Add(_progressBar, 0, 1);

        // Use a custom renderer for the menu strip to provide dark-mode
        menuStrip1.Renderer = new CustomMenuStripRenderer();

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

        menuStrip1.BackColor = isDarkTheme ? Color.FromArgb(32, 32, 32) : Color.FromArgb(243, 243, 243);
        menuStrip1.ForeColor = ForeColor;

        fileToolStripMenuItem.BackColor = menuStrip1.BackColor;
        fileToolStripMenuItem.ForeColor = ForeColor;

        openToolStripMenuItem.BackColor = menuStrip1.BackColor;
        openToolStripMenuItem.ForeColor = ForeColor;
        saveToolStripMenuItem.BackColor = menuStrip1.BackColor;
        saveToolStripMenuItem.ForeColor = ForeColor;
        exitToolStripMenuItem.BackColor = menuStrip1.BackColor;
        exitToolStripMenuItem.ForeColor = ForeColor;

        imageToolStripMenuItem.BackColor = menuStrip1.BackColor;
        imageToolStripMenuItem.ForeColor = ForeColor;

        reduceColorsToolStripMenuItem.BackColor = menuStrip1.BackColor;
        reduceColorsToolStripMenuItem.ForeColor = ForeColor;

        _progressBar.BackColor = menuStrip1.BackColor;
        _progressBar.ForeColor = Color.DeepSkyBlue;
    }

    /// <summary>
    ///     Load / Open a new image.
    /// </summary>
    private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Check if the image is currently being processed
        if (imageReducerBgWorker.IsBusy)
        {
            MessageBox.Show(@"The image is currently being processed. Wait until it's finished. Then try again.",
                @"Open", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Ask what image file to load
        if (openImageDialog.ShowDialog() != DialogResult.OK) return;

        // Load the selected image
        LoadImage(openImageDialog.FileName);
    }

    /// <summary>
    ///     Save the color-reduced image.
    /// </summary>
    private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Check if a color-reduced image is available
        if (_reducedImagePixels is null)
        {
            MessageBox.Show(@"Please run the color reducer first. Then try again.", @"Save", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        // Ask where to save the image
        if (saveImageDialog.ShowDialog() != DialogResult.OK) return;

        // Save the image
        _reducedImage?.Save(saveImageDialog.FileName);
    }

    /// <summary>
    ///     Run the color reducer.
    /// </summary>
    private void ReduceColorsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // Check if an image was opened / loaded
        if (_originalImagePixels is null)
        {
            MessageBox.Show(@"Please open an image first. Then try again.", @"Reduce colors", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            return;
        }

        // Check if the image is already being processed
        if (imageReducerBgWorker.IsBusy)
        {
            MessageBox.Show(@"The image is already being processed. Wait until it's finished. Then try again.",
                @"Reduce colors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // Show color reducer settings
        DarkNet.Instance.SetWindowThemeForms(Program.ColorReducerDialog, Program.Theme, Program.ThemeOptions);
        if (Program.ColorReducerDialog.ShowDialog(this) != DialogResult.OK) return;

        // Run the image color reducer
        imageReducerBgWorker.RunWorkerAsync((_originalImagePixels, (uint)Program.ColorReducerDialog.numColors.Value,
            (uint)Program.ColorReducerDialog.numCycles.Value));
    }

    /// <summary>
    ///     Quit / Exit the application.
    /// </summary>
    private void ExitToolStripMenuItem_Click(object sender, EventArgs e) => Application.Exit();

    /// <summary>
    ///     Event that's raised when the window size changed.
    /// </summary>
    private void KMeansImageCompression_ClientSizeChanged(object sender, EventArgs e) => UpdateImagePreview();

    /// <summary>
    ///     Event that's raised when the splitter was moved.
    /// </summary>
    private void ImageSplitContainer_SplitterMoved(object sender, SplitterEventArgs e) => UpdateImagePreview();

    /// <summary>
    ///     Load an image from its path.
    /// </summary>
    /// <param name="imagePath">The path to the image.</param>
    /// <exception cref="ArgumentException">Image doesn't exist.</exception>
    private void LoadImage(string imagePath)
    {
        // Check if the provided image path is valid
        if (!File.Exists(imagePath))
            throw new ArgumentException($@"Image ('{imagePath}') doesn't exist", nameof(imagePath));

        // Load the picture
        _originalImagePixels = KMeansColorReducer.ConvertImageTo2DPixelArray(ArrayImage.ReadAs3DArray(imagePath));
        _reducedImagePixels = null;

        // Show image preview
        UpdateImagePreview(true);

        // Reset progress bar
        _progressBar.Value = 0;
    }

    /// <summary>
    ///     Update the image preview.
    /// </summary>
    /// <param name="rebuildCache">Whether to rebuild the image cache. Required if the images changed.</param>
    private void UpdateImagePreview(bool rebuildCache = false)
    {
        // # --- Hide images if they don't exist --- #

        if (_originalImagePixels == null)
        {
            pictureBox1.Image = null;
            pictureBox2.Image = null;
        }
        else if (_reducedImagePixels == null)
        {
            pictureBox2.Image = null;
        }

        // # --- Exit if there's no image to preview --- #

        if (_originalImagePixels == null) return;

        // # --- Calculate display size --- #

        // Calculate available image display size
        Rectangle displaySize = new(0, 0, pictureBox1.Width + imageSplitContainer.SplitterWidth + pictureBox2.Width,
            Math.Min(pictureBox1.Height, pictureBox2.Height));

        // # --- SplitContainer: Splitter --- #

        // If no reduced image is available, show the whole original image
        if (_reducedImagePixels == null && !imageSplitContainer.IsSplitterFixed)
        {
            imageSplitContainer.SplitterDistance = displaySize.Width - imageSplitContainer.SplitterWidth - 1;
            imageSplitContainer.IsSplitterFixed = true;
        }

        // If a reduced image is available, move the splitter to the middle
        if (_reducedImagePixels != null && imageSplitContainer.IsSplitterFixed)
        {
            imageSplitContainer.IsSplitterFixed = false;
            imageSplitContainer.SplitterDistance = (displaySize.Width - imageSplitContainer.SplitterWidth) / 2;
        }

        // # --- Calculate image coordinates and size --- #

        // Calculate image to screen ratio
        double widthDisplayRatio = (double)_originalImagePixels.GetLength(0) / displaySize.Width;
        double heightDisplayRatio = (double)_originalImagePixels.GetLength(1) / displaySize.Height;

        // Calculate image ratio
        double imageRatio = (double)_originalImagePixels.GetLength(0) / _originalImagePixels.GetLength(1);

        // Calculate theoretical image size
        Rectangle theoreticalImage;

        if (heightDisplayRatio > widthDisplayRatio)
        {
            // Scale image to match the height
            int newWidth = (int)(displaySize.Height * imageRatio);
            int newX = (displaySize.Width - newWidth) / 2;

            theoreticalImage = new Rectangle(newX, 0, newWidth, displaySize.Height);
        }
        else
        {
            // Scale image to match the width
            int newHeight = (int)(displaySize.Width / imageRatio);
            int newY = (displaySize.Height - newHeight) / 2;

            theoreticalImage = new Rectangle(0, newY, displaySize.Width, newHeight);
        }

        // # --- Original image preview --- #

        // Rebuild cache
        if (rebuildCache)
        {
            _originalImage?.Dispose();
            _originalImage = null;
        }

        // Cache the image for later use if it's not already cached
        _originalImage ??= ArrayImage.CreateImage(KMeansColorReducer.ConvertImageTo3DByteArray(_originalImagePixels));

        // Create the original image preview
        Bitmap previewBitmap = new(pictureBox1.Width, pictureBox1.Height);
        Graphics previewGraphics = Graphics.FromImage(previewBitmap);

        previewGraphics.DrawImage(_originalImage, new PointF[]
        {
            new(theoreticalImage.X, theoreticalImage.Y),
            new(theoreticalImage.X + theoreticalImage.Width, theoreticalImage.Y),
            new(theoreticalImage.X, theoreticalImage.Y + theoreticalImage.Height)
        });

        // Save the current image for later disposal
        Image? oldImage = pictureBox1.Image;

        // Show the new image
        pictureBox1.Image = previewBitmap;

        // Cleanup
        oldImage?.Dispose();
        previewGraphics.Dispose();

        // # --- Reduced image preview --- #

        // Check if there's a color-reduced image available, if not return
        if (_reducedImagePixels == null) return;

        // Rebuild cache
        if (rebuildCache)
        {
            _reducedImage?.Dispose();
            _reducedImage = null;
        }

        // Cache the image for later use
        _reducedImage ??= ArrayImage.CreateImage(KMeansColorReducer.ConvertImageTo3DByteArray(_reducedImagePixels));

        // Create the reduced image preview
        Bitmap previewReducedBitmap = new(pictureBox2.Width, pictureBox2.Height);
        Graphics previewReducedGraphics = Graphics.FromImage(previewReducedBitmap);

        previewReducedGraphics.DrawImage(_reducedImage, new PointF[]
        {
            new(pictureBox2.Width - theoreticalImage.X - theoreticalImage.Width, theoreticalImage.Y),
            new(pictureBox2.Width - theoreticalImage.X, theoreticalImage.Y),
            new(pictureBox2.Width - theoreticalImage.X - theoreticalImage.Width,
                theoreticalImage.Y + theoreticalImage.Height)
        });

        // Save the current image for later disposal
        Image? oldReducedImage = pictureBox2.Image;

        // Show the new picture
        pictureBox2.Image = previewReducedBitmap;

        // Cleanup
        oldReducedImage?.Dispose();
        previewReducedGraphics.Dispose();
    }

    /// <summary>
    ///     BackgroundWorker Thread. Uses the <see cref="KMeansColorReducer" /> class to reduce the image colors.
    /// </summary>
    private void ImageReducerBgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
        // Get the BackgroundWorker that raised this event
        if (sender is not BackgroundWorker worker) return;

        // Extract the arguments
        var (image, centroids, iterations) = ((Pixel[,] image, uint centroids, uint iterations))e.Argument!;

        // Temporary variables to store the current progress
        uint maxProgress = iterations * centroids + centroids;
        uint iterationProgress = 0;
        uint centroidProgress = 0;

        // Function to update the iteration progress
        void UpdateIterationProgress(object? sender, uint iteration)
        {
            iterationProgress = iteration;
        }

        // Function to update the centroid progress and report the overall progress back
        void UpdateCentroidProgress(object? sender, uint centroid)
        {
            centroidProgress = centroid;

            // Calculate overall progress and report it back
            uint progress = iterationProgress * centroids + centroidProgress;
            worker.ReportProgress((int)(100 * progress / maxProgress));
        }

        // Register progress event handlers
        KMeansColorReducer.NextIteration += UpdateIterationProgress;
        KMeansColorReducer.NextCentroid += UpdateCentroidProgress;

        // Reduce image colors
        e.Result = KMeansColorReducer.ReduceImageColors(image, centroids, iterations);

        // Unregister progress event handlers
        KMeansColorReducer.NextIteration -= UpdateIterationProgress;
        KMeansColorReducer.NextCentroid -= UpdateCentroidProgress;
    }

    /// <summary>
    ///     Event that's raised when the progress of the BackgroundWorker changed.
    /// </summary>
    private void ImageReducerBgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        _progressBar.Value = Math.Min(Math.Max(e.ProgressPercentage, 0), 100);
    }

    /// <summary>
    ///     Event that's raised when the BackgroundWorker finished reducing the image colors.
    /// </summary>
    private void ImageReducerBgWorker_WorkCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // Save the reduced image
        _reducedImagePixels = (Pixel[,])e.Result!;

        // Update image preview
        UpdateImagePreview(true);
    }

    /// <summary>
    ///     Custom tool strip professional renderer with dark-mode support.
    /// </summary>
    private class CustomMenuStripRenderer : ToolStripProfessionalRenderer
    {
        /// <summary>
        ///     Render the menu item background with dark-mode support.
        /// </summary>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rc = new(Point.Empty, e.Item.Size);
            Color c = e.Item.Selected ? ColorUtils.ChangeColorBrightness2(e.Item.BackColor, 0.1f) : e.Item.BackColor;
            using SolidBrush brush = new(c);
            e.Graphics.FillRectangle(brush, rc);
        }
    }
}