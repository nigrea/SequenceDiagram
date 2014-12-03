using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class AddBox : IUndoableCommand
    {
        public ComponentGrid componentGrid;
        private Box box = new Box();

        public AddBox(ComponentGrid componentGrid)
        {
            this.componentGrid = componentGrid;
        }

        public void Run() {
            componentGrid.addBox(box);
        }

        public void Undo() {
            componentGrid.removeBox(box);
        }
    }
}
