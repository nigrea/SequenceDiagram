using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsProgrammingAssignment.ViewModel
{
    class MainViewModel
    {
        public ICommand ExpandButton { get; private set; }

        public MainViewModel() { 
        
            ExpandButton = new RelayCommand(ExpandSideBar);

        }

        public void ExpandSideBar()
        {
            System.Console.WriteLine("IT WORKS!!!! YAY");       
        }

        /*    private void ExpandToolbar_Button_Click(object sender, RoutedEventArgs e)
        {
            if (rect.Visibility == System.Windows.Visibility.Collapsed)
            {
                rect.Visibility = System.Windows.Visibility.Visible;
                (sender as Button).Content = "<";
                (sender as Button).Margin = new Thickness(65, 226, 0, 0); 
                
            }
            else
            {
                rect.Visibility = System.Windows.Visibility.Collapsed;
                (sender as Button).Content = ">";
                (sender as Button).Margin = new Thickness(0, 226, 0, 0); 
            }     
        }*/


    }
}
