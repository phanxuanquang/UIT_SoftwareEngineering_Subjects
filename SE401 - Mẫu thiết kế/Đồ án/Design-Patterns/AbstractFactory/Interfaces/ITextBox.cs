using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Interfaces
{
    internal interface ITextBox
    {
        string Value { get; set; }
        string Render();
        string OnTextChange(string text);
    }
}
