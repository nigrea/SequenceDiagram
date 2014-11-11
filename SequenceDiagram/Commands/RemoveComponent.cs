using System;
using Elements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    class RemoveComponent
    {

        private ObservableCollection<Component> components;
        private ObservableCollection<Component> toRemove;
        private ObservableCollection<Message> messages;

        public RemoveComponent(ObservableCollection<Component> components, ObservableCollection<Message> messages, ObservableCollection<Component> toRemove)
        {
            this.components = components;
            this.messages = messages;
            this.toRemove = toRemove;        
        }

        public void Run()
        {
            foreach (Component component in toRemove){
                components.Remove(component);
                foreach (Message message in component.Messages) messages.Remove(message);
            }
            
        }

        public void Undo()
        {
            foreach (Component component in toRemove){
                components.Add(component);
                foreach (Message message in component.Messages) messages.Add(message);
            }
        }
    }
}
