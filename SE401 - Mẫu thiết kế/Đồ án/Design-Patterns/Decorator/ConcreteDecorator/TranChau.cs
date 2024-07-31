using Decorator.BaseDecorator;
using Decorator.Component;
using System;
using System.Text;

namespace Decorator.ConcreteDecorator
{
    internal class TranChau : BaseTraSua
    {
        public TranChau(IMilkTea wrappee) : base(wrappee) { }

        public override StringBuilder Make()
        {
            StringBuilder sb = base.Make();
            sb.AppendLine("Tran chau");
            return sb;
        }

        public override double Price()
        {
            return base.Price() + 1.75;
        }
    }
}
