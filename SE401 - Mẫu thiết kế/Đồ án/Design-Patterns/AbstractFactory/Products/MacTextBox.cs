using AbstractFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Products
{
    internal class MacTextBox : ITextBox
    {
        public string Value { get; set; }

        public string OnTextChange(string text)
        {
            return $"Text changed: {Value} ---> {Value = text}";
        }

        public string Render()
        {
            return "Rendering Mac textbox";
        }
    }
}
