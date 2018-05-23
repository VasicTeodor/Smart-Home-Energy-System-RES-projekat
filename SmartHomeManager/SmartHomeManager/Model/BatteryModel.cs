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
