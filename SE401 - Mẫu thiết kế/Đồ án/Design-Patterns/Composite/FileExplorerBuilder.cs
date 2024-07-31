using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class FileExplorerBuilder : IFileExplorerBuilder
    {
        private Directory product;

        public FileExplorerBuilder(string root)
        {
            ResetBuilder(root);
        }

        public void ResetBuilder(string root)
        {
            product = new Directory(root);
        }

        public IFileExplorerBuilder Add(IFileExplorer item)
        {
            if (item is File file)
                return AddFile(file);
            if (item is Directory directory)
                if (product.Name == directory.Name)
                {
                    foreach (IFileExplorer subDirectory in directory.GetChild)
                        Add(subDirectory);
                    return this;
                }
                else return AddDirectory(directory);
            product.Add(item);
            return this;
        }

        public IFileExplorerBuilder AddDirectory(string nameDirectory)
        {
            return AddDirectory(new Directory(nameDirectory));
        }

        public IFileExplorerBuilder AddDirectory(Directory directory)
        {
            Directory exist = product.GetChild.Where(item => item is Directory).FirstOrDefault(item => item.Name.Equals(directory.Name)) as Directory;
            if (exist == null)
                product.Add(directory);
            else
                foreach (IFileExplorer subDirectory in directory.GetChild)
                    exist.Add(subDirectory);
            return this;
        }

        public IFileExplorerBuilder AddFile(string nameFile)
        {
            return AddFile(new File(nameFile));
        }

        public IFileExplorerBuilder AddFile(File file)
        {
            if (product.GetChild.Where(item => item is File).FirstOrDefault(item => item.Name.Equals(file.Name)) == null)
                product.Add(file);
            return this;
        }

        public IFileExplorer Build()
        {
            return product;
        }
    }
}
