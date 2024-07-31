namespace App
{
    partial class MainUI
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
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.wizardButton = new Guna.UI2.WinForms.Guna2ImageButton();
            this.githubButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.updateButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.cleanButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.settingButton = new Guna.UI2.WinForms.Guna2GradientButton();
            this.uninstallButton = new Guna.UI2.WinForms.Guna2Button();
            this.installButton = new Guna.UI2.WinForms.Guna2Button();
            this.exitButton = new Guna.UI2.WinForms.Guna2Button();
            this.roundEdge = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.SuspendLayout();
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.Animated = true;
            this.minimizeButton.BorderColor = System.Drawing.Color.Cyan;
            this.minimizeButton.BorderRadius = 3;
            this.minimizeButton.CheckedState.Parent = this.minimizeButton;
            this.minimizeButton.CustomImages.Parent = this.minimizeButton;
            this.minimizeButton.FillColor = System.Drawing.Color.Transparent;
            this.minimizeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.minimizeButton.ForeColor = System.Drawing.Color.White;
            this.minimizeButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(125)))));
            this.minimizeButton.HoverState.Parent = this.minimizeButton;
            this.minimizeButton.Image = global::App.Properties.Resources.Minimalize;
            this.minimizeButton.Location = new System.Drawing.Point(896, 12);
            this.minimizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.ShadowDecoration.Parent = this.minimizeButton;
            this.minimizeButton.Size = new System.Drawing.Size(30, 30);
            this.minimizeButton.TabIndex = 5;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // wizardButton
            // 
            this.wizardButton.BackColor = System.Drawing.Color.Transparent;
            this.wizardButton.CheckedState.Parent = this.wizardButton;
            this.wizardButton.HoverState.ImageSize = new System.Drawing.Size(60, 60);
            this.wizardButton.HoverState.Parent = this.wizardButton;
            this.wizardButton.Image = global::App.Properties.Resources.Wizard;
            this.wizardButton.ImageSize = new System.Drawing.Size(58, 58);
            this.wizardButton.Location = new System.Drawing.Point(848, 273);
            this.wizardButton.Margin = new System.Windows.Forms.Padding(0);
            this.wizardButton.Name = "wizardButton";
            this.wizardButton.PressedState.ImageSize = new System.Drawing.Size(58, 58);
            this.wizardButton.PressedState.Parent = this.wizardButton;
            this.wizardButton.Size = new System.Drawing.Size(60, 60);
            this.wizardButton.TabIndex = 8;
            this.wizardButton.Click += new System.EventHandler(this.wizardButton_Click);
            // 
            // githubButton
            // 
            this.githubButton.Animated = true;
            this.githubButton.BorderColor = System.Drawing.Color.Transparent;
            this.githubButton.CheckedState.Parent = this.githubButton;
            this.githubButton.CustomImages.Parent = this.githubButton;
            this.githubButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.githubButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.githubButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.githubButton.ForeColor = System.Drawing.Color.Transparent;
            this.githubButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.githubButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.githubButton.HoverState.ForeColor = System.Drawing.Color.Cyan;
            this.githubButton.HoverState.Parent = this.githubButton;
            this.githubButton.Image = global::App.Properties.Resources.Github;
            this.githubButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.githubButton.ImageOffset = new System.Drawing.Point(10, 0);
            this.githubButton.ImageSize = new System.Drawing.Size(30, 30);
            this.githubButton.Location = new System.Drawing.Point(2, 401);
            this.githubButton.Margin = new System.Windows.Forms.Padding(0);
            this.githubButton.Name = "githubButton";
            this.githubButton.PressedColor = System.Drawing.Color.Cyan;
            this.githubButton.PressedDepth = 20;
            this.githubButton.ShadowDecoration.Parent = this.githubButton;
            this.githubButton.Size = new System.Drawing.Size(200, 75);
            this.githubButton.TabIndex = 7;
            this.githubButton.Text = "Phần mềm";
            this.githubButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.githubButton.TextOffset = new System.Drawing.Point(35, 0);
            this.githubButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.githubButton.Click += new System.EventHandler(this.githubButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Animated = true;
            this.updateButton.BorderColor = System.Drawing.Color.Transparent;
            this.updateButton.CheckedState.Parent = this.updateButton;
            this.updateButton.CustomImages.Parent = this.updateButton;
            this.updateButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.updateButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.updateButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.updateButton.ForeColor = System.Drawing.Color.Transparent;
            this.updateButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.updateButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.updateButton.HoverState.ForeColor = System.Drawing.Color.Cyan;
            this.updateButton.HoverState.Parent = this.updateButton;
            this.updateButton.Image = global::App.Properties.Resources.Update;
            this.updateButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.updateButton.ImageOffset = new System.Drawing.Point(13, 0);
            this.updateButton.ImageSize = new System.Drawing.Size(25, 25);
            this.updateButton.Location = new System.Drawing.Point(2, 311);
            this.updateButton.Margin = new System.Windows.Forms.Padding(0);
            this.updateButton.Name = "updateButton";
            this.updateButton.PressedColor = System.Drawing.Color.Cyan;
            this.updateButton.PressedDepth = 20;
            this.updateButton.ShadowDecoration.Parent = this.updateButton;
            this.updateButton.Size = new System.Drawing.Size(200, 75);
            this.updateButton.TabIndex = 6;
            this.updateButton.Text = "Cập nhật";
            this.updateButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.updateButton.TextOffset = new System.Drawing.Point(40, 0);
            this.updateButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // cleanButton
            // 
            this.cleanButton.Animated = true;
            this.cleanButton.BorderColor = System.Drawing.Color.Transparent;
            this.cleanButton.CheckedState.Parent = this.cleanButton;
            this.cleanButton.CustomImages.Parent = this.cleanButton;
            this.cleanButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.cleanButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.cleanButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.cleanButton.ForeColor = System.Drawing.Color.Transparent;
            this.cleanButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.cleanButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.cleanButton.HoverState.ForeColor = System.Drawing.Color.Cyan;
            this.cleanButton.HoverState.Parent = this.cleanButton;
            this.cleanButton.Image = global::App.Properties.Resources.Clean;
            this.cleanButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.cleanButton.ImageOffset = new System.Drawing.Point(10, 0);
            this.cleanButton.ImageSize = new System.Drawing.Size(30, 30);
            this.cleanButton.Location = new System.Drawing.Point(2, 221);
            this.cleanButton.Margin = new System.Windows.Forms.Padding(0);
            this.cleanButton.Name = "cleanButton";
            this.cleanButton.PressedColor = System.Drawing.Color.Cyan;
            this.cleanButton.PressedDepth = 20;
            this.cleanButton.ShadowDecoration.Parent = this.cleanButton;
            this.cleanButton.Size = new System.Drawing.Size(200, 75);
            this.cleanButton.TabIndex = 5;
            this.cleanButton.Text = "Dọn dẹp";
            this.cleanButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.cleanButton.TextOffset = new System.Drawing.Point(40, 0);
            this.cleanButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.cleanButton.Click += new System.EventHandler(this.cleanButton_Click);
            // 
            // settingButton
            // 
            this.settingButton.Animated = true;
            this.settingButton.BorderColor = System.Drawing.Color.Transparent;
            this.settingButton.CheckedState.Parent = this.settingButton;
            this.settingButton.CustomImages.Parent = this.settingButton;
            this.settingButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.settingButton.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.settingButton.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.settingButton.ForeColor = System.Drawing.Color.Transparent;
            this.settingButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.settingButton.HoverState.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.settingButton.HoverState.ForeColor = System.Drawing.Color.Cyan;
            this.settingButton.HoverState.Parent = this.settingButton;
            this.settingButton.Image = global::App.Properties.Resources.Setting;
            this.settingButton.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.settingButton.ImageOffset = new System.Drawing.Point(10, 0);
            this.settingButton.ImageSize = new System.Drawing.Size(30, 30);
            this.settingButton.Location = new System.Drawing.Point(2, 131);
            this.settingButton.Margin = new System.Windows.Forms.Padding(0);
            this.settingButton.Name = "settingButton";
            this.settingButton.PressedColor = System.Drawing.Color.Cyan;
            this.settingButton.PressedDepth = 20;
            this.settingButton.ShadowDecoration.Parent = this.settingButton;
            this.settingButton.Size = new System.Drawing.Size(200, 75);
            this.settingButton.TabIndex = 4;
            this.settingButton.Text = "Thiết lập";
            this.settingButton.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.settingButton.TextOffset = new System.Drawing.Point(40, 0);
            this.settingButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.settingButton.Click += new System.EventHandler(this.settingButton_Click);
            // 
            // uninstallButton
            // 
            this.uninstallButton.Animated = true;
            this.uninstallButton.BackColor = System.Drawing.Color.Transparent;
            this.uninstallButton.BorderColor = System.Drawing.Color.Cyan;
            this.uninstallButton.BorderRadius = 60;
            this.uninstallButton.BorderThickness = 3;
            this.uninstallButton.CheckedState.Parent = this.uninstallButton;
            this.uninstallButton.CustomImages.Parent = this.uninstallButton;
            this.uninstallButton.FillColor = System.Drawing.Color.Transparent;
            this.uninstallButton.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.uninstallButton.ForeColor = System.Drawing.Color.Cyan;
            this.uninstallButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.uninstallButton.HoverState.Parent = this.uninstallButton;
            this.uninstallButton.Location = new System.Drawing.Point(219, 319);
            this.uninstallButton.Margin = new System.Windows.Forms.Padding(0);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.PressedColor = System.Drawing.Color.Cyan;
            this.uninstallButton.PressedDepth = 20;
            this.uninstallButton.ShadowDecoration.Parent = this.uninstallButton;
            this.uninstallButton.Size = new System.Drawing.Size(536, 126);
            this.uninstallButton.TabIndex = 1;
            this.uninstallButton.Text = "GỠ CÀI ĐẶT PHẦN MỀM";
            this.uninstallButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.uninstallButton.Click += new System.EventHandler(this.uninstallButton_Click);
            // 
            // installButton
            // 
            this.installButton.Animated = true;
            this.installButton.BackColor = System.Drawing.Color.Transparent;
            this.installButton.BorderColor = System.Drawing.Color.Cyan;
            this.installButton.BorderRadius = 60;
            this.installButton.BorderThickness = 3;
            this.installButton.CheckedState.Parent = this.installButton;
            this.installButton.CustomImages.Parent = this.installButton;
            this.installButton.FillColor = System.Drawing.Color.Transparent;
            this.installButton.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.installButton.ForeColor = System.Drawing.Color.Cyan;
            this.installButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.installButton.HoverState.Parent = this.installButton;
            this.installButton.Location = new System.Drawing.Point(219, 162);
            this.installButton.Margin = new System.Windows.Forms.Padding(0);
            this.installButton.Name = "installButton";
            this.installButton.PressedColor = System.Drawing.Color.Cyan;
            this.installButton.PressedDepth = 20;
            this.installButton.ShadowDecoration.Parent = this.installButton;
            this.installButton.Size = new System.Drawing.Size(536, 126);
            this.installButton.TabIndex = 0;
            this.installButton.Text = "CÀI ĐẶT PHẦN MỀM";
            this.installButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Animated = true;
            this.exitButton.BorderColor = System.Drawing.Color.Cyan;
            this.exitButton.BorderRadius = 3;
            this.exitButton.CheckedState.Parent = this.exitButton;
            this.exitButton.CustomImages.Parent = this.exitButton;
            this.exitButton.FillColor = System.Drawing.Color.Transparent;
            this.exitButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.exitButton.ForeColor = System.Drawing.Color.White;
            this.exitButton.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(57)))), ((int)(((byte)(125)))));
            this.exitButton.HoverState.Parent = this.exitButton;
            this.exitButton.Image = global::App.Properties.Resources.Exit;
            this.exitButton.Location = new System.Drawing.Point(931, 12);
            this.exitButton.Margin = new System.Windows.Forms.Padding(0);
            this.exitButton.Name = "exitButton";
            this.exitButton.ShadowDecoration.Parent = this.exitButton;
            this.exitButton.Size = new System.Drawing.Size(30, 30);
            this.exitButton.TabIndex = 4;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // roundEdge
            // 
            this.roundEdge.BorderRadius = 15;
            this.roundEdge.TargetControl = this;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.TargetControl = this;
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.BackgroundImage = global::App.Properties.Resources.Background_Main_UI;
            this.ClientSize = new System.Drawing.Size(971, 607);
            this.Controls.Add(this.wizardButton);
            this.Controls.Add(this.githubButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.cleanButton);
            this.Controls.Add(this.settingButton);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.uninstallButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "autoStudent";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.Shown += new System.EventHandler(this.MainUI_Shown);
            this.SizeChanged += new System.EventHandler(this.MainUI_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion
        private Guna.UI2.WinForms.Guna2ImageButton wizardButton;
        private Guna.UI2.WinForms.Guna2GradientButton githubButton;
        private Guna.UI2.WinForms.Guna2GradientButton updateButton;
        private Guna.UI2.WinForms.Guna2GradientButton cleanButton;
        private Guna.UI2.WinForms.Guna2GradientButton settingButton;
        private Guna.UI2.WinForms.Guna2Button uninstallButton;
        private Guna.UI2.WinForms.Guna2Button installButton;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2Button exitButton;
        private Guna.UI2.WinForms.Guna2Elipse roundEdge;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}