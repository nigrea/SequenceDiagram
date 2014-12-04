using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class MoveMessage : IUndoableCommand
    {
        public ComponentGrid componentGrid;
        public Message message;
        public double oldCoordinate, newCoordinate;

        public MoveMessage(ComponentGrid componentGrid, Message message)
        {
            this.componentGrid = componentGrid;
            this.message = message;
            this.oldCoordinate = message.Y;
        }

        public void Run() {
            componentGrid.setNewMessagePosition(message, newCoordinate);
        }

        public void Undo() {
            componentGrid.setNewMessagePosition(message, oldCoordinate);
        }
    }
}
