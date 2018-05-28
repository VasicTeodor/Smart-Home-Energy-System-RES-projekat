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
        private double capacity;
        private double capacityMin;
        private Enums.BatteryState state;
        public double MaxCapacity { get; set; }

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

        //public Enums.BatteryState State { get => state; set => state = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
