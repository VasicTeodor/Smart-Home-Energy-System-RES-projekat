using SmartHomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace SmartHomeManager
{
    /// <summary>
    /// Interaction logic for AddConsumerWindow.xaml
    /// </summary>
    public partial class AddConsumerWindow : Window
    {
        public AddConsumerWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                int newId = SHES.devicesList.Count + 1;
                //SHES.devicesList.Add(new Model.Consumers { Id = newId, Working = false, Name = txtName.Text });
                ConsumersViewModel.Consumers.Add(new Model.Consumers { Id = newId, Working = false, Name = txtName.Text });
                //logika za upis novih
                SHES.importer.SerializeObject<ObservableCollection<Model.Consumers>>(ConsumersViewModel.Consumers, "../../ConfigFiles/ConsumersConfig.xml");
                this.Close();
            }
            else
            {
                MessageBox.Show("Error while adding new consumer!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Trim().Equals(""))
            {
                txtName.BorderBrush = Brushes.Red;
                txtName.BorderThickness = new Thickness(2);
                labelError.Content = "Name field can't be empty!";
            }
            else
            {
                txtName.BorderBrush = Brushes.Green;
                labelError.Content = "";
            }
        }

        private bool Validate()
        {
            bool result = true;

            if (txtName.Text.Trim().Equals(""))
            {
                result = false;
                txtName.BorderBrush = Brushes.Red;
                txtName.BorderThickness = new Thickness(2);
                labelError.Content = "Name field can't be empty!";
            }
            else
            {
                txtName.BorderBrush = Brushes.Green;
                labelError.Content = "";
            }

            return result;
        }
    }
}
