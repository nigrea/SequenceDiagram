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
        public ObservableCollection<Box> Boxes { get; set; }
        private int x;
        private int y;
        


        private int ScreenWidth = 600; //CHANGE THIS, NOT DYNEMIC!! ONLY FOR TESTING!!!

        public ComponentGrid()
        {
            Components = new ObservableCollection<Component>();
            Messages = new ObservableCollection<Message>();
            Boxes = new ObservableCollection<Box>();

        }

        public void addBox(Box box) {
            Boxes.Add(box);
        }

        public void removeBox(Box box)
        {
            Boxes.Remove(box);
        }

        public void addComponent(Component component)
        {
            component.Position = Components.Count + 1; // Probably needs to be changed!!
            Components.Add(component);
            refresh();
        }

        public void removeComponent(Component toRemove)
        {
            Components.Remove(toRemove);
            foreach (Component component in Components)
            {
                if (component.Position > toRemove.Position)
                {
                    component.Position--;
                }
            }
            refresh();
        }

        public void addMessage(Message newMessage, int position)
        {
            Messages.Add(newMessage);
            newMessage.Position = position;
            foreach (Message message in Messages)
            {
                if (message != newMessage && message.Position >= position)
                {
                    message.Position++;
                }
            }

            refresh();
        }

        public void removeMessage(Message toRemove)
        {
            Messages.Remove(toRemove);
            foreach (Message message in Messages)
            {
                if (message.Position > toRemove.Position)
                {
                    message.Position--;
                }
            }
            refresh();
        }

        public Component getComponentFromCoordinate(double coordinate) {

            int position = (int)coordinate * (Components.Count + 1) / ScreenWidth;

            foreach (Component component in Components) {
                if (position == component.Position) {
                    return component;
                }
            }
            
            return null;
        }

        public Message getComponentFromMessage(double coordinate)
        {

            int position = getPositionOfMessage(coordinate);

            foreach (Message message in Messages)
            {
                if (position == message.Position)
                {
                    return message;
                }
            }

            return null;
        }

        public int getPositionOfMessage(double coordinate) {
            return ((int)coordinate / 100);
        }

        public void refresh()
        {
            foreach (Component component in Components)
            {
                component.X = ScreenWidth / (Components.Count + 1) * component.Position;
                component.Width = (ScreenWidth / (Components.Count + 1)) - 20;
                component.Height = 100 + (Messages.Count * 100);
                component.refresh();
            }
            foreach (Message message in Messages) 
            {
                message.Y = 100 * message.Position;
                message.refresh();
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

        public void setNewMessagePosition(Message movingMessage, double coordinate)
        {
            int newPosition = getPositionOfMessage(coordinate);
            System.Console.WriteLine("newPosition before "+newPosition + " coordinate "+coordinate);
            if (newPosition == 0)
            {
                newPosition = 1;
            }
            if (newPosition > Messages.Count)
            {
                System.Console.WriteLine("Component.Count " + Components.Count);
                newPosition = Messages.Count;
            }
            if (newPosition < movingMessage.Position)
            {
                foreach (Message message in Messages)
                {
                    if (message != movingMessage && message.Position >= newPosition && message.Position < movingMessage.Position)
                    {
                        message.Position++;
                    }
                }
            }
            else if (newPosition > movingMessage.Position)
            {
                foreach (Message message in Messages)
                {
                    if (message != movingMessage && message.Position <= newPosition && message.Position > movingMessage.Position)
                    {
                        message.Position--;
                    }
                }
            }
            System.Console.WriteLine("newPosition after " + newPosition);
            movingMessage.Position = newPosition;
            refresh();

        }

    }
}
