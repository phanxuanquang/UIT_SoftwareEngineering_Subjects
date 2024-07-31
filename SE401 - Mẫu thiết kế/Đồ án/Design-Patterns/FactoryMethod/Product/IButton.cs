using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.Product
{
    internal interface IButton
    {
        string Render();
        string OnClick(string text);
    }
}
