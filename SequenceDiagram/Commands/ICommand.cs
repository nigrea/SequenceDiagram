using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsProgrammingAssignment.Command
{
    interface ICommand
    {
        void run();
        void undo();
           
    }
}
