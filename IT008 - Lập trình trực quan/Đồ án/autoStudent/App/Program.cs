using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [STAThread]
         
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            (bool, uint) checkCurrentRunning = CheckProcessExists();
            if (checkCurrentRunning.Item1)
            {
                MessageBox.Show("Bạn đang mở một cửa số khác.\nVui lòng quay lại cửa sổ trước", "autoStudent. Đã chạy chương trình rồi!", MessageBoxButtons.OK);
                SetForegroundWindow(Process.GetProcessById(Convert.ToInt32(checkCurrentRunning.Item2)).MainWindowHandle);
                return;
            }

            LoadingWindow loading = new LoadingWindow();
            Application.Run(loading);

            (bool, List<Package>, List<Package>) checkLastRun = Startup.ReadSchedule();
            if (checkLastRun.Item1)
            {
                installSchedule = checkLastRun.Item2;
                uninstallSchedule = checkLastRun.Item3;
            }

            if (loading.isDone)
            {
                mainUI = new MainUI();
                Application.Run(mainUI);
            }
        }

        #region Declaration
        static public List<Package> software_Database;
        static public List<Package> software_System;
        static public MainUI mainUI;
        static public Setting setting;
        public enum ExitRunBackground
        {
            Startup,
            Waiting,
            None
        };
        public static ExitRunBackground SetStartup = ExitRunBackground.None;
        public static List<string> installName;
        public static List<string> uninstallName;
        public static List<Package> installSchedule;
        public static List<Package> uninstallSchedule;
        #endregion

        public static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if ((setting.isSetTime && SetStartup == ExitRunBackground.Startup && DateTime.Now.Subtract(setting.timeSetter).TotalSeconds <= 0)
                || (Program.installName != null && Program.installName.Count > 0) || (Program.uninstallName != null && Program.uninstallName.Count > 0))
            {
                Startup.WriteSchedule(installName, uninstallName);
            }
        }

        private static (bool, uint) CheckProcessExists()
        {
            string query = "SELECT ProcessID, ExecutablePath FROM Win32_Process";
            using (ManagementObjectSearcher seacher = new ManagementObjectSearcher(query))
            using (ManagementObjectCollection collection = seacher.Get())
            {
                if (collection != null && collection.Count > 0)
                {
                    uint PIDExists = 0;
                    uint thisPID = Convert.ToUInt32(Process.GetCurrentProcess().Id);
                    foreach (var item in collection)
                    {
                        if (Application.ExecutablePath == (string)item.GetPropertyValue("ExecutablePath"))
                        {
                            if (thisPID != (uint)item.GetPropertyValue("ProcessID"))
                            {
                                PIDExists = (uint)item.GetPropertyValue("ProcessID");
                                break;
                            }
                        }
                    }
                    return PIDExists > 0 ? (true, PIDExists) : (false, 0);
                }
            }
            return (false, 0);
        }

        public static void SetDoubleBuffered(System.Windows.Forms.Control c)
        {
            //Taxes: Remote Desktop Connection and painting
            //http://blogs.msdn.com/oldnewthing/archive/2006/01/03/508694.aspx
            //https://stackoverflow.com/questions/76993/how-to-double-buffer-net-controls-on-a-form/77233#77233
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            System.Reflection.PropertyInfo aProp =
                  typeof(System.Windows.Forms.Control).GetProperty(
                        "DoubleBuffered",
                        System.Reflection.BindingFlags.NonPublic |
                        System.Reflection.BindingFlags.Instance);

            aProp.SetValue(c, true, null);
        }
    }
}
