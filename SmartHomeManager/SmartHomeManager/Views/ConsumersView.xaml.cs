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
    public partial class ConsumersView : UserControl
    {
        public ConsumersView()
        {
            InitializeComponent();
            this.DataContext = new SmartHomeManager.ViewModel.ConsumersViewModel();
        }

        private void AddConsumer_Click(object sender, RoutedEventArgs e)
        {
            AddConsumerWindow addConsumer = new AddConsumerWindow();
            addConsumer.ShowDialog();
        }
    }
}
