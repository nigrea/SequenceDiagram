using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elements;

namespace SequenceDiagram.Commands
{
    public class ResizeBox : IUndoableCommand
    {
        public ComponentGrid componentGrid;
        public Box box;
        public int oldWidth, oldHeight, newWidth, newHeight;

        public ResizeBox(ComponentGrid componentGrid, Box box)
        {
            this.componentGrid = componentGrid;
            this.box = box;
            this.oldWidth = box.Width;
            this.oldHeight = box.Height;
        }

        public void Run() {
            if (newHeight < 100) {
                newHeight = 100;
            }
            if (newWidth < 100) {
                newWidth = 100;
            }

            box.Width = newWidth;
            box.Height = newHeight;
        }

        public void Undo() {
            box.Width = oldWidth;
            box.Height = oldHeight;
        }
    }
}
