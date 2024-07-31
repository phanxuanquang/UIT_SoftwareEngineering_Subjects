using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App.InstallUninstall
{
    abstract class BaseProcess
    {
        protected List<Package> listSoftware;
        protected TrackingProcess tracking;
        protected bool statusProcess;

        public BaseProcess()
        {
            tracking = new TrackingProcess();
        }

        public void Start(List<Package> listSoftware)
        {
            this.listSoftware = listSoftware;
        }

        public bool isCompleted
        {
            get
            {
                return statusProcess;
            }
        }

        public abstract void RunProcess(int index);

        protected bool CreateProcess(GetPath.NewProcess newProcess)
        {
            if (newProcess != null)
            {
                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = newProcess.FileName;
                processStartInfo.Arguments = newProcess.Arguments;
                processStartInfo.UseShellExecute = false;
                processStartInfo.Verb = "runas";
                process.StartInfo = processStartInfo;
                process.Start();
                tracking.Tracking(process.Id);
                return true;
            }
            return false;
        }
    }
}
