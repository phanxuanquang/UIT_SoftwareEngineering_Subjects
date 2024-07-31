using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System;
using Guna.UI2.WinForms;
using Guna.UI.WinForms;

namespace App.InstallUninstall
{
    class Download
    {
        private static HttpClient client;
        private List<Package> listSoftware;
        private string directoryFolderDownload;
        private const int step = 10;
        private bool statusDownload;
        private float percentDownload;
        private bool exception;
        public bool isCompleted
        {
            get
            {
                return statusDownload;
            }
        }
        public float GetPercentDownload
        {
            get
            {
                return percentDownload;
            }
        }
        public bool HasException
        {
            get
            {
                return exception;
            }
        }

        public Download()
        {
            client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
        }

        public void Start(List<Package> listSoftware, string directoryFolderDownload)
        {
            this.listSoftware = listSoftware;
            this.directoryFolderDownload = directoryFolderDownload;
            statusDownload = false;
        }

        public async void DownloadsNext(int index, List<ProgressWindow_Base.ActionProcess> blackList)
        {
            statusDownload = false;
            percentDownload = 0.0f;
            if (blackList != null && index > -1 && index < listSoftware.Count && blackList[index] == ProgressWindow_Base.ActionProcess.Canceled)
            {
                statusDownload = true;
                return;
            }
            exception = true;
            if (this.listSoftware != null && index > -1)
            {
                if (listSoftware.Count > index)
                {
                    string fileName = GetPath.GetFileName(listSoftware[index]);
                    string URL = GetPath.GetURL(listSoftware[index]);
                    string pathFile = Path.Combine(this.directoryFolderDownload, fileName);
                    try
                    {
                        using (HttpResponseMessage response = client.GetAsync(new Uri(URL), HttpCompletionOption.ResponseHeadersRead).Result)
                        {
                            HttpResponseMessage httpRequestMessage = response.EnsureSuccessStatusCode();
                            if (httpRequestMessage.IsSuccessStatusCode && long.TryParse(response.Content.Headers.SingleOrDefault(h => h.Key.Equals("Content-Length")).Value.First(), out long totalSize))
                            {
                                FileInfo checkExistsFile = new FileInfo(pathFile);
                                if (checkExistsFile.Exists)
                                {
                                    if (checkExistsFile.Length == totalSize)
                                    {
                                        statusDownload = true;
                                        percentDownload = 100.0f;
                                        exception = false;
                                        return;
                                    }
                                    else
                                    {
                                        File.Delete(pathFile);
                                    }
                                }
                                try
                                {
                                    using (Stream contentStream = await response.Content.ReadAsStreamAsync(), fileStream = new FileStream(pathFile, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                                    {
                                        var totalRead = 0L;
                                        var stepSegment = 0L;
                                        var buffer = new byte[8192];
                                        var isMoreToRead = true;
                                        do
                                        {
                                            var read = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                                            if (read == 0)
                                            {
                                                isMoreToRead = false;
                                            }
                                            else
                                            {
                                                await fileStream.WriteAsync(buffer, 0, read);

                                                totalRead += read;
                                                stepSegment += 1;

                                                percentDownload = totalRead * 100.0f / totalSize;
                                            }
                                            if (blackList != null && blackList[index] == ProgressWindow_Base.ActionProcess.Canceled) break;
                                        }
                                        while (isMoreToRead);
                                        exception = false;
                                    }
                                    if (blackList != null && blackList[index] == ProgressWindow_Base.ActionProcess.Canceled)
                                    {
                                        if (File.Exists(pathFile))
                                        {
                                            File.Delete(pathFile);
                                        }
                                    }
                                }
                                catch (IOException)
                                {
                                    MessageBox.Show("Không đủ dung lượng hệ thống.");
                                }
                                catch (UnauthorizedAccessException)
                                {
                                    MessageBox.Show("Không có quyền truy cập vào thư mục lưu trình cài đặt.");
                                }
                                statusDownload = true;
                            }
                        }
                    }
                    catch
                    {
                        statusDownload = true;
                    }
                }
            }
        }
    }
}