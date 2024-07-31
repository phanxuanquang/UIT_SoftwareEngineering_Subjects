using AbstractFactory.Interfaces;
using AbstractFactory.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Factories
{
    internal class WinFormSubmit : IAbstractFactoryFormSubmit
    {
        public IButton CreateButton() => new WinButton();

        public ITextBox CreateTextBox() => new WinTextBox();
    }
}
