using AbstractFactory.Factories;
using AbstractFactory.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string arg = "mac";
            IAbstractFactoryFormSubmit formSubmit = null;

            switch (arg)
            {
                case "mac":
                    formSubmit = new MacFormSubmit();
                    break;
                case "win":
                    formSubmit = new WinFormSubmit();
                    break;
            }

            if (formSubmit != null)
            {
                IButton button = formSubmit.CreateButton();
                ITextBox textbox = formSubmit.CreateTextBox();

                Console.WriteLine(button.Render());
                Console.WriteLine(textbox.Render());
                Console.WriteLine(textbox.OnTextChange("Yoh"));
                Console.WriteLine(button.OnClick("Submit"));
            }
        }
    }
}
