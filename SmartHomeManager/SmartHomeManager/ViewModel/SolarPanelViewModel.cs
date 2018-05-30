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
        public static ObservableCollection<SolarPanel> Panels { get; set; }
        private SolarPanel selectedPanel;
        private double currentPowerPanels;

        public SolarPanelViewModel()
        {
            Panels = new ObservableCollection<SolarPanel>();
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
            lock (Panels)
            {
                Panels.Add(new SolarPanel { Name = "Solar Panel A", MaxPower = 5.43, CurrentPower = 5.3 });
                Panels.Add(new SolarPanel { Name = "Solar Panel B", MaxPower = 3.14, CurrentPower = 4.34 });
            }
        }

        public void SendPowerToShes()
        {
            new Thread(() =>
            {
                while (!SHES.shutDown)
                {
                    currentPowerPanels = 0;

                    if (Int32.Parse(DateTime.Now.ToString("HH")) >= 20 || Int32.Parse(DateTime.Now.ToString("HH")) < 6)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0;
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 6 && Int32.Parse(DateTime.Now.ToString("HH")) < 8)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * (20 / 100);
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 8 && Int32.Parse(DateTime.Now.ToString("HH")) < 11)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * (50 / 100);
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 11 && Int32.Parse(DateTime.Now.ToString("HH")) < 16)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower;
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 16 && Int32.Parse(DateTime.Now.ToString("HH")) < 18)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * (50 / 100);
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 18 && Int32.Parse(DateTime.Now.ToString("HH")) < 20)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * (20 / 100);
                            }
                        }
                    }

                    lock (Panels)
                    {
                        foreach (var p in Panels)
                        {
                            currentPowerPanels += p.CurrentPower;
                        }
                    }

                    SHES.solarPnaelsPower = currentPowerPanels;

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
