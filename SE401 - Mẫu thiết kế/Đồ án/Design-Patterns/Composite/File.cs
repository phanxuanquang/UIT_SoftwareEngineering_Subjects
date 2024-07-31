using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class File : IFileExplorer
    {
        private string nameFile;
        public string Name { get => nameFile; }

        public File(string nameFile)
        {
            this.nameFile = nameFile;
        }

        public string Path(string _)
        {
            return nameFile;
        }
    }
}
