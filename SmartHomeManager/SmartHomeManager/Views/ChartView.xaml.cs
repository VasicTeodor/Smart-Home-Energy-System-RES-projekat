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

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            canGraph.Children.Clear();

            List<double> batteryPlotValues = new List<double>();
            List<double> solarPanelsPlotValues = new List<double>();
            List<double> utilityPlotValues = new List<double>();
            List<double> consumersPlotValues = new List<double>();
            List<double> hlp = new List<double>();

            hlp = SHES.importer.ReadLog("UtilityLog.xml", "Utility");
            utilityPlotValues = LimitValues(hlp);

            hlp = SHES.importer.ReadLog("BatteryLog.xml", "Battery");
            batteryPlotValues = LimitValues(hlp);

            hlp = SHES.importer.ReadLog("ConsumptionLog.xml", "Consumption");
            consumersPlotValues = LimitValues(hlp);

            hlp = SHES.importer.ReadLog("SolarPanelsLog.xml", "SolarPanel");
            solarPanelsPlotValues = LimitValues(hlp);

            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymax = canGraph.Height - margin;
            const double stepX = 24.583;
            const double stepY = 10;

            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(new Point(0, ymax/2), new Point(canGraph.Width, ymax/2)));

            for (double x = xmin + stepX; x <= canGraph.Width - stepX; x += stepX)
            {
                xaxis_geom.Children.Add(new LineGeometry(new Point(x, ymax/2 - margin / 2), new Point(x, ymax/2 + margin / 2)));
            }

            System.Windows.Shapes.Path xaxis_path = new System.Windows.Shapes.Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;

            canGraph.Children.Add(xaxis_path);

            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(new Point(xmin, 0), new Point(xmin, canGraph.Height)));

            for (double y = stepY; y <= canGraph.Height; y += stepY)
            {
                yaxis_geom.Children.Add(new LineGeometry(new Point(xmin - margin / 2, y), new Point(xmin + margin / 2, y)));
            }

            System.Windows.Shapes.Path yaxis_path = new System.Windows.Shapes.Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;

            canGraph.Children.Add(yaxis_path);

            int br = 0;
            double y_new;

            PointCollection utilityPoints = new PointCollection();
            for (double x = xmin; x <= ((utilityPlotValues.Count) * stepX); x += stepX)
            {
                y_new = (19.5 - utilityPlotValues[br]);
                if (br < utilityPlotValues.Count)
                {
                    br++;
                }
                utilityPoints.Add(new Point(x, y_new * stepY));
            }

            br = 0;
            y_new = 0;

            PointCollection batteryPoints = new PointCollection();
            for (double x = xmin; x <= ((batteryPlotValues.Count) * stepX); x += stepX)
            {
                y_new = (19.5 - batteryPlotValues[br]);
                if (br < batteryPlotValues.Count)
                {
                    br++;
                }
                batteryPoints.Add(new Point(x, y_new * stepY));
            }

            br = 0;
            y_new = 0;

            PointCollection panelPoints = new PointCollection();
            for (double x = xmin; x <= ((solarPanelsPlotValues.Count) * stepX); x += stepX)
            {
                y_new = (19.5 - solarPanelsPlotValues[br]);
                if (br < solarPanelsPlotValues.Count)
                {
                    br++;
                }
                panelPoints.Add(new Point(x, y_new * stepY));
            }

            br = 0;
            y_new = 0;

            PointCollection consumersPoints = new PointCollection();
            for (double x = xmin; x <= ((consumersPlotValues.Count) * stepX); x += stepX)
            {
                y_new = (19.5 - consumersPlotValues[br]);
                if (br < consumersPlotValues.Count)
                {
                    br++;
                }
                consumersPoints.Add(new Point(x, y_new * stepY));
            }

            br = 0;
            y_new = 0;

            Polyline utilityPolyline = new Polyline();
            utilityPolyline.StrokeThickness = 3;
            utilityPolyline.Stroke = Brushes.Black;
            utilityPolyline.Points = utilityPoints;

            Polyline batteryPolyline = new Polyline();
            batteryPolyline.StrokeThickness = 3;
            batteryPolyline.Stroke = Brushes.Blue;
            batteryPolyline.Points = batteryPoints;

            Polyline consumersPolyline = new Polyline();
            consumersPolyline.StrokeThickness = 3;
            consumersPolyline.Stroke = Brushes.Red;
            consumersPolyline.Points = consumersPoints;

            Polyline panelsPolyline = new Polyline();
            panelsPolyline.StrokeThickness = 3;
            panelsPolyline.Stroke = Brushes.Green;
            panelsPolyline.Points = panelPoints;

            canGraph.Children.Add(utilityPolyline);
            canGraph.Children.Add(batteryPolyline);
            canGraph.Children.Add(panelsPolyline);
            canGraph.Children.Add(consumersPolyline);
        }

        private void cmbObj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            canGraph.Children.Clear();
        }

        private List<double> LimitValues(List<double> values)
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
