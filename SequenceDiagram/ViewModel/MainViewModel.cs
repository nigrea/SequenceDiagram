using System.Windows.Input;
using SequenceDiagram.Commands;
using Elements;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;

namespace SequenceDiagram.ViewModel
{
    class MainViewModel
    {


        public ComponentGrid ComponentGrid;
        public ObservableCollection<Component> Components { get; set; }
        private CommandController commandController = CommandController.GetInstance();

        public ICommand Test { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand LoadCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand RedoCommand { get; private set; }

        public MainViewModel() {

            ComponentGrid = new ComponentGrid();
            Components = ComponentGrid.Components;
            Test = new RelayCommand(Testy);
            SaveCommand = new RelayCommand(Save);
            LoadCommand = new RelayCommand(Load);
            UndoCommand = new RelayCommand(commandController.Undo, commandController.CanUndo);
            RedoCommand = new RelayCommand(commandController.Redo, commandController.CanRedo);



        }

        public void Save() {
            SaveFileDialog filedialog = new SaveFileDialog();
            filedialog.FileName = "untitled";
            filedialog.DefaultExt = ".sqd";
            Nullable<bool> result = filedialog.ShowDialog();
            if (result == true) {
                using (Stream stream = File.Open(filedialog.FileName, FileMode.Create))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    bformatter.Serialize(stream, ComponentGrid);
                }
            }
            
        }

        public void Load() {
            OpenFileDialog filedialog = new OpenFileDialog();
            Nullable<bool> result = filedialog.ShowDialog();
            if (result == true) {
                commandController.clearStacks();
                using (Stream stream = File.Open(filedialog.FileName, FileMode.Open))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();


                    ComponentGrid = (ComponentGrid)bformatter.Deserialize(stream);
                    
                }
            }
           
        }

        public void Testy() {

            commandController.AddAndExecute(new AddComponent(ComponentGrid));

        }

        


    }
}
