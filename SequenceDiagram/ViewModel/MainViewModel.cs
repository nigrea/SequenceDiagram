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
        private Point messageStartPoint;
        public bool changeBoxSize, enabledDelete;
        private MoveComponent moveComponent;
        private MoveBox moveBox;
        private MoveMessage moveMessage;
        private ResizeBox resizeBox;

        public ObservableCollection<Component> Components { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public ObservableCollection<Box> Boxes { get; set; }

        private CommandController commandController = CommandController.GetInstance();

        public ICommand Test { get; private set; }

        public ICommand AddBoxCommand { get; private set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }
        public ICommand ChangeBoxModeCommand { get; private set; }
        public ICommand EnableDeleteCommand { get; private set; }

        public ICommand MouseDownComponentCommand { get; private set; }
        public ICommand MouseMoveComponentCommand { get; private set; }
        public ICommand MouseUpComponentCommand { get; private set; }
        public ICommand MouseDownLineCommand { get; private set; }
        public ICommand MouseMoveLineCommand { get; private set; }
        public ICommand MouseUpLineCommand { get; private set; }

        public ICommand MouseDownBoxCommand { get; private set; }
        public ICommand MouseMoveBoxCommand { get; private set; }
        public ICommand MouseUpBoxCommand { get; private set; }

        public ICommand MouseDownMessageCommand { get; private set; }
        public ICommand MouseMoveMessageCommand { get; private set; }
        public ICommand MouseUpMessageCommand { get; private set; }

        public MainViewModel()
        {
            changeBoxSize = false;
            enabledDelete = false;
            ComponentGrid = new ComponentGrid();
            Components = ComponentGrid.Components;
            Messages = ComponentGrid.Messages;
            Boxes = ComponentGrid.Boxes;
            Test = new RelayCommand(Testy);
            AddBoxCommand = new RelayCommand(addBox);
            ChangeBoxModeCommand = new RelayCommand(changeBoxMode);
            EnableDeleteCommand = new RelayCommand(enableDelete);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            UndoCommand = new RelayCommand(commandController.Undo);
            RedoCommand = new RelayCommand(commandController.Redo);
            MouseDownComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownComponent);
            MouseMoveComponentCommand = new RelayCommand<MouseEventArgs>(MouseMoveComponent);
            MouseUpComponentCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpComponent);
            MouseDownLineCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownLine);
            MouseMoveLineCommand = new RelayCommand<MouseEventArgs>(MouseMoveLine);
            MouseUpLineCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpLine);
            MouseDownMessageCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownMessage);
            MouseMoveMessageCommand = new RelayCommand<MouseEventArgs>(MouseMoveMessage);
            MouseUpMessageCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpMessage);
            MouseDownBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseDownBox);
            MouseMoveBoxCommand = new RelayCommand<MouseEventArgs>(MouseMoveBox);
            MouseUpBoxCommand = new RelayCommand<MouseButtonEventArgs>(MouseUpBox);

            //for testing!!!:

            commandController.AddAndExecute(new AddComponent(ComponentGrid));
            commandController.AddAndExecute(new AddComponent(ComponentGrid));




        }

        public void enableDelete()
        {
            enabledDelete = !enabledDelete;
        }

        public void changeBoxMode()
        {
            changeBoxSize = !changeBoxSize;
        }

        public void addBox()
        {
            commandController.AddAndExecute(new AddBox(ComponentGrid));
            System.Console.WriteLine("AddBox in Mvm");
        }

        public void newFile()
        {

            ComponentGrid.Components.Clear();
            ComponentGrid.Messages.Clear();
            commandController.clearStacks();

        }

        public void Save()
        {
            DataStructure dataStructure = new DataStructure(Components, Messages);
            Task task = new Task(() => SaveAsynch(dataStructure));
            task.Start();
        }

        static async void SaveAsynch(DataStructure dataStructure)
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
                    bformatter.Serialize(stream, dataStructure);
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


                    DataStructure dataStructure = (DataStructure)bformatter.Deserialize(stream);


                    newFile();

                    ObservableCollection<Component> savedComponents = dataStructure.deserialiceComponents();
                    foreach (Component component in savedComponents)
                    {

                        ComponentGrid.addComponent(component);
                    }
                    ObservableCollection<Message> savedMessanges = dataStructure.deserialiceMessages(ComponentGrid.Components);
                    foreach (Message message in savedMessanges)
                    {
                        ComponentGrid.addMessage(message, message.Position);
                        System.Console.WriteLine("Message added to thingy!!");
                    }

                    ComponentGrid.refresh();

                }
            }

        }

        public void Testy()
        {

            commandController.AddAndExecute(new AddComponent(ComponentGrid));
        }

        public void MouseDownComponent(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Component componentModel = (Component)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);

            if (enabledDelete)
            {
                commandController.AddAndExecute(new RemoveComponent(componentModel, ComponentGrid));
            }
            else
            {
                moveComponent = new MoveComponent(ComponentGrid, componentModel, mousePosition.X);

                e.MouseDevice.Target.CaptureMouse();
            }
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

            moveComponent.newCoordinate = mousePosition.X;
            commandController.AddAndExecute(moveComponent);

            //undoRedoController.AddAndExecute(new MoveShapeCommand(component, (int)moveShapePoint.X, (int)moveShapePoint.Y, (int)mousePosition.X, (int)mousePosition.Y));
            component.Y = 0;

            moveShapePoint = new Point();

            e.MouseDevice.Target.ReleaseMouseCapture();

        }


        public void MouseDownMessage(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Message message = (Message)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);
            if (enabledDelete)
            {
                commandController.AddAndExecute(new RemoveMessage(message, ComponentGrid));

            }
            else
            {
                moveMessage = new MoveMessage(ComponentGrid, message);
                e.MouseDevice.Target.CaptureMouse();
            }
        }

        public void MouseMoveMessage(MouseEventArgs e)
        {
            if (Mouse.Captured != null)
            {

                FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
                Message componentModel = (Message)shapeVisualElement.DataContext;
                Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
                Point mousePosition = Mouse.GetPosition(canvas);

                if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;


                ComponentGrid.setNewMessagePosition(componentModel, mousePosition.Y);

                //componentModel.CanvasCenterX = (int)mousePosition.X;
                //componentModel.CanvasCenterY = (int)mousePosition.Y;
            }
        }

        public void MouseUpMessage(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Message component = (Message)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);

            moveMessage.newCoordinate = mousePosition.Y;
            commandController.AddAndExecute(moveMessage);

            //ComponentGrid.setNewMessagePosition(component, mousePosition.X);

            //undoRedoController.AddAndExecute(new MoveShapeCommand(component, (int)moveShapePoint.X, (int)moveShapePoint.Y, (int)mousePosition.X, (int)mousePosition.Y));            

            e.MouseDevice.Target.ReleaseMouseCapture();

        }


        public void MouseDownBox(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Box box = (Box)shapeVisualElement.DataContext;
            if (enabledDelete)
            {
                commandController.AddAndExecute(new RemoveBox(box, ComponentGrid));

            }
            else
            {
                if (!changeBoxSize)
                {
                    moveBox = new MoveBox(ComponentGrid, box);
                }
                else
                {
                    resizeBox = new ResizeBox(ComponentGrid, box);
                }
                e.MouseDevice.Target.CaptureMouse();
            }
        }

        public void MouseMoveBox(MouseEventArgs e)
        {
            if (Mouse.Captured != null)
            {
                if (!changeBoxSize)
                {
                    FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

                    Box box = (Box)shapeVisualElement.DataContext;

                    Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);

                    Point mousePosition = Mouse.GetPosition(canvas);

                    if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;

                    box.CanvasLeft = (int)mousePosition.X;
                    box.CanvasTop = (int)mousePosition.Y;
                }
                else
                {
                    FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;

                    Box box = (Box)shapeVisualElement.DataContext;

                    Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);

                    Point mousePosition = Mouse.GetPosition(canvas);

                    if (moveShapePoint == default(Point)) moveShapePoint = mousePosition;


                    int newWidth = (int)mousePosition.X - box.CanvasLeft;
                    int newHeight = (int)mousePosition.Y - box.CanvasTop;

                    if (newWidth > 50)
                    {
                        box.Width = newWidth;
                    }
                    if (newHeight > 50)
                    {
                        box.Height = newHeight;
                    }

                }
            }
        }

        public void MouseUpBox(MouseButtonEventArgs e)
        {
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            Box box = (Box)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            Point mousePosition = Mouse.GetPosition(canvas);


            if (!changeBoxSize)
            {
                moveBox.newX = (int)mousePosition.X;
                moveBox.newY = (int)mousePosition.Y;

                commandController.AddAndExecute(moveBox);
            }
            else
            {
                resizeBox.newWidth = (int)mousePosition.X - box.CanvasLeft;
                resizeBox.newHeight = (int)mousePosition.Y - box.CanvasTop;

                commandController.AddAndExecute(resizeBox);
            }

            e.MouseDevice.Target.ReleaseMouseCapture();

        }


        public void MouseDownLine(MouseButtonEventArgs e)
        {
            e.MouseDevice.Target.CaptureMouse();
            FrameworkElement shapeVisualElement = (FrameworkElement)e.MouseDevice.Target;
            addLineFrom = (Component)shapeVisualElement.DataContext;
            Canvas canvas = FindParentOfType<Canvas>(shapeVisualElement);
            messageStartPoint = Mouse.GetPosition(canvas);
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
            if (startComponent != endComponent)
            {
                int position = ComponentGrid.getPositionOfMessage(messageStartPoint.Y) + 1;

                if (endComponent != null)
                {
                    commandController.AddAndExecute(new AddMessage(startComponent, endComponent, ComponentGrid, position));
                }
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
