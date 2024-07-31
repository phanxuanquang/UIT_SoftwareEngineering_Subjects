using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.InstallUninstall
{
    class GetPath
    {
        public class NewProcess
        {
            public string FileName;
            public string Arguments;
        }

        public static string GetFileName(Package item)
        {
            string fileName = GetURL(item);

            if (String.IsNullOrEmpty(Path.GetExtension(fileName)))
            {
                switch (item.Installer.Kind)
                {
                    case Kind.Msi:
                        fileName = item.Name + ".msi";
                        break;
                    default:
                        fileName = item.Name + ".exe";
                        break;
                }
            }
            else fileName = Path.GetFileName(fileName);

            return fileName;
        }

        public static string GetURL(Package item)
        {
            string url = "";
            if (Environment.Is64BitOperatingSystem)
            {
                if (String.IsNullOrEmpty(item.Installer.X8664))
                {
                    url = item.Installer.X86;
                }
                else url = item.Installer.X8664;
            }
            else url = item.Installer.X86;

            if (url.Contains("{{.version}}"))
            {
                url = url.Replace("{{.version}}", item.Version);
            }
            return url;
        }

        public static NewProcess CommandInstall(string directoryFolderDownload, Package package)
        {
            if (package == null || String.IsNullOrEmpty(directoryFolderDownload)) return null;
            string pathFile = Path.Combine(directoryFolderDownload, GetPath.GetFileName(package));
            if (!File.Exists(pathFile)) return null;
            NewProcess newProcess = new NewProcess();
            if (newProcess != null)
            {
                switch (package.Installer.Kind)
                {
                    case Kind.Advancedinstaller:
                        newProcess.FileName = pathFile;
                        newProcess.Arguments = @"/i /q";
                        break;
                    case Kind.Appx:
                        newProcess.FileName = "powershell.exe";
                        newProcess.Arguments = String.Format("-command Add-AppxPackage -Path \"{0}\"", pathFile);
                        break;
                    case Kind.AsIs:
                        newProcess.FileName = pathFile;
                        newProcess.Arguments = @"/i /q";
                        break;
                    case Kind.Innosetup:
                        newProcess.FileName = pathFile;
                        newProcess.Arguments = @"/norestart /sp- /verysilent /allusers";
                        break;
                    case Kind.Msi:
                        newProcess.FileName = "msiexec.exe";
                        newProcess.Arguments = String.Format("/qn /i \"{0}\" ALLUSERS=1 REBOOT=ReallySuppress", pathFile);
                        break;
                    case Kind.Nsis:
                        newProcess.FileName = pathFile;
                        newProcess.Arguments = @"/S /NCRC";
                        break;
                    case Kind.Squirrel:
                        newProcess.FileName = pathFile;
                        newProcess.Arguments = @"--silent";
                        break;
                    case Kind.Custom:
                        if (package.Installer.Options != null && package.Installer.Options.Arguments != null)
                        {
                            for (int index = 0; index < package.Installer.Options.Arguments.Count; index++)
                            {
                                if (package.Installer.Options.Arguments[index].Contains("{{.installer}}"))
                                {
                                    newProcess.FileName = pathFile;
                                }
                                else
                                {
                                    newProcess.Arguments += package.Installer.Options.Arguments[index] + " ";
                                }
                            }
                        }
                        else newProcess = null;
                        break;
                    default:
                        newProcess = null;
                        break;
                }
            }
            return newProcess;
        }

        public static NewProcess CommandUninstall(Package package)
        {
            if (package == null || String.IsNullOrEmpty(package.UninstallString)) return null;
            string pathUninstaller = UninstallString2PathFile(package.UninstallString, package.Installer.Kind);
            NewProcess newProcess = new NewProcess();
            if (newProcess != null)
            {
                switch (package.Installer.Kind)
                {
                    case Kind.Advancedinstaller:
                        newProcess.FileName = pathUninstaller;
                        newProcess.Arguments = @"/i /q";
                        break;
                    case Kind.Appx:
                        newProcess.FileName = "powershell.exe";
                        newProcess.Arguments = String.Format("-command Remove-AppxPackage -Package {0}", pathUninstaller);
                        break;
                    case Kind.AsIs:
                        newProcess.FileName = pathUninstaller;
                        newProcess.Arguments = @"/i /q";
                        break;
                    case Kind.Innosetup:
                        newProcess.FileName = pathUninstaller;
                        newProcess.Arguments = @"/norestart /verysilent /suppressmsgboxes /sp- /allusers";
                        break;
                    case Kind.Msi:
                        newProcess.FileName = "msiexec.exe";
                        newProcess.Arguments = String.Format("/x {0} /qn ALLUSERS=1 REBOOT=ReallySuppress", pathUninstaller);
                        break;
                    case Kind.Nsis:
                        newProcess.FileName = pathUninstaller;
                        newProcess.Arguments = @"/S /NCRC /ALLUSERS";
                        break;
                    case Kind.Squirrel:
                        newProcess.FileName = pathUninstaller;
                        newProcess.Arguments = @"--uninstall --silent";
                        break;
                    case Kind.Custom:
                        if (package.UninstallArgument != null)
                        {
                            for (int index = 0; index < package.UninstallArgument.Count; index++)
                            {
                                if (package.UninstallArgument[index].Contains("{{.uninstaller}}"))
                                {
                                    newProcess.FileName = pathUninstaller;
                                }
                                else
                                {
                                    newProcess.Arguments += package.UninstallArgument[index] + " ";
                                }
                            }
                        }
                        else newProcess = null;
                        break;
                    default:
                        newProcess = null;
                        break;
                }
            }
            return newProcess;
        }

        private static string UninstallString2PathFile(string uninstallString, Kind kind)
        {
            Match regex = null;
            if (kind != Kind.Custom)
            {
                regex = Regex.Match(uninstallString, @"\{.*?\}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                if (regex.Success) return regex.Value;
            }
            regex = Regex.Match(uninstallString, "\\\"(.*?)\\\"", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (regex.Success) return regex.Value;
            return uninstallString.Split(' ')[0];

        }
    }
}
