using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal interface IFileExplorerBuilder
    {
        IFileExplorerBuilder Add(IFileExplorer item);
        IFileExplorerBuilder AddFile(string nameFile);
        IFileExplorerBuilder AddDirectory(string nameDirectory);
        IFileExplorer Build();
    }
}
