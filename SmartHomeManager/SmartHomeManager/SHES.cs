using SmartHomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager
{
    public class SHES
    {
        public static bool bateryCharging = false;
        public static double solarPnaelsPower = 0;

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
        }
    }
}
