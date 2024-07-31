namespace App
{
    partial class ProgressWindow_Install : ProgressWindow_Base
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
            this.label1 = new System.Windows.Forms.Label();
            this.ThreadProgressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            this.CancelButton = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(0, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 25);
            this.label1.TabIndex = 41;
            this.label1.Text = "Tiến độ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // ThreadProgressBar
            // 
            this.ThreadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ThreadProgressBar.BorderRadius = 12;
            this.ThreadProgressBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(29)))), ((int)(((byte)(67)))));
            this.ThreadProgressBar.ForeColor = System.Drawing.Color.Cyan;
            this.ThreadProgressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.ThreadProgressBar.Location = new System.Drawing.Point(125, 46);
            this.ThreadProgressBar.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.ThreadProgressBar.Name = "ThreadProgressBar";
            this.ThreadProgressBar.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(106)))), ((int)(((byte)(204)))));
            this.ThreadProgressBar.ProgressColor2 = System.Drawing.Color.Cyan;
            this.ThreadProgressBar.ShadowDecoration.Parent = this.ThreadProgressBar;
            this.ThreadProgressBar.Size = new System.Drawing.Size(482, 30);
            this.ThreadProgressBar.TabIndex = 42;
            this.ThreadProgressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.CancelButton.AutoRoundedCorners = true;
            this.CancelButton.BackColor = System.Drawing.Color.Transparent;
            this.CancelButton.BorderColor = System.Drawing.Color.Cyan;
            this.CancelButton.BorderRadius = 15;
            this.CancelButton.BorderThickness = 2;
            this.CancelButton.CheckedState.Parent = this.CancelButton;
            this.CancelButton.CustomImages.Parent = this.CancelButton;
            this.CancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.CancelButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.CancelButton.ForeColor = System.Drawing.Color.Cyan;
            this.CancelButton.HoverState.Parent = this.CancelButton;
            this.CancelButton.Location = new System.Drawing.Point(615, 44);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8, 3, 3, 8);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.ShadowDecoration.Parent = this.CancelButton;
            this.CancelButton.Size = new System.Drawing.Size(65, 32);
            this.CancelButton.TabIndex = 43;
            this.CancelButton.Text = "HỦY";
            this.CancelButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            // 
            // ProgressWindow_Install
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ProgressWindow_Install";
            this.Text = "autoStudent - Cài đặt phần mềm";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ProgressBar ThreadProgressBar;
        protected Guna.UI2.WinForms.Guna2Button CancelButton;
    }
}