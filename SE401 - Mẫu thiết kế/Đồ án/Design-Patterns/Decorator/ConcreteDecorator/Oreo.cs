using Decorator.BaseDecorator;
using Decorator.Component;
using System.Text;

namespace Decorator.ConcreteDecorator
{
    internal class Oreo : BaseTraSua
    {
        public Oreo(IMilkTea wrappee) : base(wrappee) { }

        public override StringBuilder Make()
        {
            StringBuilder sb = base.Make();
            sb.AppendLine("Oreo");
            return sb;
        }

        public override double Price()
        {
            return base.Price() + 1.5;
        }
    }
}
