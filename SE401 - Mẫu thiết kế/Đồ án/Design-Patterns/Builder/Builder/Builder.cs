using Builder.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder.Builder
{
    internal interface Builder
    {
        Builder ResetBuilder();
        Builder BuildFloor();
        Builder BuildDoor();
        Builder BuildWall();
        Builder BuildWindows();
        Builder BuildRoof();
        Builder BuildGarage();
        Builder BuildSwimPool();
        Builder BuildGarden();
    }
}
