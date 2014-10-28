using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SequenceDiagram.Commands
{
    class CommandController
    {

        // Part of singleton pattern.
        private static CommandController controller = new CommandController();

        // Undo stack.
        private readonly Stack<IUndoableCommand> undoStack = new Stack<IUndoableCommand>();
        // Redo stack.
        private readonly Stack<IUndoableCommand> redoStack = new Stack<IUndoableCommand>();

        // Part of singleton pattern.
        private CommandController() { }

        // Part of singleton pattern.
        public static CommandController GetInstance() { return controller; }

        // Bruges til at tilføje commander.
        public void AddAndExecute(IUndoableCommand command)
        {
            undoStack.Push(command);
            redoStack.Clear();
            command.Run();
        }

        // Sørger for at undo kun kan kaldes når der er kommandoer i undo stacken.
        public bool CanUndo()
        {
            return undoStack.Any();
        }

        // Udfører undo hvis det kan lade sig gøre.
        public void Undo()
        {
            if (undoStack.Count() <= 0) throw new InvalidOperationException();
            IUndoableCommand command = undoStack.Pop();
            redoStack.Push(command);
            command.Undo();
        }

        // Sørger for at redo kun kan kaldes når der er kommandoer i redo stacken.
        public bool CanRedo()
        {
            return redoStack.Any();
        }

        // Udfører redo hvis det kan lade sig gøre.
        public void Redo()
        {
            if (redoStack.Count() <= 0) throw new InvalidOperationException();
            IUndoableCommand command = redoStack.Pop();
            undoStack.Push(command);
            command.Run();
        }

    }
}
