using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace App.InstallUninstall
{
    class Install : BaseProcess
    {
        private string directoryFolderDownload;

        public void Start(List<Package> listSoftware, string directoryFolderDownload)
        {
            this.directoryFolderDownload = directoryFolderDownload;
            base.Start(listSoftware);
        }

        public override void RunProcess(int index)
        {
            base.statusProcess = false;
            if (listSoftware != null && index > -1)
            {
                if (listSoftware.Count > index)
                {
                    if (CreateProcess(GetPath.CommandInstall(directoryFolderDownload, listSoftware[index])))
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
