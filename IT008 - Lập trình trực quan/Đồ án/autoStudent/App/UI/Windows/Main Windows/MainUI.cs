using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class MainUI : Form
    {
        public MainUI()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            this.Icon = App.Properties.Resources.autoStudent;
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.SupportsTransparentBackColor, true);
            
            for(int i = 0; i < this.Controls.Count; i++)
                Program.SetDoubleBuffered(this.Controls[i]);
        }

        private void MainUI_Shown(object sender, EventArgs e)
        {
            if (Program.installSchedule != null || Program.uninstallSchedule != null)
            {
                switch (MessageBox.Show(
                    String.Format(
                        "Bạn đang còn {0} phần mềm ở phiên trước.\nBạn có muốn tiếp tục công việc ở phiên làm việc trước?",
                        (Program.installSchedule == null ? 0 : Program.installSchedule.Count) + (Program.uninstallSchedule == null ? 0 : Program.uninstallSchedule.Count)),
                    "autoStudent",
                    MessageBoxButtons.YesNo))
                {
                    case DialogResult.Yes:
                        ProgressWindow_Install progressWindow_Install = null;
                        ProgressWindow_Uninstall progressWindow_Uninstall = null;
                        if (Program.installSchedule != null && Program.installSchedule.Count > 0)
                        {
                            progressWindow_Install = new ProgressWindow_Install(Program.installSchedule);
                            if (!Directory.Exists(Program.setting.saveDownloadPath))
                            {
                                Directory.CreateDirectory(Program.setting.saveDownloadPath);
                            }
                            if (Program.uninstallSchedule != null && Program.uninstallSchedule.Count > 0)
                            {
                                progressWindow_Uninstall = new ProgressWindow_Uninstall(Program.uninstallSchedule);
                                progressWindow_Install.isOverlap = true;
                                progressWindow_Uninstall.isOverlap = true;
                                progressWindow_Uninstall.FormClosing += (sender, e) =>
                                {
                                    if (!progressWindow_Uninstall.PressedActionAll)
                                    {
                                        progressWindow_Install.FormClosing += (sender, e) =>
                                        {
                                            Program.mainUI.Show();
                                        };
                                        progressWindow_Install.Show();
                                    }
                                };
                                progressWindow_Uninstall.ExportData();
                                progressWindow_Install.ExportData();
                                Program.setting.CheckTimeOut(progressWindow_Uninstall);
                            }
                            else
                            {
                                progressWindow_Install.ExportData();
                                Program.setting.CheckTimeOut(progressWindow_Install);
                            }
                        }
                        else
                        {
                            if (Program.uninstallSchedule != null && Program.uninstallSchedule.Count > 0)
                            {
                                progressWindow_Uninstall = new ProgressWindow_Uninstall(Program.uninstallSchedule);
                                progressWindow_Uninstall.ExportData();
                                Program.setting.CheckTimeOut(progressWindow_Uninstall);
                            }
                        }
                        break;
                    case DialogResult.No:
                        Program.installSchedule = null;
                        Program.uninstallSchedule = null;
                        break;
                }
            }
        }

        private bool isInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #region Windows State
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
        private void MainUI_SizeChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Controls.Count; i++)
                Program.SetDoubleBuffered(this.Controls[i]);
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void wizardButton_Click(object sender, EventArgs e)
        {
            Process openGitHub = new Process();
            openGitHub.StartInfo.FileName = "CMD.exe";
            openGitHub.StartInfo.Arguments = "/C start https://github.com/phanxuanquang/autoStudent";
            openGitHub.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            openGitHub.Start();
        }
        #endregion

        #region Menu
        private void settingButton_Click(object sender, EventArgs e)
        {
            SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }
        private void cleanButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Các tệp sẽ bị xóa vĩnh viễn, bạn có muốn tiếp tục?", "DỌN DẸP", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Program.setting.cleanComputer();
                MessageBox.Show("Dọn dẹp hoàn tất.");
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (!isInternetAvailable())
            {
                MessageBox.Show("Không có kết nối mạng, vui lòng thử lại sau.");
                return;
            }

            DateTime GetLastModifyTime(string url)
            {
                WebRequest request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultNetworkCredentials;
                request.Method = "HEAD";

                using (WebResponse response = request.GetResponse())
                {
                    string lastModifyString = response.Headers.Get("Last-Modified");
                    DateTime remoteTime;
                    if (DateTime.TryParse(lastModifyString, out remoteTime))
                    {
                        return remoteTime;
                    }

                    return DateTime.MinValue;
                }
            }

            try
            {
                string content;
                string path = @"https://dung-ovl.github.io/MainData.json";
                using (WebClient client = new WebClient())
                {
                    Stream streamR = client.OpenRead(path);


                    using (StreamReader reader = new StreamReader(streamR))
                    {
                        content = reader.ReadToEnd();
                    }
                }
                DateTime modificationFileWeb = Root.FromJson(content).UpdateDate;
                DateTime modificationFileSystem = DataAccess.Instance.GetUpdateTime();

                if (modificationFileWeb > modificationFileSystem)
                {

                    string pathData = DataAccess.Instance.GetFilePath();
                    if (File.Exists(pathData))
                    {
                        File.Delete(pathData);
                    }
                    DataAccess.Instance.LoadDirect();
                    if (Environment.Is64BitOperatingSystem)
                        Program.software_Database = DataAccess.Instance.GetX64();
                    else Program.software_Database = DataAccess.Instance.GetX86();
                    MessageBox.Show("Cập nhật dữ liệu hoàn tất.");
                }
                else MessageBox.Show("Bạn đang sử dụng phiên bản mới nhất.");
            }
            catch (WebException we)
            {
                // WebException.Status holds useful information
                MessageBox.Show(String.Format("Lỗi đường truyền. Nội dung: {0}. \nMã lỗi: {1}.", we.Message, we.Status.ToString()));
            }
            catch (NotSupportedException ne)
            {
                // other errors
                MessageBox.Show(String.Format("Lỗi đường truyền. Nội dung: {0}.", ne.Message));
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi không xác định.");
            }
        }
        private void githubButton_Click(object sender, EventArgs e)
        {
            App.Main_Windows.AboutForm.AboutForm aboutForm = new App.Main_Windows.AboutForm.AboutForm();
            aboutForm.ShowDialog();
        }
        #endregion

        #region Main Buttons
        private void installButton_Click(object sender, EventArgs e)
        {
            if (!isInternetAvailable())
            {
                MessageBox.Show("Không có kết nối mạng, vui lòng thử lại sau.");
                return;
            }

            BaseTab installTab = new InstallTab();
            loadTab(installTab);
        }
        private void uninstallButton_Click(object sender, EventArgs e)
        {
            BaseTab uninstallTab = new UninstallTab();
            loadTab(uninstallTab);
        }
        #endregion

        public void loadTab(UserControl tab)
        {
            this.Controls.Add(tab);
            tab.Dock = DockStyle.Fill;
            tab.BringToFront();
        }

        private static int WM_QUERYENDSESSION = 0x11;
        private static bool systemShutdown = false;
        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.sessionending?redirectedfrom=MSDN&view=dotnet-plat-ext-6.0
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_QUERYENDSESSION)
            {
                systemShutdown = true;
            }
            base.WndProc(ref m);
        }

        private void MainUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.SetStartup = Program.ExitRunBackground.Waiting;
            string textMessageBox = "";
            if (Program.setting.isSetTime && DateTime.Now.Subtract(Program.setting.timeSetter).TotalSeconds <= 0)
            {
                textMessageBox = String.Format(
                    "Bạn có đặt lịch cài đặt vào lúc {0}.\nBạn có muốn lưu lại trạng thái và tiếp tục vào phiên khởi động tiếp theo?",
                    Program.setting.timeSetter.ToString("HH:mm:ss dd/MM/yyyy"));
            }
            else if ((Program.installName != null && Program.installName.Count > 0) || (Program.uninstallName != null && Program.uninstallName.Count > 0))
            {
                textMessageBox = String.Format(
                    "Bạn đang còn {0} phần mềm cần xử lý.\nBạn có muốn lưu lại trạng thái và tiếp tục vào phiên khởi động tiếp theo?",
                    (Program.installName == null ? 0 : Program.installName.Count) + (Program.uninstallName == null ? 0 : Program.uninstallName.Count));
            }
            if (!String.IsNullOrEmpty(textMessageBox))
            {
                switch (MessageBox.Show(textMessageBox, "autoStudent", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        e.Cancel = false;
                        Program.SetStartup = Program.ExitRunBackground.Startup;
                        this.FormClosing -= MainUI_FormClosing;
                        if (systemShutdown)
                        {
                            Program.setting.afterAction = Setting.AfterAction.Shutdown;
                            Program.CurrentDomain_ProcessExit(null, null);
                            Program.setting.RunAfterAction();
                        }
                        break;
                    case DialogResult.No:
                        e.Cancel = false;
                        Program.SetStartup = Program.ExitRunBackground.None;
                        this.FormClosing -= MainUI_FormClosing;
                        if (systemShutdown)
                        {
                            Program.setting.afterAction = Setting.AfterAction.Shutdown;
                            Program.setting.RunAfterAction();
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        Program.SetStartup = Program.ExitRunBackground.None;
                        break;
                }
            }
            else e.Cancel = false;
        }
    }
}
