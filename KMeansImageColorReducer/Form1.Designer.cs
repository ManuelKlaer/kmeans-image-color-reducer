namespace KMeansImageCompression
{
    partial class KMeansImageColorReducer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            imageToolStripMenuItem = new ToolStripMenuItem();
            reduceColorsToolStripMenuItem = new ToolStripMenuItem();
            mainTableLayoutPanel = new TableLayoutPanel();
            imageSplitContainer = new SplitContainer();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            openImageDialog = new OpenFileDialog();
            imageReducerBgWorker = new System.ComponentModel.BackgroundWorker();
            saveImageDialog = new SaveFileDialog();
            menuStrip1.SuspendLayout();
            mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)imageSplitContainer).BeginInit();
            imageSplitContainer.Panel1.SuspendLayout();
            imageSplitContainer.Panel2.SuspendLayout();
            imageSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, imageToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(834, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, saveToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(148, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(148, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(148, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // imageToolStripMenuItem
            // 
            imageToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { reduceColorsToolStripMenuItem });
            imageToolStripMenuItem.Name = "imageToolStripMenuItem";
            imageToolStripMenuItem.Size = new Size(52, 20);
            imageToolStripMenuItem.Text = "Image";
            // 
            // reduceColorsToolStripMenuItem
            // 
            reduceColorsToolStripMenuItem.Name = "reduceColorsToolStripMenuItem";
            reduceColorsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            reduceColorsToolStripMenuItem.Size = new Size(191, 22);
            reduceColorsToolStripMenuItem.Text = "Reduce colors";
            reduceColorsToolStripMenuItem.Click += ReduceColorsToolStripMenuItem_Click;
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.AutoSize = true;
            mainTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainTableLayoutPanel.ColumnCount = 1;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            mainTableLayoutPanel.Controls.Add(imageSplitContainer, 0, 0);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 24);
            mainTableLayoutPanel.Margin = new Padding(0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 2;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 6F));
            mainTableLayoutPanel.Size = new Size(834, 437);
            mainTableLayoutPanel.TabIndex = 2;
            // 
            // imageSplitContainer
            // 
            imageSplitContainer.Dock = DockStyle.Fill;
            imageSplitContainer.Location = new Point(0, 0);
            imageSplitContainer.Margin = new Padding(0);
            imageSplitContainer.Name = "imageSplitContainer";
            // 
            // imageSplitContainer.Panel1
            // 
            imageSplitContainer.Panel1.Controls.Add(pictureBox1);
            imageSplitContainer.Panel1MinSize = 1;
            // 
            // imageSplitContainer.Panel2
            // 
            imageSplitContainer.Panel2.Controls.Add(pictureBox2);
            imageSplitContainer.Panel2MinSize = 1;
            imageSplitContainer.Size = new Size(834, 431);
            imageSplitContainer.SplitterDistance = 411;
            imageSplitContainer.SplitterWidth = 5;
            imageSplitContainer.TabIndex = 0;
            imageSplitContainer.SplitterMoved += ImageSplitContainer_SplitterMoved;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(411, 431);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(418, 431);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 0;
            pictureBox2.TabStop = false;
            // 
            // openImageDialog
            // 
            openImageDialog.Filter = "Image|*.png;*.jpg;*.jpeg|All files|*.*";
            openImageDialog.OkRequiresInteraction = true;
            openImageDialog.ShowPreview = true;
            openImageDialog.SupportMultiDottedExtensions = true;
            openImageDialog.Title = "Open image";
            // 
            // imageReducerBgWorker
            // 
            imageReducerBgWorker.WorkerReportsProgress = true;
            imageReducerBgWorker.DoWork += ImageReducerBgWorker_DoWork;
            imageReducerBgWorker.ProgressChanged += ImageReducerBgWorker_ProgressChanged;
            imageReducerBgWorker.RunWorkerCompleted += ImageReducerBgWorker_WorkCompleted;
            // 
            // saveImageDialog
            // 
            saveImageDialog.DefaultExt = "png";
            saveImageDialog.Filter = "Image|*.png;*.jpg;*.jpeg|All files|*.*";
            saveImageDialog.OkRequiresInteraction = true;
            saveImageDialog.Title = "Save reduced image";
            // 
            // KMeansImageColorReducer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(834, 461);
            Controls.Add(mainTableLayoutPanel);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            ForeColor = Color.Black;
            MainMenuStrip = menuStrip1;
            MinimumSize = new Size(850, 500);
            Name = "KMeansImageColorReducer";
            Text = "k-means Image Color Reducer";
            ClientSizeChanged += KMeansImageCompression_ClientSizeChanged;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            mainTableLayoutPanel.ResumeLayout(false);
            imageSplitContainer.Panel1.ResumeLayout(false);
            imageSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)imageSplitContainer).EndInit();
            imageSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private TableLayoutPanel mainTableLayoutPanel;
        private SplitContainer imageSplitContainer;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private ToolStripMenuItem openToolStripMenuItem;
        private OpenFileDialog openImageDialog;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem imageToolStripMenuItem;
        private System.ComponentModel.BackgroundWorker imageReducerBgWorker;
        private ToolStripMenuItem reduceColorsToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private SaveFileDialog saveImageDialog;
    }
}
