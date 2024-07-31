using Builder.ConcreteBuilder;
using Builder.Director;
using Builder.Product;
using System;
using System.Text;

namespace Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HouseBuilder builder = new HouseBuilder();
            NormalDirector director = new NormalDirector(builder);

            director.NormalHouse();
            House normalHouse= builder.GetProduct();
            Console.WriteLine("Normal house:\n" + string.Join("\n-->", normalHouse.step));

            director.Penhouse();
            House penhouse = builder.GetProduct();
            Console.WriteLine("Penhouse:\n" + string.Join("\n-->", penhouse.step));
        }
    }
}
