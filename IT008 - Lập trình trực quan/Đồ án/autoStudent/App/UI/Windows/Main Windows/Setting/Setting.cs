using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using Newtonsoft.Json;
using System.IO;
using System.Threading;

namespace App
{
    class Setting
    {
        #region Variables Declaration
        public enum AfterAction
        {
            Shutdown = 5,
            Restart = 4,
            Sleep = 3,
            Lock = 2,
            Exit = 1,
            None = 0
        }
        private DateTime _timeSetter;
        private AfterAction _afterAction;
        private bool _cleanAfter;
        private bool _dataExport;
        private bool _isSetTime;
        private string _saveDownloadPath;
        private string _exportPath;
        private string _settingFilePath;
        public DateTime timeSetter
        {
            get
            {
                return _timeSetter;
            }
            set
            {
                _timeSetter = value;
            }
        }
        public AfterAction afterAction
        {
            get
            {
                return _afterAction;
            }
            set
            {
                _afterAction = value;
            }
        }
        public bool cleanAfter
        {
            get
            {
                return _cleanAfter;
            }
            set
            {
                _cleanAfter = value;
            }
        }
        public bool dataExport
        {
            get
            {
                return _dataExport;
            }
            set
            {
                _dataExport = value;
            }
        }
        public string saveDownloadPath
        {
            get
            {
                return _saveDownloadPath;
            }
            set
            {
                _saveDownloadPath = value;
            }
        }
        public bool isSetTime
        {
            get
            {
                return _isSetTime;
            }
            set
            {
                _isSetTime = value;
            }
        }
        public string exportPath
        {
            get
            {
                return _exportPath;
            }
            set
            {
                _exportPath = value;
            }
        }
        public string settingFilePath
        {
            get
            {
                return _settingFilePath;
            }
            set
            {
                _settingFilePath = value;
            }
        }
        #endregion

        public Setting(DateTime dateTime)
        {
            settingFilePath = Application.StartupPath + @"/Setting/Setting.setting";
            _timeSetter = dateTime;
            importSetting();
            
        }

        public Setting()
        {
            _timeSetter = DateTime.Now;
            _afterAction = AfterAction.None;
            _cleanAfter = false;
            _isSetTime = false;
            _dataExport = false;
            _saveDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\autoStudent";
            _exportPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AutoStudentDataExport.as";
        }

        #region Actions
        // For Lock
        [DllImport("user32")]
        public static extern void LockWorkStation();

        // For Sleep
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hiberate, bool forceCritical, bool disableWakeEvent);

        public void RunAfterAction()
        {
            switch (afterAction)
            {
                case AfterAction.Exit:
                    if (Program.mainUI != null)
                    {
                        Program.mainUI.Close();
                    }
                    break;
                case AfterAction.Shutdown:
                    Process.Start("shutdown", "/s /t 0");
                    break;
                case AfterAction.Restart:
                    Process.Start("shutdown", "/r /t 0");
                    break;
                case AfterAction.Lock:
                    LockWorkStation();
                    break;
                case AfterAction.Sleep:
                    SetSuspendState(false, true, true);
                    break;
                default:
                    return;
            }
        }

