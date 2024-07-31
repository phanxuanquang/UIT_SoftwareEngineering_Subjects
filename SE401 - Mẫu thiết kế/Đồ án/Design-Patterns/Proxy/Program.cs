using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Proxy
{
    internal class Program
    {
        public interface IGraphic
        {
            void Display();
        }

        class Image : IGraphic
        {

            private string _fileName;
            public Image(string fileName)
            {
                _fileName = fileName;
                LoadFromDisk(_fileName);
            }

            public void Display()
            {
                Console.WriteLine("Displaying " + _fileName);
            }

            private void LoadFromDisk(string fileName)
            {
                Console.WriteLine("Loading " + fileName);
            }
        }

        public class ProxyImage : IGraphic
        {
            private Image image;
            private string fileName;

            public ProxyImage(string fileName)
            {
                this.fileName = fileName;
            }
            public void Display()
            {
                if (image == null) // lazy loading
                {
                    image = new Image(fileName);
                }
                image.Display();
            }
        }

        class TextDocument
        {
            public void Insert(IGraphic graphic)
            {
                graphic.Display();
            }

        }

        static void Main(string[] args)
        {
            TextDocument textDocument = new TextDocument();
            ProxyImage proxy = new ProxyImage("Proxy.png");
            textDocument.Insert(proxy);
            Console.WriteLine("After loading first time");
            textDocument.Insert(proxy);
        }
    }
}
