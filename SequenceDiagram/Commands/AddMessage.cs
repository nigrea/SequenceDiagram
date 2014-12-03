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

        private ComponentGrid componentGrid;
        private Message message;
        private Component start;
        private int position;

        public AddMessage(Component start, Component end, ComponentGrid componentGrid, int position)
        {
            message = new Message(start, end);
            this.componentGrid = componentGrid;
            this.start = start;
            this.position = position;
        }

        public void Run()
        {
            
            componentGrid.addMessage(message,position);
            start.Messages.Add(message);
        }

        public void Undo()
        {
            componentGrid.removeMessage(message);
            start.Messages.Remove(message);
        }
    }
}
