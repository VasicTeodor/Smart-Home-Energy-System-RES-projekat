using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Model
{
    public class SolarPanelModel
    {
    }

    public class SolarPanel : INotifyPropertyChanged
    {
        private string name;
        private double power;

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

        public double Power
        {
            get { return power; }
            set
            {
                if (power != value)
                {
                    power = value;
                    RaisePropertyChanged("Power");
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
