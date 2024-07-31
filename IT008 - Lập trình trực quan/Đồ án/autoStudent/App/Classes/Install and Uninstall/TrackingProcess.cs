using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace App.InstallUninstall
{
    class TrackingProcess
    {
        private string _pid;
        private int pidNewProcess;
        private bool running;
        public delegate void delegateAction(object sender, EventArgs e);
        private bool _isProcessing;
        private bool _isCompleted;
        private bool isProcessing
        {
            get
            {
                return _isProcessing;
            }
            set
            {
                if (_isProcessing)
                {
                    if (value != _isProcessing)
                    {
                        _isCompleted = true;
                        running = false;
                        _isProcessing = false;
                    }
                }
                else _isProcessing = value;
            }
        }

        public bool isCompleted
        {
            get
            {
                return _isCompleted;
            }
        }

        public void Tracking(int PID)
        {
            _isCompleted = false;
            running = false;
            this.pidNewProcess = PID;
            Start();
        }

        private void Start()
        {
            running = true;
            Task.Factory.StartNew(() =>
            {
                while (running)
                {
                    _pid = "";
                    SearchPID(ref _pid, this.pidNewProcess);
                    SearchPID(ref _pid, Process.GetCurrentProcess().Id);
                    Analyst();
                    Thread.Sleep(200);
                }
            });
        }

        private void SearchPID(ref string pid, int PID)
        {
            if (PID > 0)
            {
                string query = "SELECT ProcessID FROM Win32_Process WHERE ParentProcessId = " + PID;
                using (ManagementObjectSearcher seacher = new ManagementObjectSearcher(query))
                using (ManagementObjectCollection collection = seacher.Get())
                {
                    if (collection != null && collection.Count > 0)
                    {
                        foreach (var item in collection)
                        {
                            pid += item.GetPropertyValue("ProcessID") + "\t";
                            SearchPID(ref pid, int.Parse(item.GetPropertyValue("ProcessID").ToString()));
                        }
                    }
                }
            }
        }

        private void Analyst()
        {
            isProcessing = !String.IsNullOrEmpty(_pid);
        }
    }
}
