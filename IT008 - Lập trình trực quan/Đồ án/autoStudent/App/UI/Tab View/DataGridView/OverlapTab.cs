using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class OverlapTab : UserControl
    {
        List<Package> overlapList;
        List<Package> softwareList;
        List<Package> selectedSoftwareList = new List<Package>();
        public bool isExitByButton = true;

        public OverlapTab(List<Package> overlapList, List<Package> softwareList)
        {
            InitializeComponent();
            this.overlapList = overlapList;
            this.softwareList = softwareList;
            loadSoftwareToGridView(overlapList);
        }

        protected void loadSoftwareToGridView(List<Package> softwareList)
        {
            originalGridView.Rows.Clear();
            for (int i = 0; i < softwareList.Count; i++)
            {
                originalGridView.Rows.Add(softwareList[i].Displayname);
            }
        }
        private List<Package> DeleteSoftware()
        {
            if (finalGridView.Rows.Count > 0)
            {
                for (int j = 0; j < selectedSoftwareList.Count; j++)
                {
                    for (int i = 0; i < softwareList.Count; i++)
                    {
                        if (softwareList[i].Name == selectedSoftwareList[j].Name)
                        {
                            softwareList.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
            return softwareList;
        }

        #region GridView Data Changing
        private void originalGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < overlapList.Count; i++)
                {
                    if (overlapList[i].Displayname == originalGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        selectedSoftwareList.Add(overlapList[i]);
                        finalGridView.Rows.Add(selectedSoftwareList[selectedSoftwareList.Count - 1].Displayname);
                        overlapList.RemoveAt(i);
                        originalGridView.Rows.RemoveAt(i);
                        return;
                    }
                }
            }
        }

        private void finalGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                for (int i = 0; i < selectedSoftwareList.Count; i++)
                {
                    if (selectedSoftwareList[i].Displayname == finalGridView.Rows[e.RowIndex].Cells[0].Value.ToString())
                    {
                        overlapList.Add(selectedSoftwareList[i]);
                        originalGridView.Rows.Add(overlapList[overlapList.Count - 1].Displayname);
                        selectedSoftwareList.RemoveAt(i);
                        finalGridView.Rows.RemoveAt(i);
                        return;
                    }
                }
            }
        }
        #endregion

        #region Buttons
        private void exitButton_Click(object sender, EventArgs e)
        {
            isExitByButton = true;
            this.Parent.Controls.Remove(this);
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            isExitByButton = false;
            DeleteSoftware();
            ProgressWindow_Install progressWindow_Install = new ProgressWindow_Install(softwareList);
            if (overlapList.Count > 0)
            {
                ProgressWindow_Uninstall progressWindow_Uninstall = new ProgressWindow_Uninstall(overlapList);
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
                Program.mainUI.Controls.Remove(this);
                progressWindow_Uninstall.ExportData();
                progressWindow_Install.ExportData();
                if (Program.setting.dataExport)
                {
                    Program.setting.RunDataExport(Program.installName, null, Program.setting.exportPath);
                }
                Program.setting.CheckTimeOut(progressWindow_Uninstall);
            }
            else
            {
                progressWindow_Install.FormClosing += (sender, e) =>
                {
                    Program.mainUI.Show();
                };
                Program.setting.CheckTimeOut(progressWindow_Install);
            }
        }
        #endregion
    }
}
