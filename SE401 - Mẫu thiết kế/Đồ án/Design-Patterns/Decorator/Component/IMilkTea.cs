using System.Text;

namespace Decorator.Component
{
    internal interface IMilkTea
    {
        StringBuilder Make();
        double Price();
    }
}
