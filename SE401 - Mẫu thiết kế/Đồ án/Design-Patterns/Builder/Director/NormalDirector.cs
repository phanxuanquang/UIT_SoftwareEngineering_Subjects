using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Builder.Builder;

namespace Builder.Director
{
    internal class NormalDirector
    {
        private Builder.Builder builder;

        public NormalDirector(Builder.Builder builder)
        {
            this.builder = builder;
        }

        public void NormalHouse()
        {
            builder
                .ResetBuilder()
                .BuildFloor()
                .BuildWall()
                .BuildDoor()
                .BuildWindows()
                .BuildRoof();
        }

        public void Penhouse()
        {
            builder
                .ResetBuilder()
                .BuildFloor()
                .BuildWall()
                .BuildDoor()
                .BuildWindows()
                .BuildRoof()
                .BuildSwimPool()
                .BuildGarden();
        }
    }
}
