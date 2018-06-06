using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Model
{
    public class Enums
    {
        public enum BatteryState : int
        {
            Idlle,
            Charging,
            Discharging
        }

        public enum PanelState : int
        {
            Producing,
            Idlle
        }
    }
}
