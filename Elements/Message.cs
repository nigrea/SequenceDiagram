using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public class Message : NotifyBase
    {

        private int position;
        public int Position { get { return position; } set { position = value; NotifyPropertyChanged("Position"); } }

        private Component endA;
        private Component endB;
        public Component EndA { get { return endA; } set { endA = value; NotifyPropertyChanged("EndA"); } }
        public Component EndB { get { return endB; } set { endB = value; NotifyPropertyChanged("EndB"); } }

    }
}