        public bool RunDataExport(List<string> installSoftwares, List<string> uninstallSoftwares, string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                string passExport = DataAccess.Instance.GetPassCry();
                try
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        string encrypt = Cryptography.Encrypt(GetData(installSoftwares, "INSTALL") + GetData(uninstallSoftwares, "UNINSTALL"), passExport);
                        sw.Write(encrypt);
                    }
                    return true;
                }
                catch (IOException)
                {
                    MessageBox.Show("Lỗi lưu tệp tin.");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Không có quyền lưu thư mục được chọn.");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi không xác định.");
            }
            return false;
        }

        public (bool, List<Package>, List<Package>) RunDataImport(string filePath)
        {
            string passImport = DataAccess.Instance.GetPassCry();
            if (File.Exists(filePath))
            {
                try
                {
                    List<string> install = null;
                    List<string> uninstall = null;
                    using (StreamReader sr = File.OpenText(filePath))
                    {
                        string dataImport = sr.ReadToEnd();
                        string[] decrypt = Cryptography.Decrypt(dataImport, passImport).Split('\n');
                        int indexInstall = Array.IndexOf(decrypt, "INSTALL");
                        int indexUninstall = Array.IndexOf(decrypt, "UNINSTALL");
                        if (indexUninstall - indexInstall > 1)
                        {
                            install = new List<string>();
                            for (int index = indexInstall + 1; index < indexUninstall; index++)
                            {
                                install.Add(decrypt[index]);
                            }
                        }
                        if (decrypt.Length - indexUninstall > 1)
                        {
                            uninstall = new List<string>();
                            for (int index = indexUninstall + 1; index < decrypt.Length; index++)
                            {
                                uninstall.Add(decrypt[index]);
                            }
                        }
                    }

                    if (install == null && uninstall == null) return (false, null, null);
                    return (true, install == null ? null : DataAccess.Instance.GetPackagesOfName(install),
                        uninstall == null ? null : DataAccess.Instance.GetPackagesOfName(uninstall));

                }
                catch (IOException)
                {
                    MessageBox.Show("Lỗi đọc tệp tin.");
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Không có quyền đọc thư mục được chọn.");
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi không xác định.");
                }
            }
            else
            {
                MessageBox.Show("Không tồn tại thư mục.");
            }
            return (false, null, null);
        }

        private string GetData(List<string> packages, string nameField)
        {
            if (packages != null)
            {
                string data = nameField + "\n";
                for (int index = 0; index < packages.Count; index++)
                {
                    data += packages[index] + "\n";
                }
                return data;
            }
            return nameField + "\n";
        }

        public void cleanComputer()
        {
            if (Directory.Exists(saveDownloadPath))
            {
                void deleteFileIn(string path)
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    foreach (FileInfo file in di.EnumerateFiles())
                    {
                        file.Delete();
                    }
                    foreach (DirectoryInfo dir in di.EnumerateDirectories())
                    {
                        dir.Delete(true);
                    }
                }
                try
                {
                    deleteFileIn(Program.setting.saveDownloadPath);
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("Không có quyền xóa thư mục tạm thời.");
                }
                catch (IOException)
                {
                    MessageBox.Show("Lỗi xóa thư mục tạm thời.");
                }
                catch { }
            }
        }

        public void CheckTimeOut(ProgressWindow_Base action)
        {
            if (action != null)
            {
                if ((Program.setting.isSetTime && DateTime.Now.Subtract(Program.setting.timeSetter).TotalSeconds >= 0) || !Program.setting.isSetTime)
                {
                    action.Show();
                }
                else
                {
                    action.SetRunBackground(true, true);
                    Task.Factory.StartNew(() =>
                    {
                        while (Program.setting.isSetTime && DateTime.Now.Subtract(Program.setting.timeSetter).TotalSeconds <= 0)
                        {
                            Thread.Sleep(1000);
                        }
                        if (action != null && !action.Visible)
                        {
                            try
                            {
                                action.Show();
                            }
                            catch
                            {
                                try
                                {
                                    action.BeginInvoke(new Action(() =>
                                    {
                                        action.Show();
                                    }));
                                }
                                catch { }
                            }
                            action.SetRunBackground(true, false);
                        }
                    });
                }
            }
        }
        #endregion

        #region Setting Data
        public void importSetting()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(settingFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(settingFilePath));
                }
                if (!File.Exists(settingFilePath))
                {
                    var setting = new Setting();
                    using (var stream = File.Create(settingFilePath))
                    {
                        string content = JsonConvert.SerializeObject(setting);
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(Cryptography.Encrypt(content, DataAccess.Instance.GetPassCry()));
                        }
                    }
                }
                using (var stream = File.OpenRead(settingFilePath))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string data = reader.ReadToEnd();
                        Setting info = JsonConvert.DeserializeObject<Setting>(Cryptography.Decrypt(data, DataAccess.Instance.GetPassCry()));
                        this._cleanAfter = info.cleanAfter;
                        this._dataExport = info.dataExport;
                        this._saveDownloadPath = info.saveDownloadPath;
                        this._exportPath = info.exportPath;
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(String.Format("Lỗi không xác định. Nội dung lỗi: {0}.", e.ToString()));
            }

        }

        public void exportSetting()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(settingFilePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(settingFilePath));
                }
                if (!File.Exists(settingFilePath))
                {
                    using (var stream = File.Create(settingFilePath))
                    {
                        string content = JsonConvert.SerializeObject(this);
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(Cryptography.Encrypt(content, DataAccess.Instance.GetPassCry()));
                        }
                    }
                }
                else
                {
                    using (StreamWriter writer = new StreamWriter(settingFilePath))
                    {
                        string content = JsonConvert.SerializeObject(this);
                        writer.Write(Cryptography.Encrypt(content, DataAccess.Instance.GetPassCry()));
                    }
                }

            }
            catch (Exception e)
            {

                MessageBox.Show(String.Format("Lỗi không xác định. Nội dung lỗi: {0}.", e.ToString()));
            }

        }
        #endregion
    }
}