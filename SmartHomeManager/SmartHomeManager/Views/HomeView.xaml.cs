using System;
using System.Collections.Generic;
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

namespace SmartHomeManager.Views
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
            this.DataContext = new SmartHomeManager.ViewModel.HomeViewModel();

            new Thread(() =>
            {
                while (!SHES.shutDown)
                {
                    Dispatcher.Invoke(() =>
                    {
                        labelTime.Content = DateTime.Now.ToString("HH : mm : ss");
                        labelDate.Content = DateTime.Now.ToString(("MMMM dd, yyyy")) + ".";

                        if (Int32.Parse(DateTime.Now.ToString("mm")) >= 50 && Int32.Parse(DateTime.Now.ToString("mm")) < 55)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s82.png", UriKind.Relative));
                            imgSecond.Source = new BitmapImage(new Uri(@"/images/s76.png", UriKind.Relative));
                        }
                        else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 55 || Int32.Parse(DateTime.Now.ToString("mm")) < 15)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s81.ico", UriKind.Relative));
                            imgSecond.Source = new BitmapImage();
                        }
                        else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 15 && Int32.Parse(DateTime.Now.ToString("mm")) < 22)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s76.png", UriKind.Relative));
                            imgSecond.Source = new BitmapImage(new Uri(@"/images/s82.png", UriKind.Relative));
                        }
                        else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 22 && Int32.Parse(DateTime.Now.ToString("mm")) < 30)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s76.png", UriKind.Relative));
                            imgSecond.Source = new BitmapImage();
                        }
                        else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 30 && Int32.Parse(DateTime.Now.ToString("mm")) < 45)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s78.png", UriKind.Relative));
                            imgSecond.Source = new BitmapImage();
                        }
                        else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 45 && Int32.Parse(DateTime.Now.ToString("mm")) < 50)
                        {
                            imgFirst.Source = new BitmapImage(new Uri(@"/images/s76.png", UriKind.Relative));
                            imgSecond.Source = new BitmapImage(new Uri(@"/images/s82.png", UriKind.Relative));
                        }


                    });
                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
