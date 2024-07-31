
namespace App
{
    partial class LoadingWindow
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
            this.components = new System.ComponentModel.Container();
            this.dataLoadingProgressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.dataLoading_clock = new System.Windows.Forms.Timer(this.components);
            this.guna2ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.roundEdge = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.SuspendLayout();
            // 
            // dataLoadingProgressBar
            // 
            this.dataLoadingProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataLoadingProgressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.dataLoadingProgressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.dataLoadingProgressBar.Location = new System.Drawing.Point(0, 303);
            this.dataLoadingProgressBar.Name = "dataLoadingProgressBar";
            this.dataLoadingProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(106)))), ((int)(((byte)(204)))));
            this.dataLoadingProgressBar.ProgressColor2 = System.Drawing.Color.Cyan;
            this.dataLoadingProgressBar.ShadowDecoration.Parent = this.dataLoadingProgressBar;
            this.dataLoadingProgressBar.Size = new System.Drawing.Size(539, 30);
            this.dataLoadingProgressBar.TabIndex = 18;
            this.dataLoadingProgressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // dataLoading_clock
            // 
            this.dataLoading_clock.Interval = 1;
            this.dataLoading_clock.Tick += new System.EventHandler(this.dataLoading_clock_Tick);
            // 
            // roundEdge
            // 
            this.roundEdge.BorderRadius = 15;
            this.roundEdge.TargetControl = this;
            // 
            // LoadingWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(15)))), ((int)(((byte)(46)))));
            this.BackgroundImage = global::App.Properties.Resources.Background;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(539, 333);
            this.Controls.Add(this.dataLoadingProgressBar);
            this.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "LoadingWindow";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "autoStudent - Loading Data. . . ";
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ProgressBar dataLoadingProgressBar;
        private System.Windows.Forms.Timer dataLoading_clock;
        private Guna.UI2.WinForms.Guna2ShadowForm guna2ShadowForm;
        private Guna.UI2.WinForms.Guna2Elipse roundEdge;
    }
}

