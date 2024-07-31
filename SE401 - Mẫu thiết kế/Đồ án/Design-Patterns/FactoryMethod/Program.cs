using FactoryMethod.ConcreteCreator;
using FactoryMethod.Creator;
using FactoryMethod.Product;
using System;

namespace FactoryMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string arg = "win";
            Dialog dialog = null;

            switch (arg)
            {
                case "mac":
                    dialog = new MacDialog();
                    break;
                case "win":
                    dialog = new WinDialog();
                    break;
            }

            if (dialog != null)
            {
                IButton button = dialog.CreateButton();
                Console.WriteLine(button.Render());
            }
        }
    }
}
