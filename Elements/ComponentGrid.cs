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

        public ComponentGrid()
        {
            Components = new ObservableCollection<Component>();
            Messages = new ObservableCollection<Message>();

        }

        public void addComponent(Component component)
        {
            component.Position = Components.Count + 1; // Probably needs to be changed!!
            Components.Add(component);
            refresh();
        }

        public void removeComponent(Component component)
        {
            Components.Remove(component);
        }

        public void addMessage(Message message)
        {
            message.Position = Messages.Count + 1;
            Messages.Add(message);
            refresh();
        }

        public void removeMessage(Message message)
        {
            Messages.Remove(message);
        }

        public void refresh()
        {
            foreach (Component component in Components)
            {
                component.X = ScreenWidth / (Components.Count + 1) * component.Position;
                component.Width = (ScreenWidth / (Components.Count + 1)) - 20;
                component.Height = 100 + (Messages.Count * 100);
            }
            foreach (Message message in Messages) 
            {
                message.Y = 100 * message.Position;
            }
        }

        public void setNewPosition(Component movingComponent, double coordinate)
        {
            int newPosition = (Components.Count + 1) * (int)coordinate / ScreenWidth;
            if (newPosition == 0)
            {
                newPosition = 1;
            }
            if (newPosition > Components.Count)
            {
                newPosition = Components.Count;
            }
            if (newPosition < movingComponent.Position)
            {
                foreach (Component component in Components)
                {
                    if (component != movingComponent && component.Position >= newPosition && component.Position < movingComponent.Position)
                    {
                        component.Position++;
                    }
                }
            }
            else if (newPosition > movingComponent.Position)
            {
                foreach (Component component in Components)
                {
                    if (component != movingComponent && component.Position <= newPosition && component.Position > movingComponent.Position)
                    {
                        component.Position--;
                    }
                }
            }
            movingComponent.Position = newPosition;
            refresh();

        }

    }
}
