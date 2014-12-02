using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    [Serializable]
    public class Message : NotifyBase
    {

        private int position;
        public int Position { get { return position; } set { position = value; NotifyPropertyChanged("Position"); } }

        private int y;
        public int Y { get { return y; } set { y = value; NotifyPropertyChanged("Y"); } }

        public int YTop { get { return -20; } }
        public int YBot { get { return 20; } }
        public int XLeft { get { if (start.Position < end.Position) { return Width - 20; } else { return 20; }; } }

        public int CanvasLeft { get{ if (start.Position < end.Position) { return start.CanvasCenterX; } else { return end.CanvasCenterX; }} }
        public int CanvasRight { get { if (start.Position < end.Position) { return end.CanvasCenterX; } else { return start.CanvasCenterX; } } }

        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }

        public int Width { get { return CanvasRight - CanvasLeft; } set { NotifyPropertyChanged("Width"); } }

        public int ArrowPosition { get { if (start.Position < end.Position) { return Width; } else { return 0; } } }

        private Component start;
        private Component end;
        public Component Start { get { return start; } set { start = value; NotifyPropertyChanged("EndA"); } }
        public Component End { get { return end; } set { end = value; NotifyPropertyChanged("EndB"); } }


        public Message(Component start, Component end){
            this.name = "Function Name";
            this.start = start;
            this.end = end;
        }

        public void refresh() {
            NotifyPropertyChanged("CanvasLeft");
            NotifyPropertyChanged("CanvasRight");
            NotifyPropertyChanged("Width");
            NotifyPropertyChanged("XLeft");
            NotifyPropertyChanged("YTop");
            NotifyPropertyChanged("YBot");
            NotifyPropertyChanged("ArrowPosition"); 
        }

    }
}
