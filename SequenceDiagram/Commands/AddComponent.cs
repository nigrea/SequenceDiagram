using GalaSoft.MvvmLight.Command;
using Elements;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    public class AddComponent : IUndoableCommand
    {

        public ComponentGrid componentGrid;
        private Component component = new Component();

        public AddComponent(ComponentGrid componentGrid)
        {
            this.componentGrid = componentGrid;
        }

        public void Run() {
            componentGrid.addComponent(component);
        }

        public void Undo() {
            componentGrid.removeComponent(component);
        }

    }
}
