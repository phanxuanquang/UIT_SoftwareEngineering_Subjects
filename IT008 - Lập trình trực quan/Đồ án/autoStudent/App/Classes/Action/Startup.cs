using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace App
{
    class Startup
    {
        public readonly static string saveHistory = Application.StartupPath + @"\schedule.as";

        public static void WriteSchedule(List<string> installSoftwares, List<string> uninstallSoftwares)
        {
            if (HasScheduled())
            {
                File.Delete(saveHistory);
            }
            Program.setting.RunDataExport(installSoftwares, uninstallSoftwares, saveHistory);
            SetStartupEnviroment();
        }

        public static (bool, List<Package>, List<Package>) ReadSchedule()
        {
            (bool, List<Package>, List<Package>) checkLastRun = (false, null, null);
            if (HasScheduled())
            {
                checkLastRun = Program.setting.RunDataImport(saveHistory);
                File.Delete(saveHistory);
                RemoveStartupEnviroment();

                if (checkLastRun.Item1 && checkLastRun.Item3 != null)
                {
                    List<Package> packages = new List<Package>();
                    for (int index = 0; index < checkLastRun.Item3.Count; index++)
                    {
                        Package item = Program.software_System.FirstOrDefault(items => items.Name == checkLastRun.Item3[index].Name);
                        if (item != null)
                        {
                            packages.Add(item);
                        }
                    }
                    checkLastRun.Item3 = packages;
                }
            }
            return checkLastRun;
        }

        private static bool HasScheduled()
        {
            return File.Exists(saveHistory);
        }

        private static void SetStartupEnviroment()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (registry.GetValue("autoStudent") == null)
            {
                registry.SetValue("autoStudent", Application.ExecutablePath);
            }
        }

        private static void RemoveStartupEnviroment()
        {
            RegistryKey registry = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (registry.GetValue("autoStudent") != null)
            {
                registry.DeleteValue("autoStudent", false);
            }
        }
    }
}
