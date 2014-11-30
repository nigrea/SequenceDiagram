using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;


namespace Elements
{
    [Serializable]
    public class Component : NotifyBase
    {
        private static int counter = 0;
        private string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }
        private int position;
        public int Position { get { return position; } set { position = value; NotifyPropertyChanged("Position"); } }
        private int x;
        public int X { get { return x; } set { x = value; NotifyPropertyChanged("X"); NotifyPropertyChanged("CanvasCenterX"); } }
        private int y;
        public int Y { get { return y; } set { y = value; NotifyPropertyChanged("Y"); NotifyPropertyChanged("CanvasCenterY"); } }
        private int width;
        public int Width { get { return width; } set { width = value; NotifyPropertyChanged("Width"); NotifyPropertyChanged("CenterX"); NotifyPropertyChanged("CanvasCenterX"); } }
        private int height;
        public int Height { get { return height; } set { height = value; NotifyPropertyChanged("Height"); NotifyPropertyChanged("CenterY"); NotifyPropertyChanged("CanvasCenterY"); } }

        public int CanvasCenterX { get { return X + Width / 2; } set { X = value - Width / 2; NotifyPropertyChanged("X"); } }
        public int CanvasCenterY { get { return Y + Height / 2; } set { Y = value - Height / 2; NotifyPropertyChanged("Y"); } }
        public int CenterX { get { return Width / 2; } }
        public int CenterY { get { return Height / 2; } }

        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages{get { return messages; } set { messages = value; NotifyPropertyChanged("Messages"); }}


        private bool isSelected;
        public bool IsSelected { get { return isSelected; } set { isSelected = value; NotifyPropertyChanged("IsSelected"); NotifyPropertyChanged("SelectedColor"); } }


        public Component() {

            Position = ++counter;
            Height = 300;

        }


    }
}
