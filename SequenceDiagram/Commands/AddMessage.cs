using System;
using Elements;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    class AddMessage : IUndoableCommand
    {

        private ObservableCollection<Message> messages;
        private Message message;
        private Component start;

        public AddMessage(Component start, Component end, ObservableCollection<Message> messages)
        {
            message = new Message(start, end);
            this.messages = messages;
            this.start = start;
        }

        public void Run()
        {
            messages.Add(message);
            start.Messages.Add(message);
        }

        public void Undo()
        {
            messages.Remove(message);
            start.Messages.Remove(message);
        }
    }
}
