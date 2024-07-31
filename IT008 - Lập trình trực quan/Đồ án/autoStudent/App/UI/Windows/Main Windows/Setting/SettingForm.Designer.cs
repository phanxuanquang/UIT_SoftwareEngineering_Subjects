namespace App
{
    partial class SettingForm
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
            this.timeSetter = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.activatedAction = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cleanAfterCompleted_Switch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.label4 = new System.Windows.Forms.Label();
            this.defaultSetting_Button = new Guna.UI2.WinForms.Guna2Button();
            this.exitButton = new Guna.UI2.WinForms.Guna2Button();
            this.label5 = new System.Windows.Forms.Label();
            this.dataExportAfterCompleted_Switch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.label6 = new System.Windows.Forms.Label();
            this.saveDownload = new Guna.UI2.WinForms.Guna2TextBox();
            this.saveDownload_Button = new Guna.UI2.WinForms.Guna2Button();
            this.exportPath_Button = new Guna.UI2.WinForms.Guna2Button();
            this.exportPath = new Guna.UI2.WinForms.Guna2TextBox();
            this.timeSetter_Switch = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.roundEdge = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.ShadowForm = new Guna.UI2.WinForms.Guna2ShadowForm(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.SuspendLayout();
            // 
            // timeSetter
            // 
            this.timeSetter.CalendarForeColor = System.Drawing.Color.Cyan;
            this.timeSetter.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.timeSetter.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.timeSetter.CalendarTitleForeColor = System.Drawing.Color.Cyan;
            this.timeSetter.CalendarTrailingForeColor = System.Drawing.Color.Cyan;
            this.timeSetter.CausesValidation = false;
            this.timeSetter.Checked = false;
            this.timeSetter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.timeSetter.CustomFormat = "    dd/MM/yyyy   hh:mm:ss tt";
            this.timeSetter.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.timeSetter.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeSetter.Location = new System.Drawing.Point(415, 198);
            this.timeSetter.Margin = new System.Windows.Forms.Padding(10);
            this.timeSetter.Name = "timeSetter";
            this.timeSetter.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.timeSetter.Size = new System.Drawing.Size(320, 26);
            this.timeSetter.TabIndex = 0;
            this.timeSetter.Value = new System.DateTime(2021, 11, 26, 8, 29, 0, 0);
            this.timeSetter.Visible = false;
            this.timeSetter.ValueChanged += new System.EventHandler(this.timeSetter_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.Cyan;
            this.label1.Location = new System.Drawing.Point(63, 206);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Hẹn thời gian bắt đầu:";
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.ForeColor = System.Drawing.Color.Cyan;
            this.label3.Location = new System.Drawing.Point(63, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Sau khi hoàn tất:";
            this.label3.UseCompatibleTextRendering = true;
            // 
            // activatedAction
            // 
            this.activatedAction.Animated = true;
            this.activatedAction.BackColor = System.Drawing.Color.Transparent;
            this.activatedAction.BorderColor = System.Drawing.Color.Cyan;
            this.activatedAction.BorderRadius = 5;
            this.activatedAction.BorderThickness = 2;
            this.activatedAction.DisplayMember = "1";
            this.activatedAction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.activatedAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.activatedAction.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.activatedAction.FocusedColor = System.Drawing.Color.Empty;
            this.activatedAction.FocusedState.Parent = this.activatedAction;
            this.activatedAction.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.activatedAction.ForeColor = System.Drawing.Color.Cyan;
            this.activatedAction.FormattingEnabled = true;
            this.activatedAction.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.activatedAction.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.activatedAction.HoverState.Parent = this.activatedAction;
            this.activatedAction.ItemHeight = 30;
            this.activatedAction.Items.AddRange(new object[] {
            "Không làm gì",
            "Thoát chương trình",
            "Khóa máy",
            "Ngủ",
            "Khởi động lại",
            "Tắt máy"});
            this.activatedAction.ItemsAppearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.activatedAction.ItemsAppearance.ForeColor = System.Drawing.Color.Cyan;
            this.activatedAction.ItemsAppearance.Parent = this.activatedAction;
            this.activatedAction.ItemsAppearance.SelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.activatedAction.ItemsAppearance.SelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.activatedAction.Location = new System.Drawing.Point(357, 126);
            this.activatedAction.Name = "activatedAction";
            this.activatedAction.ShadowDecoration.Parent = this.activatedAction;
            this.activatedAction.Size = new System.Drawing.Size(378, 36);
            this.activatedAction.TabIndex = 5;
            this.activatedAction.TextOffset = new System.Drawing.Point(10, 0);
            this.activatedAction.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.activatedAction.SelectedIndexChanged += new System.EventHandler(this.activatedAction_SelectedIndexChanged);
            // 
            // cleanAfterCompleted_Switch
            // 
            this.cleanAfterCompleted_Switch.Animated = true;
            this.cleanAfterCompleted_Switch.AutoRoundedCorners = true;
            this.cleanAfterCompleted_Switch.BackColor = System.Drawing.Color.Transparent;
            this.cleanAfterCompleted_Switch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cleanAfterCompleted_Switch.CheckedState.BorderRadius = 12;
            this.cleanAfterCompleted_Switch.CheckedState.FillColor = System.Drawing.Color.Cyan;
            this.cleanAfterCompleted_Switch.CheckedState.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.cleanAfterCompleted_Switch.CheckedState.InnerBorderRadius = 8;
            this.cleanAfterCompleted_Switch.CheckedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.cleanAfterCompleted_Switch.CheckedState.Parent = this.cleanAfterCompleted_Switch;
            this.cleanAfterCompleted_Switch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cleanAfterCompleted_Switch.Location = new System.Drawing.Point(357, 68);
            this.cleanAfterCompleted_Switch.Margin = new System.Windows.Forms.Padding(0);
            this.cleanAfterCompleted_Switch.Name = "cleanAfterCompleted_Switch";
            this.cleanAfterCompleted_Switch.ShadowDecoration.Parent = this.cleanAfterCompleted_Switch;
            this.cleanAfterCompleted_Switch.Size = new System.Drawing.Size(50, 26);
            this.cleanAfterCompleted_Switch.TabIndex = 6;
            this.cleanAfterCompleted_Switch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.cleanAfterCompleted_Switch.UncheckedState.BorderRadius = 12;
            this.cleanAfterCompleted_Switch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.cleanAfterCompleted_Switch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.cleanAfterCompleted_Switch.UncheckedState.InnerBorderRadius = 8;
            this.cleanAfterCompleted_Switch.UncheckedState.InnerColor = System.Drawing.Color.Cyan;
            this.cleanAfterCompleted_Switch.UncheckedState.Parent = this.cleanAfterCompleted_Switch;
            this.cleanAfterCompleted_Switch.CheckedChanged += new System.EventHandler(this.cleanAfterCompleted_Switch_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.ForeColor = System.Drawing.Color.Cyan;
            this.label4.Location = new System.Drawing.Point(63, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(207, 23);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dọn dẹp sau khi hoàn tất:";
            this.label4.UseCompatibleTextRendering = true;
            // 
            // defaultSetting_Button
            // 
            this.defaultSetting_Button.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.defaultSetting_Button.BackColor = System.Drawing.Color.Transparent;
            this.defaultSetting_Button.BorderColor = System.Drawing.Color.Cyan;
            this.defaultSetting_Button.BorderRadius = 30;
            this.defaultSetting_Button.BorderThickness = 3;
            this.defaultSetting_Button.CheckedState.Parent = this.defaultSetting_Button;
            this.defaultSetting_Button.CustomImages.Parent = this.defaultSetting_Button;
            this.defaultSetting_Button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.defaultSetting_Button.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.defaultSetting_Button.ForeColor = System.Drawing.Color.Cyan;
            this.defaultSetting_Button.HoverState.Parent = this.defaultSetting_Button;
            this.defaultSetting_Button.Location = new System.Drawing.Point(267, 427);
            this.defaultSetting_Button.Name = "defaultSetting_Button";
            this.defaultSetting_Button.ShadowDecoration.Parent = this.defaultSetting_Button;
            this.defaultSetting_Button.Size = new System.Drawing.Size(275, 66);
            this.defaultSetting_Button.TabIndex = 22;
            this.defaultSetting_Button.Text = "ĐẶT VỀ MẶC ĐỊNH";
            this.defaultSetting_Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            this.defaultSetting_Button.Click += new System.EventHandler(this.defaultSetting_Button_Click);
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exitButton.Animated = true;
            this.exitButton.BackColor = System.Drawing.Color.Transparent;
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
            this.exitButton.Location = new System.Drawing.Point(774, 4);
            this.exitButton.Name = "exitButton";
            this.exitButton.ShadowDecoration.Parent = this.exitButton;
            this.exitButton.Size = new System.Drawing.Size(30, 30);
            this.exitButton.TabIndex = 24;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.ForeColor = System.Drawing.Color.Cyan;
            this.label5.Location = new System.Drawing.Point(63, 273);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(288, 23);
            this.label5.TabIndex = 26;
            this.label5.Text = "Xuất danh sách phần mềm đã chọn:";
            this.label5.UseCompatibleTextRendering = true;
            // 
            // dataExportAfterCompleted_Switch
            // 
            this.dataExportAfterCompleted_Switch.Animated = true;
            this.dataExportAfterCompleted_Switch.AutoRoundedCorners = true;
            this.dataExportAfterCompleted_Switch.BackColor = System.Drawing.Color.Transparent;
            this.dataExportAfterCompleted_Switch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.dataExportAfterCompleted_Switch.CheckedState.BorderRadius = 12;
            this.dataExportAfterCompleted_Switch.CheckedState.FillColor = System.Drawing.Color.Cyan;
            this.dataExportAfterCompleted_Switch.CheckedState.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.dataExportAfterCompleted_Switch.CheckedState.InnerBorderRadius = 8;
            this.dataExportAfterCompleted_Switch.CheckedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.dataExportAfterCompleted_Switch.CheckedState.Parent = this.dataExportAfterCompleted_Switch;
            this.dataExportAfterCompleted_Switch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dataExportAfterCompleted_Switch.Location = new System.Drawing.Point(357, 268);
            this.dataExportAfterCompleted_Switch.Margin = new System.Windows.Forms.Padding(0);
            this.dataExportAfterCompleted_Switch.Name = "dataExportAfterCompleted_Switch";
            this.dataExportAfterCompleted_Switch.ShadowDecoration.Parent = this.dataExportAfterCompleted_Switch;
            this.dataExportAfterCompleted_Switch.Size = new System.Drawing.Size(50, 26);
            this.dataExportAfterCompleted_Switch.TabIndex = 25;
            this.dataExportAfterCompleted_Switch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.dataExportAfterCompleted_Switch.UncheckedState.BorderRadius = 12;
            this.dataExportAfterCompleted_Switch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.dataExportAfterCompleted_Switch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.dataExportAfterCompleted_Switch.UncheckedState.InnerBorderRadius = 8;
            this.dataExportAfterCompleted_Switch.UncheckedState.InnerColor = System.Drawing.Color.Cyan;
            this.dataExportAfterCompleted_Switch.UncheckedState.Parent = this.dataExportAfterCompleted_Switch;
            this.dataExportAfterCompleted_Switch.CheckedChanged += new System.EventHandler(this.dataExportAfterCompleted_Switch_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.ForeColor = System.Drawing.Color.Cyan;
            this.label6.Location = new System.Drawing.Point(63, 340);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 23);
            this.label6.TabIndex = 27;
            this.label6.Text = "Nơi lưu trình cài đặt:";
            this.label6.UseCompatibleTextRendering = true;
            // 
            // saveDownload
            // 
            this.saveDownload.BackColor = System.Drawing.Color.Transparent;
            this.saveDownload.BorderColor = System.Drawing.Color.Cyan;
            this.saveDownload.BorderRadius = 5;
            this.saveDownload.BorderThickness = 2;
            this.saveDownload.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.saveDownload.DefaultText = "";
            this.saveDownload.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.saveDownload.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.saveDownload.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.saveDownload.DisabledState.Parent = this.saveDownload;
            this.saveDownload.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.saveDownload.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.saveDownload.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.saveDownload.FocusedState.Parent = this.saveDownload;
            this.saveDownload.ForeColor = System.Drawing.Color.Cyan;
            this.saveDownload.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.saveDownload.HoverState.Parent = this.saveDownload;
            this.saveDownload.Location = new System.Drawing.Point(357, 333);
            this.saveDownload.Margin = new System.Windows.Forms.Padding(0);
            this.saveDownload.Name = "saveDownload";
            this.saveDownload.PasswordChar = '\0';
            this.saveDownload.PlaceholderText = "C:\\";
            this.saveDownload.SelectedText = "";
            this.saveDownload.ShadowDecoration.Parent = this.saveDownload;
            this.saveDownload.Size = new System.Drawing.Size(291, 35);
            this.saveDownload.TabIndex = 28;
            this.saveDownload.TextOffset = new System.Drawing.Point(5, 0);
            this.saveDownload.TextChanged += new System.EventHandler(this.saveDownload_TextChanged);
            // 
            // saveDownload_Button
            // 
            this.saveDownload_Button.Animated = true;
            this.saveDownload_Button.BackColor = System.Drawing.Color.Transparent;
            this.saveDownload_Button.BorderColor = System.Drawing.Color.Cyan;
            this.saveDownload_Button.BorderRadius = 5;
            this.saveDownload_Button.BorderThickness = 2;
            this.saveDownload_Button.CheckedState.Parent = this.saveDownload_Button;
            this.saveDownload_Button.CustomImages.Parent = this.saveDownload_Button;
            this.saveDownload_Button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.saveDownload_Button.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.saveDownload_Button.ForeColor = System.Drawing.Color.Cyan;
            this.saveDownload_Button.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(75)))), ((int)(((byte)(163)))));
            this.saveDownload_Button.HoverState.Parent = this.saveDownload_Button;
            this.saveDownload_Button.Location = new System.Drawing.Point(656, 333);
            this.saveDownload_Button.Margin = new System.Windows.Forms.Padding(0);
            this.saveDownload_Button.Name = "saveDownload_Button";
            this.saveDownload_Button.ShadowDecoration.Parent = this.saveDownload_Button;
            this.saveDownload_Button.Size = new System.Drawing.Size(79, 35);
            this.saveDownload_Button.TabIndex = 29;
            this.saveDownload_Button.Text = "KHÁC";
            this.saveDownload_Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.saveDownload_Button.Click += new System.EventHandler(this.saveDownload_Button_Click);
            // 
            // exportPath_Button
            // 
            this.exportPath_Button.Animated = true;
            this.exportPath_Button.BackColor = System.Drawing.Color.Transparent;
            this.exportPath_Button.BorderColor = System.Drawing.Color.Cyan;
            this.exportPath_Button.BorderRadius = 5;
            this.exportPath_Button.BorderThickness = 2;
            this.exportPath_Button.CheckedState.Parent = this.exportPath_Button;
            this.exportPath_Button.CustomImages.Parent = this.exportPath_Button;
            this.exportPath_Button.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.exportPath_Button.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.exportPath_Button.ForeColor = System.Drawing.Color.Cyan;
            this.exportPath_Button.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(75)))), ((int)(((byte)(163)))));
            this.exportPath_Button.HoverState.Parent = this.exportPath_Button;
            this.exportPath_Button.Location = new System.Drawing.Point(656, 263);
            this.exportPath_Button.Margin = new System.Windows.Forms.Padding(0);
            this.exportPath_Button.Name = "exportPath_Button";
            this.exportPath_Button.ShadowDecoration.Parent = this.exportPath_Button;
            this.exportPath_Button.Size = new System.Drawing.Size(79, 35);
            this.exportPath_Button.TabIndex = 31;
            this.exportPath_Button.Text = "KHÁC";
            this.exportPath_Button.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.exportPath_Button.Click += new System.EventHandler(this.exportPath_Button_Click);
            // 
            // exportPath
            // 
            this.exportPath.Animated = true;
            this.exportPath.BackColor = System.Drawing.Color.Transparent;
            this.exportPath.BorderColor = System.Drawing.Color.Cyan;
            this.exportPath.BorderRadius = 5;
            this.exportPath.BorderThickness = 2;
            this.exportPath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.exportPath.DefaultText = "";
            this.exportPath.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.exportPath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.exportPath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.exportPath.DisabledState.Parent = this.exportPath;
            this.exportPath.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.exportPath.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(40)))), ((int)(((byte)(87)))));
            this.exportPath.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.exportPath.FocusedState.Parent = this.exportPath;
            this.exportPath.ForeColor = System.Drawing.Color.Cyan;
            this.exportPath.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.exportPath.HoverState.Parent = this.exportPath;
            this.exportPath.Location = new System.Drawing.Point(415, 263);
            this.exportPath.Margin = new System.Windows.Forms.Padding(0);
            this.exportPath.Name = "exportPath";
            this.exportPath.PasswordChar = '\0';
            this.exportPath.PlaceholderText = "C:\\";
            this.exportPath.SelectedText = "";
            this.exportPath.ShadowDecoration.Parent = this.exportPath;
            this.exportPath.Size = new System.Drawing.Size(233, 35);
            this.exportPath.TabIndex = 30;
            this.exportPath.TextOffset = new System.Drawing.Point(5, 0);
            this.exportPath.TextChanged += new System.EventHandler(this.exportPath_TextChanged);
            // 
            // timeSetter_Switch
            // 
            this.timeSetter_Switch.Animated = true;
            this.timeSetter_Switch.AutoRoundedCorners = true;
            this.timeSetter_Switch.BackColor = System.Drawing.Color.Transparent;
            this.timeSetter_Switch.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.timeSetter_Switch.CheckedState.BorderRadius = 12;
            this.timeSetter_Switch.CheckedState.FillColor = System.Drawing.Color.Cyan;
            this.timeSetter_Switch.CheckedState.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.timeSetter_Switch.CheckedState.InnerBorderRadius = 8;
            this.timeSetter_Switch.CheckedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(35)))), ((int)(((byte)(77)))));
            this.timeSetter_Switch.CheckedState.Parent = this.timeSetter_Switch;
            this.timeSetter_Switch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.timeSetter_Switch.Location = new System.Drawing.Point(357, 199);
            this.timeSetter_Switch.Margin = new System.Windows.Forms.Padding(0);
            this.timeSetter_Switch.Name = "timeSetter_Switch";
            this.timeSetter_Switch.ShadowDecoration.Parent = this.timeSetter_Switch;
            this.timeSetter_Switch.Size = new System.Drawing.Size(50, 26);
            this.timeSetter_Switch.TabIndex = 32;
            this.timeSetter_Switch.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.timeSetter_Switch.UncheckedState.BorderRadius = 12;
            this.timeSetter_Switch.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(16)))), ((int)(((byte)(47)))));
            this.timeSetter_Switch.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.timeSetter_Switch.UncheckedState.InnerBorderRadius = 8;
            this.timeSetter_Switch.UncheckedState.InnerColor = System.Drawing.Color.Cyan;
            this.timeSetter_Switch.UncheckedState.Parent = this.timeSetter_Switch;
            this.timeSetter_Switch.CheckedChanged += new System.EventHandler(this.timeSetter_Switch_CheckedChanged);
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
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::App.Properties.Resources.Background__SubWindow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(808, 540);
            this.Controls.Add(this.timeSetter_Switch);
            this.Controls.Add(this.exportPath_Button);
            this.Controls.Add(this.exportPath);
            this.Controls.Add(this.saveDownload_Button);
            this.Controls.Add(this.saveDownload);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataExportAfterCompleted_Switch);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.defaultSetting_Button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cleanAfterCompleted_Switch);
            this.Controls.Add(this.activatedAction);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timeSetter);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "autoStudent - Thiết lập";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker timeSetter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2ComboBox activatedAction;
        private Guna.UI2.WinForms.Guna2ToggleSwitch cleanAfterCompleted_Switch;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2Button defaultSetting_Button;
        private Guna.UI2.WinForms.Guna2Button exitButton;
        private System.Windows.Forms.Label label5;
        private Guna.UI2.WinForms.Guna2ToggleSwitch dataExportAfterCompleted_Switch;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2TextBox saveDownload;
        private Guna.UI2.WinForms.Guna2Button saveDownload_Button;
        private Guna.UI2.WinForms.Guna2Button exportPath_Button;
        private Guna.UI2.WinForms.Guna2TextBox exportPath;
        private Guna.UI2.WinForms.Guna2ToggleSwitch timeSetter_Switch;
        private Guna.UI2.WinForms.Guna2Elipse roundEdge;
        private Guna.UI2.WinForms.Guna2ShadowForm ShadowForm;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl1;
    }
}