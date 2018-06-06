using System;
using System.Collections.Generic;
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
using System.Xml;

namespace SmartHomeManager.Views
{
    /// <summary>
    /// Interaction logic for ChartView.xaml
    /// </summary>
    public partial class ChartView : UserControl
    {
        public ChartView()
        {
            InitializeComponent();
            this.DataContext = new SmartHomeManager.ViewModel.ChartViewModel();

        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime? date = datePicker.SelectedDate;
            labelDate.Content = date.Value.ToString("dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture);

            Show(date.Value.ToShortDateString());
        }

        private void Show(string date)
        {
            List<KeyValuePair<int, double>> batteryPlotValues = new List<KeyValuePair<int, double>>();
            List<KeyValuePair<int, double>> solarPanelsPlotValues = new List<KeyValuePair<int, double>>();
            List<KeyValuePair<int, double>> utilityPlotValues = new List<KeyValuePair<int, double>>();
            List<KeyValuePair<int, double>> consumersPlotValues = new List<KeyValuePair<int, double>>();
            List<KeyValuePair<int, double>> help = new List<KeyValuePair<int, double>>();

            help = SHES.importer.ReadLog("UtilityLog.xml", "Utility", date);
            utilityPlotValues = LimitValues(help);

            help = SHES.importer.ReadLog("BatteryLog.xml", "Battery", date);
            batteryPlotValues = LimitValues(help);

            help = SHES.importer.ReadLog("ConsumptionLog.xml", "Consumption", date);
            consumersPlotValues = LimitValues(help);

            help = SHES.importer.ReadLog("SolarPanelsLog.xml", "SolarPanel", date);
            solarPanelsPlotValues = LimitValues(help);

            KWUtility.ItemsSource = utilityPlotValues;
            KWBattery.ItemsSource = batteryPlotValues;
            KWConsumption.ItemsSource = consumersPlotValues;
            KWPanels.ItemsSource = solarPanelsPlotValues;

            var dataSourceList = new List<List<KeyValuePair<int, double>>>();
            dataSourceList.Add(consumersPlotValues);
            dataSourceList.Add(solarPanelsPlotValues);
            dataSourceList.Add(batteryPlotValues);
            dataSourceList.Add(utilityPlotValues);

            //reportChart.DataContext = dataSourceList;
        }

        private List<KeyValuePair<int, double>> LimitValues(List<KeyValuePair<int, double>> values)
        {
            if (values.Count != 0 && values.Count > 24)
            {
                while (values.Count != 24)
                {
                    values.RemoveAt(0);
                }
            }
            return values;
        }


    }
}
