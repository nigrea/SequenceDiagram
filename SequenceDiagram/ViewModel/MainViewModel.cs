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

namespace SequenceDiagram.ViewModel
{
    class MainViewModel
    {



        public ObservableCollection<Component> Components { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        private CommandController commandController = CommandController.GetInstance();

        public ICommand Test { get; private set; }

        public MainViewModel() { 
        
            Components = new ObservableCollection<Component>();
            Test = new RelayCommand(Testy);


        }

 

        public void Testy() {

            commandController.AddAndExecute(new AddComponent(Components));

        }

        


    }
}
