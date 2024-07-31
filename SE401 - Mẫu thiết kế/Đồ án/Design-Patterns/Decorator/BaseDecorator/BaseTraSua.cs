using Decorator.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator.BaseDecorator
{
    internal class BaseTraSua : IMilkTea
    {
        private IMilkTea wrappee;

        public BaseTraSua(IMilkTea wrappee)
        {
            this.wrappee = wrappee;
        }

        public virtual double Price()
        {
            return wrappee.Price();
        }

        public virtual StringBuilder Make()
        {
            return wrappee.Make();
        }
    }
}
