using Builder.Builder;
using Builder.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.ConcreteBuilder
{
    internal class HouseBuilder : Builder.Builder
    {
        private House product;

        public HouseBuilder()
        {
            ResetBuilder();
        }

        public Builder.Builder ResetBuilder()
        {
            product = new House();
            return this;
        }

        public Builder.Builder BuildFloor()
        {
            product.step.Add("Building floor....");
            return this;
        }

        public Builder.Builder BuildDoor()
        {
            product.step.Add("Building door....");
            return this;
        }

        public Builder.Builder BuildGarage()
        {
            product.step.Add("Building garade....");
            return this;
        }

        public Builder.Builder BuildGarden()
        {
            product.step.Add("Building garden....");
            return this;
        }

        public Builder.Builder BuildRoof()
        {
            product.step.Add("Building roof....");
            return this;
        }

        public Builder.Builder BuildSwimPool()
        {
            product.step.Add("Building swim pool....");
            return this;
        }

        public Builder.Builder BuildWall()
        {
            product.step.Add("Building wall....");
            return this;
        }

        public Builder.Builder BuildWindows()
        {
            product.step.Add("Building windows....");
            return this;
        }

        public House GetProduct()
        {
            this.product.step.Add("Completed");
            House product = this.product;
            ResetBuilder();
            return product;
        }
    }
}
