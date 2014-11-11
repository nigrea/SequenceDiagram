using System;
using Elements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SequenceDiagram.Commands
{
    class RemoveMessage
    {

        private ObservableCollection<Message> toRemove;
        private ObservableCollection<Message> messages;

        public RemoveMessage(ObservableCollection<Message> messages, ObservableCollection<Message> toRemove)
        {
            this.messages = messages;
            this.toRemove = toRemove;
        }

        public void Run() {
            foreach (Message message in toRemove)
            {
                messages.Remove(message);
            }
        }

        public void Undo() {
            foreach (Message message in toRemove)
            {
                messages.Add(message);
            }
        }

    }
}
