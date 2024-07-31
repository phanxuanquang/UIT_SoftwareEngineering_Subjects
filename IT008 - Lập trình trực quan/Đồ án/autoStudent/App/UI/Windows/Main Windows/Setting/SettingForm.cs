using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            this.Icon = App.Properties.Resources.autoStudent;

            timeSetter.Value = Program.setting.timeSetter;
            timeSetter.Checked = Program.setting.isSetTime;
            activatedAction.SelectedIndex = ((int)Program.setting.afterAction);
            cleanAfterCompleted_Switch.Checked = Program.setting.cleanAfter;
            timeSetter_Switch.Checked = Program.setting.isSetTime;
            saveDownload.Text = Program.setting.saveDownloadPath;
            exportPath.Visible = exportPath_Button.Visible = dataExportAfterCompleted_Switch.Checked = Program.setting.dataExport;
            exportPath.Text = Program.setting.exportPath;
        }

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

        #region Setting Options
        private void timeSetter_Switch_CheckedChanged(object sender, EventArgs e)
        {
            Program.setting.isSetTime = timeSetter_Switch.Checked;
            timeSetter.Visible = timeSetter_Switch.Checked;
        }
        private void timeSetter_ValueChanged(object sender, EventArgs e)
        {
            Program.setting.timeSetter = timeSetter.Value;
        }

        private void activatedAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.setting.afterAction = ((Setting.AfterAction)activatedAction.SelectedIndex);
        }
        private void cleanAfterCompleted_Switch_CheckedChanged(object sender, EventArgs e)
        {
            Program.setting.cleanAfter = cleanAfterCompleted_Switch.Checked;
        }

        private void dataExportAfterCompleted_Switch_CheckedChanged(object sender, EventArgs e)
        {
            Program.setting.dataExport = dataExportAfterCompleted_Switch.Checked;
            exportPath.Visible = exportPath_Button.Visible = dataExportAfterCompleted_Switch.Checked;
        }
        private void exportPath_TextChanged(object sender, EventArgs e)
        {
            Program.setting.exportPath = exportPath.Text;
        }
        private void exportPath_Button_Click(object sender, EventArgs e)
        {
            SaveFileDialog destinationPathdlg = new SaveFileDialog();
            destinationPathdlg.Title = "Save AutoStudentDataExport";
            destinationPathdlg.Filter = "AS files (*.as)|*.as";
            if (destinationPathdlg.ShowDialog() == DialogResult.OK)
            {
                exportPath.Text = destinationPathdlg.FileName;
            }
        }

        private void saveDownload_TextChanged(object sender, EventArgs e)
        {
            Program.setting.saveDownloadPath = saveDownload.Text;
        }
        private void saveDownload_Button_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog destinationPathdlg = new CommonOpenFileDialog();
            destinationPathdlg.IsFolderPicker = true;
            if (destinationPathdlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                saveDownload.Text = destinationPathdlg.FileName + @"\autoStudent";
            }
        }
        #endregion

        #region Buttons
        private void exitButton_Click(object sender, EventArgs e)
        {
            Program.setting.exportSetting();
            this.Close();
        }

        private void defaultSetting_Button_Click(object sender, EventArgs e)
        {
            cleanAfterCompleted_Switch.Checked = false;
            activatedAction.SelectedIndex = 0;
            timeSetter_Switch.Checked = false;
            dataExportAfterCompleted_Switch.Checked = false;

            timeSetter.Value = DateTime.Now;
            saveDownload.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\autoStudent";
            exportPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AutoStudentDataExport.as";
        }
        #endregion
    }
}

