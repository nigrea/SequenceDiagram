using System.Windows.Input;
using SequenceDiagram.Commands;
using Elements;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media;
using Microsoft.Win32;

namespace SequenceDiagram.ViewModel
{
    public class MainViewModel
    {

        private Point moveShapePoint;
        public ComponentGrid ComponentGrid;
        public ObservableCollection<Component> Components { get; set; }
        private CommandController commandController = CommandController.GetInstance();

        public ICommand Test { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand MouseDownComponentCommand { get; private set; }
        public ICommand MouseMoveComponentCommand { get; private set; }
        public ICommand MouseUpComponentCommand { get; private set; }
        public MainViewModel()
        {

            ComponentGrid = new ComponentGrid();
            Components = ComponentGrid.Components;
            Test = new RelayCommand(Testy);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            UndoCommand = new RelayCommand(commandController.Undo, commandController.CanUndo);
            RedoCommand = new RelayCommand(commandController.Redo, commandController.CanRedo);
            MouseDownComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownComponent);
            MouseMoveComponentCommand = new RelayCommand<MouseEventArgs>(MouseMoveComponent);
            MouseUpComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpComponent);



        }

        public void Save()
        {
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.FileName = "untitled";
            filedialog.DefaultExt = ".sqd";
            Nullable<bool> result = filedialog.ShowDialog();
            if (result == true)
            {
                using (Stream stream = File.Open(filedialog.FileName, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bformatter.Serialize(stream, ComponentGrid);
                }
            }

        }

        public void Load()
        {
            OpenFileDialog filedialog = new OpenFileDialog();
            Nullable<bool> result = filedialog.ShowDialog();
            if (result == true)
            {
                commandController.clearStacks();
                using (Stream stream = File.Open(filedialog.FileName, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();


                    ComponentGrid = (ComponentGrid)bformatter.Deserialize(stream);

                }
            }

        }

        public void Testy()
        {

            commandController.AddAndExecute(new AddComponent(ComponentGrid));

        }

        public void MouseDownComponent(MouseButtonEventArgs e)
        {
            System.Console.WriteLine("MouseDown!!!");
            e.MouseDevice.Target.CaptureMouse();
        }

        // This is only used for moving a Shape, and only if the mouse is already captured.
        public void MouseMoveComponent(MouseEventArgs e)
        {
            System.Console.WriteLine("MouseMove!!!");
            // Checks that the mouse is captured and that a line is not being drawn.
            if (Mouse.Captured != null)
            {
                // It is now known that the mouse is captured and here the visual element that the mouse is captured by is retrieved.
                FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
                // From the shapes visual element, the Shape object which is the DataContext is retrieved.
                Component componentModel = (Component)shapeVisualElement.DataContext;
                // The canvas holding the shapes visual element, is found by searching up the tree of visual elements.
                Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
                // The mouse position relative to the canvas is gotten here.
                Point mousePosition = Mouse.GetPosition(canvas);
                // When the shape is moved with the mouse, this method is called many times, for each part of the movement.
                // Therefore to only have 1 Undo/Redo command saved for the whole movement, the initial position is saved, 
                //  during the first part of the movement, so that it together with the final position, 
                //  from when the mouse is released, can become one Undo/Redo command.
                if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;
                // The Shape is moved to the position of the mouse in relation to the canvas.
                // The View (GUI) is then notified by the Shape, that its properties have changed.
                componentModel.CanvasCenterX = (int)mousePosition.X;
                componentModel.CanvasCenterY = (int)mousePosition.Y;
            }
        }

        // There are two reasons for doing a 'MouseUp'.
        // Either a Line is being drawn, and the second Shape has just been chosen.
        // Or a Shape is being moved and the move is now done.
        public void MouseUpComponent(MouseButtonEventArgs e)
        {
            System.Console.WriteLine("MouseUp!!!");
            // Here the visual element that the mouse is captured by is retrieved.
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            // From the shapes visual element, the Shape object which is the DataContext is retrieved.
            Component component = (Component)shapeVisualElement.DataContext;
            // The canvas holding the shapes visual element, is found by searching up the tree of visual elements.
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            // The mouse position relative to the canvas is gotten here.
            Point mousePosition = Mouse.GetPosition(canvas);

            ComponentGrid.setNewPosition(component, mousePosition.X);

            // Now that the Move Shape operation is over, the Shape is moved to the final position (coordinates), 
            //  by using a MoveNodeCommand to move it.
            // The MoveNodeCommand is given the original coordinates and with respect to the Undo/Redo functionality, 
            //  the Shape has only been moved once, with this Command.
            //undoRedoController.AddAndExecute(new MoveShapeCommand(component, (int)moveShapePoint.X, (int)moveShapePoint.Y, (int)mousePosition.X, (int)mousePosition.Y));
            component.Y = 0;
            // The original Shape point before the move is cleared, so the MainViewModel is ready for the next move operation.
            moveShapePoint = new Point();
            // The mouse is released, as the move operation is done, so it can be used by other controls.
            e.MouseDevice.Target.ReleaseMouseCapture();

        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }

    }
}
