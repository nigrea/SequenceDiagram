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

        private Component start;
        private Component end;
        public Component Start { get { return start; } set { start = value; NotifyPropertyChanged("EndA"); } }
        public Component End { get { return end; } set { end = value; NotifyPropertyChanged("EndB"); } }

        public Message(Component start, Component end){
            this.start = start;
            this.end = end;
        }

    }
}
