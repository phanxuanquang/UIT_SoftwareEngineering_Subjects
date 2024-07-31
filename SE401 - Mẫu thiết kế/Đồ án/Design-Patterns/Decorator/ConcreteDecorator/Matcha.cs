using Decorator.Component;
using Decorator.BaseDecorator;
using System.Text;

namespace Decorator.ConcreteDecorator
{
    internal class Matcha : BaseTraSua
    {
        public Matcha(IMilkTea wrappee) : base(wrappee) { }

        public override StringBuilder Make()
        {
            StringBuilder sb = base.Make();
            sb.AppendLine("Matcha");
            return sb;
        }

        public override double Price()
        {
            return base.Price() + 0.75;
        }
    }
}
