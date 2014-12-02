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

        public int YTop { get { return y - 20; } set { YTop = value; NotifyPropertyChanged("YTop"); } }
        public int YBot { get { return y + 20; } set { YBot = value; NotifyPropertyChanged("YBot"); } }
        public int XLeft { get { if (start.Position < end.Position) { return end.CanvasCenterX - 20; } else { return end.CanvasCenterX + 20; }; } set { if (start.Position < end.Position) { end.CanvasCenterX = value + 20; } else { end.CanvasCenterX = value - 20;  }; NotifyPropertyChanged("XLeft"); } }

        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }


        private Component start;
        private Component end;
        public Component Start { get { return start; } set { start = value; NotifyPropertyChanged("EndA"); } }
        public Component End { get { return end; } set { end = value; NotifyPropertyChanged("EndB"); } }

        public Message(Component start, Component end){
            this.name = "Function Name";
            this.start = start;
            this.end = end;
        }

    }
}
