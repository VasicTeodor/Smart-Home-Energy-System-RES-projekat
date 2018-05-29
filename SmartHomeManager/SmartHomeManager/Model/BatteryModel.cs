using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Model
{
    public class BatteryModel
    {
    }

    public class Battery : INotifyPropertyChanged
    {
        private string name;
        private double maxPower;
        public static double capacity = SHES.batteryCapacity;
        public static double capacityMin = SHES.batteryCapacityMin;
        private Enums.BatteryState state = SHES.batteryState;
        public double MaxCapacity { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        public Enums.BatteryState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    RaisePropertyChanged("State");
                }
            }
        }

        public double Capacity
        {
            get { return capacity; }
            set
            {
                if (capacity != value)
                {
                    capacity = value;
                    RaisePropertyChanged("Capacity");
                }
            }
        }

        public double CapacityMin
        {
            get { return capacityMin; }
            set
            {
                if (capacityMin != value)
                {
                    capacityMin = value;
                    RaisePropertyChanged("CapacityMin");
                }
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public double MaxPower
        {
            get { return maxPower; }
            set
            {
                if (maxPower != value)
                {
                    maxPower = value;
                    RaisePropertyChanged("MaxPower");
                }
            }
        }

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
