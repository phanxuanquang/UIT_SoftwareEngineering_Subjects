using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal interface IFileExplorer
    {
        string Name { get; }
        string Path(string parent = null);
    }
}
