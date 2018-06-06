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

        private void LoadPanels()
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

                    if ((Int32.Parse(DateTime.Now.ToString("mm")) >= 50 || Int32.Parse(DateTime.Now.ToString("mm")) < 15))//20 i 6h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0;
                            }
                            SHES.panelState = Enums.PanelState.Idlle;
                        }
                    }
                    else if ((Int32.Parse(DateTime.Now.ToString("mm")) >= 15 && Int32.Parse(DateTime.Now.ToString("mm")) < 20))//6 i 8h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.2;
                            }
                            SHES.panelState = Enums.PanelState.Producing;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 20 && Int32.Parse(DateTime.Now.ToString("mm")) < 27)//8 i 11h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.5;
                            }
                            SHES.panelState = Enums.PanelState.Producing;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 27 && Int32.Parse(DateTime.Now.ToString("mm")) < 40)//11 i 16h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower;
                            }
                            SHES.panelState = Enums.PanelState.Producing;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 40 && Int32.Parse(DateTime.Now.ToString("mm")) < 45)//16 i 18h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.5;
                            }
                            SHES.panelState = Enums.PanelState.Producing;
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("mm")) >= 45 && Int32.Parse(DateTime.Now.ToString("mm")) < 50)//18 i 20h
                    {
                        lock (Panels)
                        {
                            foreach (var p in Panels)
                            {
                                p.CurrentPower = p.MaxPower * 0.2;
                            }
                            SHES.panelState = Enums.PanelState.Producing;
                        }
                    }

                    lock (Panels)
                    {
                        foreach (var p in Panels)
                        {
                            currentPowerPanels += p.CurrentPower;
                        }
                    }

                    SHES.solarPnaelsPower = Math.Round(currentPowerPanels/1000, 2);

                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
