using Decorator.Component;
using Decorator.BaseDecorator;
using System.Text;

namespace Decorator.ConcreteDecorator
{
    internal class KemCheese : BaseTraSua
    {
        public KemCheese(IMilkTea wrappee) : base(wrappee) { }

        public override StringBuilder Make()
        {
            StringBuilder sb = base.Make();
            sb.AppendLine("Kem cheese");
            return sb;
        }

        public override double Price()
        {
            return base.Price() + 1.25;
        }
    }
}
