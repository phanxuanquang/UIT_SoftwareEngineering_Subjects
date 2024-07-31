using FactoryMethod.ConcreteProduct;
using FactoryMethod.Creator;
using FactoryMethod.Product;

namespace FactoryMethod.ConcreteCreator
{
    internal class WinDialog : Dialog
    {
        public override IButton CreateButton()
        {
            return new WinButton();
        }
    }
}
