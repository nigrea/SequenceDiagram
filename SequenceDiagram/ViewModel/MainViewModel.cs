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

        private Component addLineFrom;
        private Point moveShapePoint;
        public ComponentGrid ComponentGrid;
        public ObservableCollection<Component> Components { get; set; }
        public ObservableCollection<Message> Messages { get; set; }
        private CommandController commandController = CommandController.GetInstance();

        public ICommand Test { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand MouseDownComponentCommand { get; private set; }
        public ICommand MouseMoveComponentCommand { get; private set; }
        public ICommand MouseUpComponentCommand { get; private set; }
        public ICommand MouseDownLineCommand { get; private set; }
        public ICommand MouseMoveLineCommand { get; private set; }
        public ICommand MouseUpLineCommand { get; private set; }

        public MainViewModel()
        {

            ComponentGrid = new ComponentGrid();
            Components = ComponentGrid.Components;
            Messages = ComponentGrid.Messages;
            Test = new RelayCommand(Testy);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            UndoCommand = new RelayCommand(commandController.Undo, commandController.CanUndo);
            RedoCommand = new RelayCommand(commandController.Redo, commandController.CanRedo);
            MouseDownComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownComponent);
            MouseMoveComponentCommand = new RelayCommand<MouseEventArgs>(MouseMoveComponent);
            MouseUpComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpComponent);
            MouseDownLineCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownLine);
            MouseMoveLineCommand = new RelayCommand<MouseEventArgs>(MouseMoveLine);
            MouseUpLineCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpLine);

            //for testing!!!:

            commandController.AddAndExecute(new AddComponent(ComponentGrid));
            commandController.AddAndExecute(new AddComponent(ComponentGrid));




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
            e.MouseDevice.Target.CaptureMouse();
        }

        public void MouseMoveComponent(MouseEventArgs e)
        {
            if (Mouse.Captured != null)
            {

                FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
                Component componentModel = (Component)shapeVisualElement.DataContext;
                Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
                Point mousePosition = Mouse.GetPosition(canvas);

                if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;


                ComponentGrid.setNewPosition(componentModel, mousePosition.X);

                //componentModel.CanvasCenterX = (int)mousePosition.X;
                //componentModel.CanvasCenterY = (int)mousePosition.Y;
            }
        }

        public void MouseUpComponent(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Component component = (Component)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);

            ComponentGrid.setNewPosition(component, mousePosition.X);

            //undoRedoController.AddAndExecute(new MoveShapeCommand(component, (int)moveShapePoint.X, (int)moveShapePoint.Y, (int)mousePosition.X, (int)mousePosition.Y));
            component.Y = 0;

            moveShapePoint = new Point();

            e.MouseDevice.Target.ReleaseMouseCapture();

        }


        public void MouseDownLine(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            addLineFrom = (Component)shapeVisualElement.DataContext;
        }

        public void MouseMoveLine(MouseEventArgs e)
        {
            /* if (Mouse.Captured != null)
             {

                 FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
                 Component componentModel = (Component)shapeVisualElement.DataContext;
                 Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
                 Point mousePosition = Mouse.GetPosition(canvas);

                 if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;


                 ComponentGrid.setNewPosition(componentModel, mousePosition.X);

                 //componentModel.CanvasCenterX = (int)mousePosition.X;
                 //componentModel.CanvasCenterY = (int)mousePosition.Y;
             }*/
        }

        public void MouseUpLine(MouseButtonEventArgs e)
        {

            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Component startComponent = (Component)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);
            Component endComponent = ComponentGrid.getComponentFromCoordinate(mousePosition.X);


            if (endComponent != null)
            {
                commandController.AddAndExecute(new AddMessage(startComponent, endComponent, ComponentGrid));
            }
            addLineFrom = null;

            moveShapePoint = new Point();

            e.MouseDevice.Target.ReleaseMouseCapture();

        }

        private static T FindParentOfType<T>(DependencyObject o)
        {
            dynamic parent = VisualTreeHelper.GetParent(o);
            return parent.GetType().IsAssignableFrom(typeof(T)) ? parent : FindParentOfType<T>(parent);
        }

    }
}
