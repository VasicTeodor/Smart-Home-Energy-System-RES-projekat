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
    public class BatteryViewModel : BindableBase
    {
        public ObservableCollection<Battery> Batteries { get; set; }
        private Battery selectedBattery;

        public BatteryViewModel()
        {
            LoadBateries();
            //SendDataToShes();
        }

        public Battery SelectedBattery
        {
            get { return selectedBattery; }
            set
            {
                selectedBattery = value;
            }
        }

        public void LoadBateries()
        {
            ObservableCollection<Battery> batteries = new ObservableCollection<Battery>();

            batteries.Add(new Battery { Name = "Battery", MaxPower = 5.23, Capacity = 4, CapacityMin = 40, MaxCapacity = 10, State = Enums.BatteryState.IDLLE });

            Batteries = batteries;
        }

        private void SendDataToShes()
        {
            new Thread(() =>
            {
                while (true)
                {
                    if (Batteries[0].State == Enums.BatteryState.CHARGING && Batteries[0].Capacity < Batteries[0].MaxCapacity)
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
                    else if (Batteries[0].State == Enums.BatteryState.DISCHARGING && Batteries[0].Capacity > 0)
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
                    else if (Batteries[0].State == Enums.BatteryState.IDLLE)
                    {
                        SHES.batteryCapacity = Batteries[0].Capacity;
                        SHES.batteryCapacityMin = Batteries[0].CapacityMin;
                    }


                    Thread.Sleep(1000);
                }

            }).Start();
        }

        public void StartChraging()
        {
            Batteries[0].State = Enums.BatteryState.CHARGING;
            SHES.batteryState = Enums.BatteryState.CHARGING;

            Batteries[0].CapacityMin++;

            if (Batteries[0].CapacityMin >= 60)
            {
                Batteries[0].Capacity++;
                Batteries[0].CapacityMin = 0;
            }

            SHES.batteryCapacity = Batteries[0].Capacity;
            SHES.batteryCapacityMin = Batteries[0].CapacityMin;
        }

        public void StartDischraging()
        {
            Batteries[0].State = Enums.BatteryState.DISCHARGING;
            SHES.batteryState = Enums.BatteryState.DISCHARGING;

            Batteries[0].CapacityMin = Batteries[0].CapacityMin - 1;

            if (Batteries[0].CapacityMin <= 0)
            {
                Batteries[0].Capacity--;
                Batteries[0].CapacityMin = 60;
            }

            SHES.batteryCapacity = Batteries[0].Capacity;
            SHES.batteryCapacityMin = Batteries[0].CapacityMin;
        }

        public void Idlle()
        {
            Batteries[0].State = Enums.BatteryState.IDLLE;
            SHES.batteryState = Enums.BatteryState.IDLLE;

            SHES.batteryCapacity = Batteries[0].Capacity;
            SHES.batteryCapacityMin = Batteries[0].CapacityMin;
        }


    }
}
