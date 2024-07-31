using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BMPImage img1 = new BMPImage();
            WinImp win = new WinImp();
            img1.SetImageImp(win);
            img1.Method("PAINTING--->");

            PNGImage img2 = new PNGImage();
            LinuxImp linux = new LinuxImp();
            img2.SetImageImp(linux);
            img2.Method("PAINTING--->");
        }
        //Abstraction 
        abstract class Image 
        {
            //thuộc tính là 1 thể hiện của interface ImageImp
            protected ImageImp ImageToUse;
            public void SetImageImp(ImageImp ip)
            {
                ImageToUse = ip;
            }
            public virtual void Method(string s1)
            { }
        }
        //Refined Abstraction
        class BMPImage : Image
        {
            public override void Method(string s1)
            {
                string s2 = s1 + "BMP Image.";
                ImageToUse.Display(s2);
            }
        }
        class PNGImage : Image
        {
            public override void Method(string s1)
            {
                string s2 = s1 + "PNG Image.";
                ImageToUse.Display(s2);
            }
        }
        //Implementation
        interface ImageImp
        {
            void Display(string str);
        }
        //Concrete Implementation
        class WinImp : ImageImp
        {
            public void Display(string str)
            {
                Console.WriteLine(str + " Win OS.");
            }
        }
        class LinuxImp : ImageImp
        {
            public void Display(string str)
            {
                Console.WriteLine(str + " Linux OS.");
            }
        }
    }
}
