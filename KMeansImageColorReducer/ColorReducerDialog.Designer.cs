namespace KMeansImageColorReducer
{
    partial class ColorReducerDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            mainTableLayoutPanel = new TableLayoutPanel();
            okButton = new Button();
            labelNumColors = new Label();
            labelNumCycles = new Label();
            numColors = new NumericUpDown();
            numCycles = new NumericUpDown();
            mainTableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numColors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCycles).BeginInit();
            SuspendLayout();
            // 
            // mainTableLayoutPanel
            // 
            mainTableLayoutPanel.AutoSize = true;
            mainTableLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            mainTableLayoutPanel.ColumnCount = 2;
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainTableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            mainTableLayoutPanel.Controls.Add(okButton, 1, 2);
            mainTableLayoutPanel.Controls.Add(labelNumColors, 0, 0);
            mainTableLayoutPanel.Controls.Add(labelNumCycles, 0, 1);
            mainTableLayoutPanel.Controls.Add(numColors, 1, 0);
            mainTableLayoutPanel.Controls.Add(numCycles, 1, 1);
            mainTableLayoutPanel.Dock = DockStyle.Fill;
            mainTableLayoutPanel.Location = new Point(0, 0);
            mainTableLayoutPanel.Margin = new Padding(0);
            mainTableLayoutPanel.Name = "mainTableLayoutPanel";
            mainTableLayoutPanel.RowCount = 3;
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            mainTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            mainTableLayoutPanel.Size = new Size(304, 141);
            mainTableLayoutPanel.TabIndex = 0;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Right;
            okButton.AutoSize = true;
            okButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            okButton.FlatAppearance.BorderSize = 0;
            okButton.FlatStyle = FlatStyle.Flat;
            okButton.Location = new Point(270, 108);
            okButton.Margin = new Padding(3, 3, 8, 3);
            okButton.Name = "okButton";
            okButton.Size = new Size(26, 25);
            okButton.TabIndex = 0;
            okButton.Text = "✓";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += OkButton_Click;
            // 
            // labelNumColors
            // 
            labelNumColors.AutoSize = true;
            labelNumColors.Dock = DockStyle.Fill;
            labelNumColors.Location = new Point(3, 3);
            labelNumColors.Margin = new Padding(3);
            labelNumColors.Name = "labelNumColors";
            labelNumColors.Size = new Size(176, 44);
            labelNumColors.TabIndex = 1;
            labelNumColors.Text = "Number of colors:";
            labelNumColors.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // labelNumCycles
            // 
            labelNumCycles.AutoSize = true;
            labelNumCycles.Dock = DockStyle.Fill;
            labelNumCycles.Location = new Point(3, 53);
            labelNumCycles.Margin = new Padding(3);
            labelNumCycles.Name = "labelNumCycles";
            labelNumCycles.Size = new Size(176, 44);
            labelNumCycles.TabIndex = 2;
            labelNumCycles.Text = "Number of k-mean cycles:";
            labelNumCycles.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // numColors
            // 
            numColors.Anchor = AnchorStyles.None;
            numColors.BorderStyle = BorderStyle.None;
            numColors.Location = new Point(203, 15);
            numColors.Maximum = new decimal(new int[] { 2048, 0, 0, 0 });
            numColors.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numColors.Name = "numColors";
            numColors.Size = new Size(80, 19);
            numColors.TabIndex = 3;
            numColors.TextAlign = HorizontalAlignment.Center;
            numColors.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // numCycles
            // 
            numCycles.Anchor = AnchorStyles.None;
            numCycles.BorderStyle = BorderStyle.None;
            numCycles.Location = new Point(203, 65);
            numCycles.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numCycles.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCycles.Name = "numCycles";
            numCycles.Size = new Size(80, 19);
            numCycles.TabIndex = 4;
            numCycles.TextAlign = HorizontalAlignment.Center;
            numCycles.Value = new decimal(new int[] { 14, 0, 0, 0 });
            // 
            // ColorReducerDialog
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(304, 141);
            Controls.Add(mainTableLayoutPanel);
            ForeColor = Color.Black;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(320, 180);
            Name = "ColorReducerDialog";
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Color Reducer";
            TopMost = true;
            mainTableLayoutPanel.ResumeLayout(false);
            mainTableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numColors).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCycles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel mainTableLayoutPanel;
        private Button okButton;
        private Label labelNumColors;
        private Label labelNumCycles;
        public NumericUpDown numColors;
        public NumericUpDown numCycles;
    }
}