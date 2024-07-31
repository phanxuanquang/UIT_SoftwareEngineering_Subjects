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
    class Uninstall : BaseProcess
    {
        public override void RunProcess(int index)
        {
            base.statusProcess = false;
            if (listSoftware != null && index > -1)
            {
                if (listSoftware.Count > index)
                {
                    if (CreateProcess(GetPath.CommandUninstall(listSoftware[index])))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            while (!tracking.isCompleted)
                            {
                                Thread.Sleep(250);
                            }
                            base.statusProcess = true;
                        });
                    }
                    else
                    {
                        base.statusProcess = true;
                    }
                }
            }
        }
    }
}
