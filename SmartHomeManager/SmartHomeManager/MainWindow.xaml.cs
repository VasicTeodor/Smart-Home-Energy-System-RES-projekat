using SmartHomeManager.Model;
using SmartHomeManager.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace SmartHomeManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            new Thread(() =>
            {
                while (!SHES.shutDown)
                {
                    InvokeMethod();
                    Thread.Sleep(100);
                }
            }).Start();
        }

        private void InvokeMethod()
        {
            Dispatcher.Invoke(() =>
            {
                //labelPanel.Content = SHES.panelState;
                labelPanelPower.Content = SHES.solarPnaelsPower;
                labelBattery.Content = SHES.batteryState;
                labelBatteryCapacity.Content = SHES.batteryCapacity;
                labelBatteryCapacityMin.Content = SHES.batteryCapacityMin;
                //labelUtilityPower.Content = SHES.utilityPower;
                labelConsumersConsumption.Content = SHES.consumersConsumption;
                /* otprilike ovako nesto
                if (SHES.panelState == generating)
                {
                    imgPanel.Source = "images/s21.png";
                }
                else if (SHES.panelState == nista)
                {
                    imgPanel.Source = "images/s23.png";
                }
                */
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SHES.shutDown = true;
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            btnHome.IsEnabled = false;
            btnConsumers.IsEnabled = true;
            btnPanel.IsEnabled = true;
            btnUtility.IsEnabled = true;
            btnBattery.IsEnabled = true;
            btnReport.IsEnabled = true;
        }


        private void ButtonConsumers_Click(object sender, RoutedEventArgs e)
        {
            btnConsumers.IsEnabled = false;
            btnPanel.IsEnabled = true;
            btnUtility.IsEnabled = true;
            btnBattery.IsEnabled = true;
            btnReport.IsEnabled = true;
            btnHome.IsEnabled = true;
        }


        private void btnPanel_Click(object sender, RoutedEventArgs e)
        {
            btnPanel.IsEnabled = false;
            btnConsumers.IsEnabled = true;
            btnUtility.IsEnabled = true;
            btnBattery.IsEnabled = true;
            btnReport.IsEnabled = true;
            btnHome.IsEnabled = true;
        }

        private void btnUtility_Click(object sender, RoutedEventArgs e)
        {
            btnUtility.IsEnabled = false;
            btnConsumers.IsEnabled = true;
            btnPanel.IsEnabled = true;
            btnBattery.IsEnabled = true;
            btnReport.IsEnabled = true;
            btnHome.IsEnabled = true;
        }

        private void btnBattery_Click(object sender, RoutedEventArgs e)
        {
            btnBattery.IsEnabled = false;
            btnConsumers.IsEnabled = true;
            btnPanel.IsEnabled = true;
            btnUtility.IsEnabled = true;
            btnReport.IsEnabled = true;
            btnHome.IsEnabled = true;
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            btnReport.IsEnabled = false;
            btnConsumers.IsEnabled = true;
            btnPanel.IsEnabled = true;
            btnUtility.IsEnabled = true;
            btnBattery.IsEnabled = true;
            btnHome.IsEnabled = true;
        }

        private void AddConsumer_Click(object sender, RoutedEventArgs e)
        {
            AddConsumerWindow addConsumer = new AddConsumerWindow();
            addConsumer.ShowDialog();
        }
    }
}
