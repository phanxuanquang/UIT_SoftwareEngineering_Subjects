using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.Main_Windows.AboutForm
{
    public partial class AboutForm : Form
    {
        Timer timer;
        List<(Point, Color)> defaultFormat;
        int y = 43;
        int max = 40;
        int index = 0;

        public AboutForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            defaultFormat = new List<(Point, Color)>();

            for (int index = 0; index < this.contentPanel.Controls.Count; index++)
            {
                defaultFormat.Add((this.contentPanel.Controls[index].Location, this.contentPanel.Controls[index].ForeColor));
                this.contentPanel.Controls[index].Location = new Point(this.contentPanel.Controls[index].Location.X, this.contentPanel.Controls[index].Location.Y + y);
                this.contentPanel.Controls[index].ForeColor = Color.FromArgb(34, 40, 87);
            }

            Program.SetDoubleBuffered(contentPanel);
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            this.contentPanel.SuspendLayout();
            if (index < contentPanel.Controls.Count)
            {
                if (this.contentPanel.Controls[index].ForeColor.G < 255)
                {
                    this.contentPanel.Controls[index].Visible = true;
                    if (y > 0)
                    {
                        y--;
                        contentPanel.Controls[index].Location = new System.Drawing.Point(contentPanel.Controls[index].Location.X, contentPanel.Controls[index].Location.Y - 1);
                    }
                    if (contentPanel.Controls[index].ForeColor.G < 255)
                    {
                        max += 5;
                        contentPanel.Controls[index].ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(max)))), ((int)(((byte)(max)))));
                    }
                    this.contentPanel.ResumeLayout(false);
                }
                else
                {
                    index++;
                    max = 40;
                    y = 43;
                }
            }
            else timer.Stop();
            timer.Start();
        }

        #region Windows 
        // Anti Flickering
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }
        private void AboutForm_Shown(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void contentPanel_Click(object sender, EventArgs e)
        {
            timer.Stop();
            for (int index = 0; index < this.contentPanel.Controls.Count; index++)
            {
                this.contentPanel.Controls[index].Location = defaultFormat[index].Item1;
                this.contentPanel.Controls[index].ForeColor = defaultFormat[index].Item2;
                this.contentPanel.Controls[index].Visible = true;
            }
        }
        #endregion

        #region Link Labels
        private void FeedbackLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        { 
            FeedbackTab feedbackTab = new FeedbackTab();
            this.Controls.Add(feedbackTab);
            feedbackTab.Dock = DockStyle.Fill;
            feedbackTab.BringToFront();
        }
        private void GithubLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process openGitHub = new Process();
            openGitHub.StartInfo.FileName = "CMD.exe";
            openGitHub.StartInfo.Arguments = "/C start https://github.com/phanxuanquang/autoStudent";
            openGitHub.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            openGitHub.Start();
        }
        #endregion
    }
}
