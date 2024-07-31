using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class Directory : IFileExplorer
    {
        private string nameDirectory;
        public string Name { get => nameDirectory; }
        private IList<IFileExplorer> subDirectory;

        public Directory(string name)
        {
            nameDirectory = name;
            subDirectory = new List<IFileExplorer>();
        }

        public void Add(IFileExplorer file)
        {
            subDirectory.Add(file);
        }

        public void Remove(IFileExplorer file)
        {
            subDirectory.Remove(file);
        }

        public IList<IFileExplorer> GetChild => subDirectory;

        public string Path(string parent = null)
        {
            if (parent == null)
                parent = Name;
            else parent += $"{Name}/";
            return parent + string.Join("\n", subDirectory.Select(item => item.Path(parent)));
        }
    }
}
