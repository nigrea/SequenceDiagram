using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class RemoveBox : IUndoableCommand
    {
        private Box toRemove;
        private ComponentGrid componentGrid;

        public RemoveBox(Box toRemove, ComponentGrid componentGrid)
        {
            this.toRemove = toRemove;
            this.componentGrid = componentGrid;
        }

        public void Run()
        {

            componentGrid.removeBox(toRemove);

        }

        public void Undo()
        {

            componentGrid.addBox(toRemove);

        }

    }
}
