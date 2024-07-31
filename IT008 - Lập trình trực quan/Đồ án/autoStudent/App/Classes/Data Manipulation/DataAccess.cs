using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    class DataAccess
    {
        static readonly string filePath;
        static Root root;

        //private constructor to avoid client applications to use constructor
        static DataAccess()
        {
            filePath = Application.StartupPath + @"/Data/MainData.autostudent";
            Load();
        }

        private static DataAccess instance;

        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccess();
                }
                return instance;
            }
        }
        private static void Load()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }
                if (!File.Exists(filePath))
                {
                    root = new Root();
                    using (var stream = File.Create(filePath))
                    {
                        string path = @"https://dung-ovl.github.io/MainData.json";
                        using (WebClient client = new WebClient())
                        {
                            Stream streamR = client.OpenRead(path);
                            string content;
                            string passDown = DataAccess.Instance.GetPassCry();
                            using (StreamReader reader = new StreamReader(streamR))
                            {
                                content = reader.ReadToEnd();
                            }
                            string encrypt = Cryptography.Encrypt(content, passDown);
                            using (StreamWriter writer = new StreamWriter(stream))
                            {
                                writer.Write(encrypt);
                            }
                        }
                    }
                }

                using (var stream = File.OpenRead(filePath))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string jsonString = reader.ReadToEnd();
                        string data = Cryptography.Decrypt(jsonString, DataAccess.Instance.GetPassCry());
                        root = Root.FromJson(data);
                    }
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(String.Format("Lỗi truy xuất. Mã lỗi: {0}.", e.ToString()));
            }
        }

        public List<Package> GetPackages()
        {
            return root.Packages;
        }

        public List<Package> GetPackagesOfKind(Role role)
        {
            var list = new List<Package>();
            foreach (var item in root.Packages)
            {
                if (item.Role == role)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public List<Package> GetX86()
        {
            List<Package> packages = new List<Package>();

            foreach (var item in root.Packages)
            {
                if (item.Installer.X8664 == null && item.Installer.X86 != null)
                    packages.Add(item);
            }
            return packages;
        }
        public List<Package> GetX64()
        {
            List<Package> packages = new List<Package>(root.Packages);
            return packages;
        }

        public List<Package> GetPackagesOfName(List<string> names)
        {
            return root.Packages.Where(item => names.Any(name => item.Name == name) == true).ToList();
        }

        public string GetFilePath()
        {
            return filePath;
        }

        public DateTime GetUpdateTime()
        {
            return root.UpdateDate;
        }

        public string GetPassCry()
        {
            return "0d481fbe6c3b5349e758fb63a49752c72b4d701de3d2e81ede1713f6471b658e";
        }

        public void LoadDirect()
        {
            Load();
        }
    }
}
