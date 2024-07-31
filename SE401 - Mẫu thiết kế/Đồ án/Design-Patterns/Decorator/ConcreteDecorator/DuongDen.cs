using Decorator.Component;
using Decorator.BaseDecorator;
using System.Text;

namespace Decorator.ConcreteDecorator
{
    internal class DuongDen : BaseTraSua
    {
        public DuongDen(IMilkTea wrappee) : base(wrappee) { }

        public override StringBuilder Make()
        {
            StringBuilder sb = base.Make();
            sb.AppendLine("Duong den");
            return sb;
        }

        public override double Price()
        {
            return base.Price() + 0.5;
        }
    }
}
