using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    public class SolarPanelViewModel : BindableBase
    {
        public ObservableCollection<SolarPanel> Panels { get; set; }
        private SolarPanel selectedPanel;
        private double currentPowerPanels;

        public SolarPanelViewModel()
        {
            LoadPanels();
            SendPowerToShes();
        }

        public SolarPanel SelectedPanel 
        {
            get { return selectedPanel; }
            set
            {
                selectedPanel = value;
            }
        }

        public void LoadPanels()
        {
            ObservableCollection<SolarPanel> panels = new ObservableCollection<SolarPanel>();

            panels.Add(new SolarPanel { Name = "Panel A", MaxPower = 5.43 });
            panels.Add(new SolarPanel { Name = "Panel B", MaxPower = 3.14 });

            Panels = panels;
        }

        public void SendPowerToShes()
        {
            new Thread(() =>
            {
                while (true)
                {
                    currentPowerPanels = 0;

                    if (Int32.Parse(DateTime.Now.ToString("HH")) >= 20 || Int32.Parse(DateTime.Now.ToString("HH")) < 6)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower * 0;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 6 && Int32.Parse(DateTime.Now.ToString("HH")) < 8)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower * (20 / 100);
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 8 && Int32.Parse(DateTime.Now.ToString("HH")) < 11)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower * (50 / 100);
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 11 && Int32.Parse(DateTime.Now.ToString("HH")) < 16)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 16 && Int32.Parse(DateTime.Now.ToString("HH")) < 18)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower * (50 / 100);
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 18 && Int32.Parse(DateTime.Now.ToString("HH")) < 20)
                    {
                        foreach (var p in Panels)
                        {
                            p.CurrentPower += p.MaxPower * (20 / 100);
                        }
                    }

                    foreach (var p in Panels)
                    {
                        currentPowerPanels += p.CurrentPower;
                    }

                    SHES.solarPnaelsPower = currentPowerPanels;

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
