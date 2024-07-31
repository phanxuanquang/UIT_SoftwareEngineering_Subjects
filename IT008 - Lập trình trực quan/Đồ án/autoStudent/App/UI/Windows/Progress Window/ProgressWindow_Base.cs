using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace App
{
    public partial class ProgressWindow_Base : Form
    {
        #region Declaration
        protected RunBackground runBackground;
        protected List<Package> listSoftware;
        protected List<ActionProcess> blackList;
        protected int countCompletedAmount;
        private bool _HasExitTodoTask;
        protected bool HasExitTodoTask
        {
            get
            {
                return _HasExitTodoTask;
            }
            set
            {
                _HasExitTodoTask = value;
                try
                {
                    if (_HasExitTodoTask
                    && !isOverlap
                    && completedAmountLabel.Text != String.Format("{0}/{1}", listSoftware.Count, listSoftware.Count))
                    {
                        ActionCompleted();
                    }
                }
                catch (Exception) { }
            }
        }
        public bool isOverlap = false;
        protected static bool wasRunBackground = false;

        private static readonly Image Ready = Properties.Resources.Ready;
        private static readonly Image Download = Properties.Resources.Download;
        private static readonly Image Install = Properties.Resources.Install;
        private static readonly Image Uninstall = Properties.Resources.Uninstall;
        private static readonly Image Complete = Properties.Resources.Complete;
        private static readonly Image Cancel = Properties.Resources.Cancel;
        private static readonly Image Fail = Properties.Resources.Fail;
        public bool PressedActionAll;

        public enum StatusDataGridView
        {
            None,
            Ready,
            Downloading,
            Installing,
            Uninstalling,
            Completed,
            Canceled,
            Failed
        }
        public enum ActionProcess
        {
            Done,
            Canceled,
            None
        }
        #endregion

        public ProgressWindow_Base(List<Package> listSoftware) : this()
        {
            SetListSoftware(listSoftware);
        }

        public ProgressWindow_Base()
        {
            InitializeComponent();
            Guna.UI.Lib.GraphicsHelper.ShadowForm(this);
            this.Icon = App.Properties.Resources.autoStudent;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.runBackground = new RunBackground(this, this.components);
            foreach(Control control in this.Controls)
            {
                Program.SetDoubleBuffered(control);
            }
            if (Program.mainUI != null)
            {
                Program.mainUI.Hide();
            }
        }

        private void ProgressWindow_Base_Shown(object sender, EventArgs e)
        {
            if (Program.setting != null)
            {
                Program.setting.isSetTime = false;
            }
        }

        #region Windows State
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleParam = base.CreateParams;
                handleParam.ExStyle |= 0x02000000;
                return handleParam;
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            if (HasExitTodoTask)
            {
                LoadingWindow.LoadAfterDone();
                if (Program.mainUI != null)
                {
                    Program.mainUI.Show();
                }
                if (PressedActionAll)
                {
                    if (Program.installName != null) Program.installName.Clear();
                    if (Program.uninstallName != null) Program.uninstallName.Clear();
                }
                this.Close();
            }
            else backgroundRunning_Button_Click(this, null);
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion

        #region Functions
        public void SetListSoftware(List<Package> listSoftware)
        {
            this.listSoftware = listSoftware;
            countCompletedAmount = 0;
            UpdateCompletedAmount(countCompletedAmount, 0);

            blackList = new List<ActionProcess>();
            for (int index = 0; index < this.listSoftware.Count; index++)
            {
                blackList.Add(ActionProcess.None);
            }
        }

        public virtual void ExportData() { }

        protected virtual void PopExportData(string namePackage) { }

        protected Image GetImageStatus(StatusDataGridView status)
        {
            Image result = null;
            switch (status)
            {
                case StatusDataGridView.Ready:
                    result = Ready;
                    break;
                case StatusDataGridView.Downloading:
                    result = Download;
                    break;
                case StatusDataGridView.Installing:
                    result = Install;
                    break;
                case StatusDataGridView.Uninstalling:
                    result = Uninstall;
                    break;
                case StatusDataGridView.Completed:
                    result = Complete;
                    break;
                case StatusDataGridView.Canceled:
                    result = Cancel;
                    break;
                case StatusDataGridView.Failed:
                    result = Fail;
                    break;
                case StatusDataGridView.None:
                    break;
            }
            return result;
        }
        protected virtual void LoadDataGridView() { }
        protected virtual void ToDo() { }
        #endregion

        #region Update
        protected void UpdateCompletedAmount(int value, float percentOfValue)
        {
            if (listSoftware != null && listSoftware.Count > 0 && value >= 0 && listSoftware.Count > 0)
            {
                try
                {
                    progressBar.Value = Convert.ToInt32((value * 100.0f + percentOfValue) / listSoftware.Count);
                    completedAmountLabel.Text = String.Format("{0}/{1}", value, listSoftware.Count);
                }
                catch
                {
                    try
                    {
                        progressBar.BeginInvoke(new Action(() =>
                        {
                            progressBar.Value = Convert.ToInt32(value * 100.0f / listSoftware.Count);
                        }));
                        completedAmountLabel.BeginInvoke(new Action(() =>
                        {
                            completedAmountLabel.Text = String.Format("{0}/{1}", value, listSoftware.Count);
                        }));
                    }
                    catch { }
                }
            }
        }

        protected void UpdateStatusProcess(int index, StatusDataGridView status)
        {
            UpdateActionButton(index, status);
            if (listSoftware != null && listSoftware.Count > 0 && index > -1 && index < listSoftware.Count)
            {
                try
                {
                    softwareGridView.Rows[index].Cells[softwareGridView.Columns.Count - 2].Value = GetImageStatus(status);
                }
                catch
                {
                    try
                    {
                        softwareGridView.BeginInvoke(new Action(() =>
                        {
                            softwareGridView.Rows[index].Cells[softwareGridView.Columns.Count - 2].Value = GetImageStatus(status);
                        }));
                    }
                    catch { }
                }
            }
        }

        protected void UpdateActionButton(int index, StatusDataGridView status)
        {
            if (status == StatusDataGridView.Uninstalling || status == StatusDataGridView.Installing)
            {
                if (listSoftware != null && listSoftware.Count > 0 && index > -1 && index < listSoftware.Count)
                {
                    try
                    {
                        ((DataGridViewDisableButtonCell)softwareGridView.Rows[index].Cells[softwareGridView.Columns.Count - 1]).Enabled = false;
                        softwareGridView.Refresh();
                    }
                    catch
                    {
                        try
                        {
                            softwareGridView.BeginInvoke(new Action(() =>
                            {
                                ((DataGridViewDisableButtonCell)softwareGridView.Rows[index].Cells[softwareGridView.Columns.Count - 1]).Enabled = false;
                                softwareGridView.Refresh();
                            }));
                        }
                        catch { }
                    }
                }
            }
        }
        #endregion

        #region GridView
        private void softwareGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1 && e.RowIndex < listSoftware.Count)
            {
                ActionButton_TextChanged(e.RowIndex, e.ColumnIndex, blackList[e.RowIndex] == ActionProcess.None ? ActionProcess.Canceled : ActionProcess.None);
            }
        }

        protected void ActionButton_TextChanged(int row, int column, ActionProcess action)
        {
            if (column == softwareGridView.Columns.Count - 1 && row > -1 && row < listSoftware.Count)
            {
                if (((DataGridViewDisableButtonCell)softwareGridView.Rows[row].Cells[softwareGridView.Columns.Count - 1]).Enabled)
                {
                    string value = "";
                    switch (action)
                    {
                        case ActionProcess.None:
                            value = "HỦY";
                            break;
                        case ActionProcess.Done:
                            value = "HỦY";
                            break;
                        case ActionProcess.Canceled:
                            value = "KHÔI PHỤC";
                            break;
                    }
                    try
                    {
                        softwareGridView.Rows[row].Cells[softwareGridView.Columns.Count - 1].Value = value;
                    }
                    catch
                    {
                        try
                        {
                            softwareGridView.BeginInvoke(new Action(() =>
                            {
                                softwareGridView.Rows[row].Cells[softwareGridView.Columns.Count - 1].Value = value;
                            }));
                        }
                        catch { }
                    }
                    blackList[row] = action;
                    if (blackList[row] == ActionProcess.None && HasExitTodoTask)
                    {
                        ToDo();
                    }
                }
            }
        }
        #endregion

        #region Main Buttons

        public void SetRefresh()
        {
            detail_Button_Click(null, null);
        }

        public void backgroundRunning_Button_Click(object sender, EventArgs e)
        {
            SetRunBackground(true, false);
        }

        public void SetRunBackground(bool showToolTip, bool overTipText)
        {
            if (runBackground != null)
            {
                wasRunBackground = runBackground.Visible;
                runBackground.EnableRunBackground(showToolTip, overTipText);
            }
            else MessageBox.Show("Không thể chạy ngầm!");
        }

        private void softwareGridView_SelectionChanged(object sender, EventArgs e)
        {
            softwareGridView.ClearSelection();
        }

        private void completedAmountLabel_TextChanged(object sender, EventArgs e)
        {
            if (completedAmountLabel.Text == String.Format("{0}/{1}", listSoftware.Count, listSoftware.Count))
            {
                if (isOverlap)
                {
                    wasRunBackground = runBackground.Visible;
                    if (wasRunBackground)
                    {
                        runBackground.OverrideNotify();
                    }

                    Task.Factory.StartNew(() =>
                    {
                        while (Program.SetStartup == Program.ExitRunBackground.Waiting) ;
                        if (Program.SetStartup == Program.ExitRunBackground.Startup) return;
                        try
                        {
                            this.Close();
                        }
                        catch
                        {
                            try
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    this.Close();
                                }));
                            }
                            catch { }
                        }
                    });
                }
                else
                {
                    ActionCompleted();
                }
            }
        }

        private void ActionCompleted()
        {
            if (Program.setting.cleanAfter)
                Program.setting.cleanComputer();
            System.Media.SoundPlayer completeSound = new System.Media.SoundPlayer(Properties.Resources.Complete_Sound);
            completeSound.Play();
            if (Program.setting.afterAction != Setting.AfterAction.None && Program.setting.afterAction != Setting.AfterAction.Sleep)
            {
                if (Program.installName != null) Program.installName.Clear();
                if (Program.uninstallName != null) Program.uninstallName.Clear();
            }
            Program.setting.RunAfterAction();
            MessageBox.Show("Hoàn thành!");
        }
         
        private void detail_Button_Click(object sender, EventArgs e)
        {
            this.SuspendLayout();
            this.processContainPanel.SuspendLayout();
            if (softwareGridView.Visible)
            {
                this.ClientSize = new System.Drawing.Size(819, 195);
                detail_Button.Image = App.Properties.Resources.Detail_2;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(819, 492);
                detail_Button.Image = App.Properties.Resources.Detail;
            }
            softwareGridView.Visible = !softwareGridView.Visible;
            Application.DoEvents();
            this.processContainPanel.ResumeLayout(true);
            this.ResumeLayout(true);
        }

        private void ActionAll_Button_Click(object sender, EventArgs e)
        {
            PressedActionAll = !PressedActionAll;
            for (int index = 0; index < blackList.Count; index++)
            {
                ActionButton_TextChanged(index, softwareGridView.Columns.Count - 1, PressedActionAll ? ActionProcess.Canceled : ActionProcess.None);
            }
            try
            {
                ActionAll_Button.Text = PressedActionAll ? "KHÔI PHỤC TB" : "HỦY TOÀN BỘ";
            }
            catch
            {
                try
                {
                    ActionAll_Button.BeginInvoke(new Action(() =>
                    {
                        ActionAll_Button.Text = PressedActionAll ? "KHÔI PHỤC TB" : "HỦY TOÀN BỘ";
                    }));
                }
                catch { }
            }
        }
        #endregion

        #region StatusStrip

        protected void UpdateStatusStrip(string actionDoing)
        {
            try
            {
                if (actionDoing != "")
                {
                    StatusDoing.Text = actionDoing;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi cập nhật thanh trạng thái.");
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.mainUI.Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.Main_Windows.AboutForm.AboutForm aboutForm = new Main_Windows.AboutForm.AboutForm();
            aboutForm.ShowDialog();
        }

        private void SettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            App.SettingForm settingForm = new SettingForm();
            settingForm.ShowDialog();
        }

        #endregion

    }

    #region Other Classes
    /// <summary>
    /// Disable button data grid view
    /// link: https://docs.microsoft.com/en-us/dotnet/desktop/winforms/controls/disable-buttons-in-a-button-column-in-the-datagrid?view=netframeworkdesktop-4.8
    /// </summary>
    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
    }

    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get
            {
                return enabledValue;
            }
            set
            {
                enabledValue = value;
            }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell =
                (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        // By default, enable the button cell.
        public DataGridViewDisableButtonCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The button cell is disabled, so paint the border,
            // background, and disabled button for the cell.
            if (!this.enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                }

                // Calculate the area in which to draw the button.
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                // Draw the disabled button.
                ButtonRenderer.DrawButton(graphics, buttonArea,
                    PushButtonState.Disabled);

                // Draw the disabled button text.
                if (this.FormattedValue is String)
                {
                    TextRenderer.DrawText(graphics,
                        (string)this.FormattedValue,
                        this.DataGridView.Font,
                        buttonArea, SystemColors.GrayText);
                }
            }
            else
            {
                // The button cell is enabled, so let the base class
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
    #endregion
}
