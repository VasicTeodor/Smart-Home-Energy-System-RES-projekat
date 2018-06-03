using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartHomeManager.Views
{
    /// <summary>
    /// Interaction logic for ConsumersView.xaml
    /// </summary>
    public partial class ConsumersView : UserControl, INotifyPropertyChanged
    {
        private bool buttonOn;
        public event PropertyChangedEventHandler PropertyChanged; // delegate { };

        public bool ButtonOn
        {
            get { return buttonOn; }
            set
            {
                if (buttonOn != value)
                {
                    buttonOn = value;
                    RaisePropertyChanged("ButtonOn");
                }
            }
        }


        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public ConsumersView()
        {
            InitializeComponent();
            this.DataContext = new SmartHomeManager.ViewModel.ConsumersViewModel();
        }

        private void buttonOn_Click(object sender, RoutedEventArgs e)
        {
            //Background="#FF63BF63"
            if (ButtonOn == true)
            {
                ButtonOn = false;
            }
            else
            {
                ButtonOn = true;
            }
        }

        private void buttonOff_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
