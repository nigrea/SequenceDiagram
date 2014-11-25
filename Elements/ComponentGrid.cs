using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    [Serializable]
    public class ComponentGrid : NotifyBase
    {
        public ObservableCollection<Component> Components { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        private int x;
        private int y;

        private int ScreenWidth = 600; //CHANGE THIS, NOT DYNEMIC!! ONLY FOR TESTING!!!

        public ComponentGrid() { 
            Components = new ObservableCollection<Component>();
            Messages = new ObservableCollection<Message>();
        
        }

        public void addComponent(Component component) {
            component.Position = Components.Count+1; // Probably needs to be changed!!
            Components.Add(component);
            refresh();
        }

        public void removeComponent(Component component)
        {
            Components.Remove(component);
        }

        public void refresh() {
            foreach (Component component in Components) {
                component.X = ScreenWidth / (Components.Count + 1) * component.Position;
                component.Width = (ScreenWidth / (Components.Count + 1)) - 20 ;
            }
        }

    }
}
