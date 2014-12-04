using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class MoveComponent : IUndoableCommand
    {
        public ComponentGrid componentGrid;
        public Component component;
        public double oldCoordinate, newCoordinate;

        public MoveComponent(ComponentGrid componentGrid, Component component, double coordinate)
        {
            this.componentGrid = componentGrid;
            this.component = component;
            this.oldCoordinate = coordinate;
        }

        public void Run() {
            componentGrid.setNewPosition(component, newCoordinate);       
        }

        public void Undo() {
            componentGrid.setNewPosition(component, oldCoordinate);            
        }
    }
}
