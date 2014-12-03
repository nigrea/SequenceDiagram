using System;
using Elements;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    class RemoveComponent : IUndoableCommand
    {

        private ComponentGrid componentGrid;
        private Component toRemove;

        public RemoveComponent(Component toRemove, ComponentGrid componentGrid)
        {
            this.componentGrid = componentGrid;                
            this.toRemove = toRemove;        
        }

        public void Run()
        {
            componentGrid.removeComponent(toRemove);                        
        }

        public void Undo()
        {
            componentGrid.addComponent(toRemove);            
        }
    }
}
