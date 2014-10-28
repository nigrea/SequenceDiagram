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

        private ObservableCollection<Component> components;
        private Component component = new Component();

        public AddComponent(ObservableCollection<Component> components)
        {
            this.components = components;
        }

        public void Run() {
            components.Add(component);
        }

        public void Undo() {
            components.Remove(component);
        }

    }
}
