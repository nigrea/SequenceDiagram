﻿using System;
using Elements;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace SequenceDiagram.Commands
{
    class RemoveMessage : IUndoableCommand
    {

        private Message toRemove;
        private ComponentGrid componentGrid;

        public RemoveMessage(Message toRemove, ComponentGrid componentGrid)
        {
            this.toRemove = toRemove;
            this.componentGrid = componentGrid;
        }

        public void Run()
        {

            componentGrid.removeMessage(toRemove);

        }

        public void Undo()
        {

            componentGrid.addMessage(toRemove, toRemove.Position);

        }

    }
}
