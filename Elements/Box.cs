using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    [Serializable]
    public class Box : NotifyBase
    {
        private int width;
        public int Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); } }

        private int height;
        public int Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); } }

        private int canvasLeft;
        public int CanvasLeft { get { return canvasLeft; } set { canvasLeft = value; NotifyPropertyChanged("CanvasLeft"); } }

        private int canvasTop;
        public int CanvasTop { get { return canvasTop; } set { canvasTop = value; NotifyPropertyChanged("CanvasTop"); } }

        private int name;
        public int Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }

        private double opacity;
        public double Opacity { get { return opacity; } set { opacity = value; NotifyPropertyChanged("Opacity"); } }

        public Box() {
            this.width = 100;
            this.height = 100;
            this.canvasLeft = 0;
            this.canvasTop = 0;
            this.opacity = 0.3;
            System.Console.WriteLine("Box Constructor");
        }

    }
}
