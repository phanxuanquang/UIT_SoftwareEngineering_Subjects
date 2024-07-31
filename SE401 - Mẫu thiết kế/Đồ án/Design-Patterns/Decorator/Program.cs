using Decorator.BaseDecorator;
using Decorator.Component;
using Decorator.ConcreteComponent;
using Decorator.ConcreteDecorator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Decorator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IDictionary<int, Type> topping = new Dictionary<int, Type>
            {
                {1, typeof(BaseTraSua)},
                {2, typeof(DuongDen)},
                {3, typeof(DuongTrang)},
                {4, typeof(KemCheese)},
                {5, typeof(Matcha)},
                {6, typeof(Oreo)},
                {7, typeof(TranChau)}
            };

            IMilkTea cupMilkTea = new TraSua();
            IList<Type> selectedTopping = new List<Type>();
            string menu = string.Join("\n", topping.Values.Select((item, index) => $"{index + 1}. {item.Name}")) + "\nHoan tat chon 0";

            Console.WriteLine("Vui long chon topping cho li tra sua");

            bool flag = true;
            do
            {
                Console.WriteLine(menu);
                Console.Write("Chon topping: ");
                if (int.TryParse(Console.ReadLine(), out int choose))
                {
                    Console.Clear();
                    if (choose == 0)
                        flag = false;
                    else if (choose < 8 && choose > 0)
                    {
                        selectedTopping.Add(topping[choose]);
                        Console.WriteLine($"Ban da chon {selectedTopping.Last().Name}");
                    }
                }
            }
            while (flag);

            foreach (Type decorator in selectedTopping)
                cupMilkTea = Activator.CreateInstance(decorator, cupMilkTea) as IMilkTea;

            Console.WriteLine(cupMilkTea.Make().ToString());
            Console.WriteLine($"Gia: {cupMilkTea.Price()}");
        }
    }
}
