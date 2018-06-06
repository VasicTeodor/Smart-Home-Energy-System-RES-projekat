using SmartHomeManager.Model;
using SmartHomeManager.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartHomeManager.ViewModel
{
    public class BatteryViewModel : BindableBase
    {
        public static ObservableCollection<Battery> Batteries { get; set; }
        private Battery selectedBattery;

        public BatteryViewModel()
        {
            Batteries = new ObservableCollection<Battery>();
            LoadBateries();
            SHES.batteryPower = Batteries[0].MaxPower;
        }

        public Battery SelectedBattery
        {
            get { return selectedBattery; }
            set
            {
                selectedBattery = value;
            }
        }

        private void LoadBateries()
        {
           lock (Batteries)
           {
                Batteries = SHES.importer.DeSerializeObject<ObservableCollection<Battery>>("../../ConfigFiles/BatteriesConfig.xml");//.Add(new Battery { Name = "Battery", MaxPower = 5.23, MaxCapacity = 10 });
           }
        }

        public void StartChraging()
        {
            Batteries[0].State = Enums.BatteryState.Charging;
            SHES.batteryState = Enums.BatteryState.Charging;

            if (Batteries[0].State == Enums.BatteryState.Charging && Batteries[0].Capacity < Batteries[0].MaxCapacity)
            {
                Batteries[0].CapacityMin++;

                if (Batteries[0].CapacityMin >= 60)
                {
                    Batteries[0].Capacity++;
                    Batteries[0].CapacityMin = 0;
                }

                SHES.batteryCapacity = Batteries[0].Capacity;
                SHES.batteryCapacityMin = Batteries[0].CapacityMin;

            }
            else
            {
                Batteries[0].State = Enums.BatteryState.Idlle;
                SHES.batteryState = Enums.BatteryState.Idlle;
            }

        }

        public void StartDischraging()
        {
            Batteries[0].State = Enums.BatteryState.Discharging;
            SHES.batteryState = Enums.BatteryState.Discharging;

            if (Batteries[0].State == Enums.BatteryState.Discharging && Batteries[0].Capacity > 0)
            {

                Batteries[0].CapacityMin--;

                if (Batteries[0].CapacityMin <= 0)
                {
                    Batteries[0].Capacity--;
                    Batteries[0].CapacityMin = 60;
                }

                SHES.batteryCapacity = Batteries[0].Capacity;
                SHES.batteryCapacityMin = Batteries[0].CapacityMin;
            }
            else
            {
                Batteries[0].State = Enums.BatteryState.Idlle;
                SHES.batteryState = Enums.BatteryState.Idlle;
            }
        }

        public void Idlle()
        {
            Batteries[0].State = Enums.BatteryState.Idlle;
            SHES.batteryState = Enums.BatteryState.Idlle;

            SHES.batteryCapacity = Batteries[0].Capacity;
            SHES.batteryCapacityMin = Batteries[0].CapacityMin;
        }


    }
}
