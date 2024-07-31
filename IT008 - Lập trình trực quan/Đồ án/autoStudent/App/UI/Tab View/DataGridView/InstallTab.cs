using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace App
{
    public partial class InstallTab : BaseTab
    {
        public InstallTab()
        {
            InitializeComponent();
            init();
        }

        protected override void exec()
        {
            if (!Directory.Exists(Program.setting.saveDownloadPath))
            {
                Directory.CreateDirectory(Program.setting.saveDownloadPath);
            }

            List<Package> overlapList = LoadingWindow.GetOverlapSoftware(Program.software_System, selectedSoftwareList);
            if (overlapList != null && overlapList.Count > 0)
            {
                OverlapTab overlapTab = new OverlapTab(overlapList, selectedSoftwareList);
                Program.mainUI.loadTab(overlapTab);
            }

            else
            {
                ProgressWindow_Install progressWindow_Install = new ProgressWindow_Install(selectedSoftwareList);
                progressWindow_Install.ExportData();
                if (Program.setting.dataExport)
                {
                    Program.setting.RunDataExport(Program.installName, null, Program.setting.exportPath);
                }
                Program.setting.CheckTimeOut(progressWindow_Install);
            }
        }

        protected override void init()
        {
            softwareList = Program.software_Database;
            loadSoftwareToGridView(softwareList);
        }
    }
}
