using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Elements
{
    [Serializable]
    public class DataStructure
    {
        public ObservableCollection<SerializableComponent> Components = new ObservableCollection<SerializableComponent>();
        public ObservableCollection<SerializableMessage> Messages = new ObservableCollection<SerializableMessage>();

        public DataStructure(ObservableCollection<Component> Components, ObservableCollection<Message> Messages)
        {

            this.Components = new ObservableCollection<SerializableComponent>();
            this.Messages = new ObservableCollection<SerializableMessage>();

            foreach (Component component in Components)
            {
                this.Components.Add(new SerializableComponent(component));
                System.Console.WriteLine("Saved Component");
            }

            foreach (Message message in Messages)
            {
                this.Messages.Add(new SerializableMessage(message));
                System.Console.WriteLine("Saved Message");
            }

        }

        public ObservableCollection<Component> deserialiceComponents()
        {
            ObservableCollection<Component> newComponents = new ObservableCollection<Component>();

            foreach (SerializableComponent component in Components)
            {
                Component newComponent = new Component();
                newComponent.Name = component.name;
                newComponent.Position = component.position;
                newComponents.Add(newComponent);
            }

            return newComponents;

        }

        public ObservableCollection<Message> deserialiceMessages(ObservableCollection<Component> Components)
        {
            ObservableCollection<Message> newMessages = new ObservableCollection<Message>();

            foreach (SerializableMessage message in Messages)
            {
                
                Component start = null, end = null;
                foreach (Component component in Components)
                {
                           
                    if (component.Position == message.startPosition)
                    {
                        start = component;
                    }
                    if (component.Position == message.endPosition)
                    {
                        end = component;
                    }
                }
                if (start != null && end != null)
                {
                                         
                    Message newMessage = new Message(start, end);
                    newMessage.Name = message.name;
                    newMessage.Position = message.position;
                    newMessages.Add(newMessage);
                    System.Console.WriteLine("Things are happening!!");
                }
            }

            return newMessages;

        }


    }
}
