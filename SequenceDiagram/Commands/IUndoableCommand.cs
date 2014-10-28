using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    public interface IUndoableCommand
    {
        void Run();
        void Undo();
           
    }
}
