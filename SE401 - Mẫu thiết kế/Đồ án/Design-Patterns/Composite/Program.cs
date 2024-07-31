using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*string[] x = new[]
            {
                "data/oop/bt1.txt",
                "data/csdl/slide-2.pdf",
                "data/note.txt",
                "data/oop/th/phanso.cpp",
                "video/phim/007.mp4",
                "video/link.txt"
            };*/
            IFileExplorerBuilder builder = new FileExplorerBuilder("E:/");
            builder
                .AddFile("hello.txt")
                .AddDirectory("data")
                .Add(new FileExplorerBuilder("data")
                    .AddFile("data.txt")
                    .Add(new FileExplorerBuilder("yoh")
                        .AddFile("nested.txt")
                        .AddDirectory("nested")
                    .Build())
                .Build())
                .AddDirectory("New folder");
            IFileExplorer explorer = builder.Build();
            Console.WriteLine(explorer.Path());
        }
    }
}
