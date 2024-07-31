using Decorator.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator.ConcreteComponent
{
    internal class TraSua : IMilkTea
    {
        public StringBuilder Make()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Tra sua");
            return sb;
        }

        public double Price()
        {
            return 5.0;
        }
    }
}
