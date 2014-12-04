using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class MoveBox : IUndoableCommand
    {
        public ComponentGrid componentGrid;
        public Box box;
        public int oldX, oldY, newX, newY;

        public MoveBox(ComponentGrid componentGrid, Box box)
        {
            this.componentGrid = componentGrid;
            this.box = box;
            this.oldX = box.CanvasLeft;
            this.oldY = box.CanvasTop;
        }

        public void Run() {
            box.CanvasLeft = newX;
            box.CanvasTop = newY;
        }

        public void Undo() {
            box.CanvasLeft = oldX;
            box.CanvasTop = oldY;
        }

    }
}
