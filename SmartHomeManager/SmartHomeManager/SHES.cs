using SmartHomeManager.Model;
using SmartHomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeManager
{
    public class SHES
    {
        public static Enums.BatteryState batteryState;
        public static double solarPnaelsPower = 0;
        public static double batteryCapacity = 0;
        public static double batteryCapacityMin = 0;

        private BatteryViewModel battery;
        private UtilityViewModel utility;
        private SolarPanelViewModel solar;
        private ConsumersViewModel consumers;

        public SHES(BatteryViewModel batteryViewModel, UtilityViewModel utilityViewModel, SolarPanelViewModel solarPanelViewModel, ConsumersViewModel consumersViewModel)
        {
            battery = batteryViewModel;
            utility = utilityViewModel;
            solar = solarPanelViewModel;
            consumers = consumersViewModel;

            BatteryManagement();
        }

        private void BatteryManagement()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (Int32.Parse(DateTime.Now.ToString("HH")) >= 0 && Int32.Parse(DateTime.Now.ToString("HH")) <= 20)
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                             battery.StartChraging();
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 11 && Int32.Parse(DateTime.Now.ToString("HH")) <= 12)
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                            battery.StartDischraging();
                        }
                    }
                    else
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                            battery.Idlle();
                        }
                    }

                    Thread.Sleep(1000);
                }

            }).Start();
        }
    }
}
