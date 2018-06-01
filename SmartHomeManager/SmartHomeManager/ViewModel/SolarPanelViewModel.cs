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
                Panels = SHES.importer.DeSerializeObject<ObservableCollection<SolarPanel>>("../../ConfigFiles/SolarPanelsConfig.xml");
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
                                p.CurrentPower = p.MaxPower * 0.2;
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 8 && Int32.Parse(DateTime.Now.ToString("HH")) < 11)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.5;
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
                                p.CurrentPower = p.MaxPower * 0.5;
                            }
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 18 && Int32.Parse(DateTime.Now.ToString("HH")) < 20)
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.2;
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
