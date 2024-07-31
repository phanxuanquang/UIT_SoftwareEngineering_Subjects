using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;
using FactoryMethod.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod.ConcreteCreator
{
    internal class MacDialog : Dialog
    {
        public override IButton CreateButton()
        {
            return new MacButton();
        }
    }
}
