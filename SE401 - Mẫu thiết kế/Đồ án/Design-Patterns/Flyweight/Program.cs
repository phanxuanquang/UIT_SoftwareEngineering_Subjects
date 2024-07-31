using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Flyweight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*FlyweightFactory factory = new FlyweightFactory();*/
            string[] cars = new[] { "Ford", "Toyota", "Mazda" };

            Random rnd = new Random();
            for(int index = 0; index < 10000; index++)
            {
                /*factory.GetCar(cars[rnd.Next(0, 3)]).ShowInfo("", 4.5f, "");*/
                switch (cars[rnd.Next(0, 3)])
                {
                    case "Ford":
                        new Ford(100, 100, 100).ShowInfo("", 4.5f, "");
                        break;
                    case "Toyota":
                        new Toyota(100, 100, 100).ShowInfo("", 4.5f, "");
                        break; ;
                    case "Mazda":
                        new Mazda(100, 100, 100).ShowInfo("", 4.5f, "");
                        break;
                    default:
                        throw new ArgumentException("Not found");
                }
            }

            // get total memory comsumption
            Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            long totalBytesOfMemoryUsed = currentProcess.WorkingSet64;
        }

        public class FlyweightFactory
        {
            private IDictionary<String, ICar> cars = new Dictionary<String, ICar>();
            /*private Random rnd = new Random();*/

            public ICar GetCar(string name)
            {
                if(!cars.ContainsKey(name))
                {
                    switch (name)
                    {
                        case "Ford":
                            cars.Add(name, new Ford(100, 100, 100));
                            break;
                        case "Toyota":
                            cars.Add(name, new Toyota(100, 100, 100));
                            break; ;
                        case "Mazda":
                            cars.Add(name, new Mazda(100, 100, 100));
                            break;
                        default:
                            throw new ArgumentException("Not found");
                    }
                }
                return cars[name];
            }
        }

        public interface ICar
        {
            void ShowInfo(string idNumber, float fuel, string color);
        }

        public class Ford : ICar
        {
            private readonly float x;
            private readonly float y;
            private readonly float z;
            private string line = "Ford everest";

            public Ford(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            public void ShowInfo(string idNumber, float fuel, string color)
            {
                Console.WriteLine($"{line}, ID: {idNumber}, FuelLevel: {fuel}, Color: {color}, Size: {x}.{y}.{z}");
            }
        }

        public class Toyota : ICar
        {
            private readonly float x;
            private readonly float y;
            private readonly float z;
            private string line = "Toyota XT";
            public Toyota(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public void ShowInfo(string idNumber, float fuel, string color)
            {
                Console.WriteLine($"{line}, ID: {idNumber}, FuelLevel: {fuel}, Color: {color}, Size: {x}.{y}.{z}");
            }
        }

        public class Mazda : ICar
        {
            private readonly float x;
            private readonly float y;
            private readonly float z;
            private string line = "Mazda CX";
            public Mazda(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public void ShowInfo(string idNumber, float fuel, string color)
            {
                Console.WriteLine($"{line}, ID: {idNumber}, FuelLevel: {fuel}, Color: {color}, Size: {x}.{y}.{z}");
            }
        }
    }
}
