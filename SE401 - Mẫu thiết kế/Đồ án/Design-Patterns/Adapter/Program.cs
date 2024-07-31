using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MallarDuck duck = new MallarDuck();

            WildTurkey turkey = new WildTurkey();
            TurkeyAdapter turkeyAdapter = new TurkeyAdapter(turkey);

            Console.WriteLine("The Turkey says....");
            turkey.Gobble();
            turkey.Fly();

            Console.WriteLine("The Duck says....");
            TestDuck(duck);

            Console.WriteLine("The TurkeyAdapter says....");
            TestDuck(turkeyAdapter);
        }

        static public void TestDuck(IDuck duck)
        {
            duck.Quack();
            duck.Fly();
        }
    }

    internal class TurkeyAdapter : IDuck
    {
        WildTurkey turkey;

        public TurkeyAdapter(WildTurkey turkey)
        {
            this.turkey = turkey;
        }

        public void Quack()
        {
            turkey.Gobble();
        }

        public void Fly()
        {
            for (int i = 0; i < 5; i++)
                turkey.Fly();
        }
    }

    internal class MallarDuck : IDuck
    {
        public void Quack()
        {
            Console.WriteLine("Quack!");
        }

        public void Fly()
        {
            Console.WriteLine("I'm flying!");
        }
    }

    internal class WildTurkey : ITurkey
    {
        public void Gobble()
        {
            Console.WriteLine("Gobble Gobble!");
        }

        public void Fly()
        {
            Console.WriteLine("I'm flying a short distance!");
        }
    }

    internal interface IDuck
    {
        void Quack();
        void Fly();
    }

    internal interface ITurkey
    {
        void Gobble();
        void Fly();
    }
}

