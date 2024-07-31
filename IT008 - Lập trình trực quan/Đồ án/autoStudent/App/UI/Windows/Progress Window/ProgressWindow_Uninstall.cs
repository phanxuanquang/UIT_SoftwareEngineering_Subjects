using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class ProgressWindow_Uninstall : ProgressWindow_Base
    {
        private InstallUninstall.BaseProcess uninstall;
        public ProgressWindow_Uninstall(List<Package> listSoftware) : base(listSoftware)
        {
            InitializeComponent();

            base.softwareGridView.Columns.Add(base.NameSoftware);
            base.softwareGridView.Columns.Add(base.StatusProcess);
            base.softwareGridView.Columns.Add(base.ActionButton);

            LoadDataGridView();

            uninstall = new InstallUninstall.Uninstall();
            uninstall.Start(listSoftware);

            this.Shown += ProgressWindow_Uninstall_Shown;
        }

        private void ProgressWindow_Uninstall_Shown(object sender, EventArgs e)
        {
            this.Shown -= ProgressWindow_Uninstall_Shown;
            ToDo();
        }

        #region Overrided Functions
        protected override void LoadDataGridView()
        {
            if (base.listSoftware != null)
            {
                for(int index = 0; index < base.listSoftware.Count; index++)
                {
                    base.softwareGridView.Rows.Add(base.listSoftware[index].Displayname, GetImageStatus(StatusDataGridView.Ready), "HỦY");
                }
            }
        }

        protected override void ToDo()
        {
            Task.Factory.StartNew(() =>
            {
                int index = -1;
                while ((index = blackList.IndexOf(ActionProcess.None)) != -1)
                {
                    while (Program.SetStartup == Program.ExitRunBackground.Waiting)
                    {
                        Thread.Sleep(1000);
                    }
                    if (Program.SetStartup == Program.ExitRunBackground.Startup) return;
                    uninstall.RunProcess(index);
                    UpdateStatusProcess(index, StatusDataGridView.Uninstalling);
                    PopExportData(listSoftware[index].Name);
                    UpdateStatusStrip(String.Format("Đang gỡ cài đặt: {0}", base.listSoftware[index].Displayname));
                    while (!uninstall.isCompleted)
                    {
                        Thread.Sleep(1000);
                    }
                    UpdateStatusProcess(index, StatusDataGridView.Completed);
                    UpdateCompletedAmount(++countCompletedAmount, 0);
                    blackList[index] = ActionProcess.Done;
                }
                UpdateStatusStrip("Hoàn thành");
                HasExitTodoTask = true;
            });
        }

        public override void ExportData()
        {
            if (base.listSoftware != null && this is ProgressWindow_Uninstall)
            {
                if (Program.uninstallName == null) Program.uninstallName = new List<string>();
                else Program.uninstallName.Clear();
                if (!isOverlap && Program.installName != null) Program.installName.Clear();
                for (int index = 0; index < this.listSoftware.Count; index++)
                {
                    Program.uninstallName.Add(this.listSoftware[index].Name);
                }
            }
        }

        protected override void PopExportData(string namePackage)
        {
            if (base.listSoftware != null && this is ProgressWindow_Uninstall)
            {
                if (Program.uninstallName != null)
                {
                    Program.uninstallName.Remove(namePackage);
                }
            }
        }

        #endregion
    }
}
