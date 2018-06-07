using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Model
{
    public class UtilityModel
    {
    }
    public class Utility : INotifyPropertyChanged
    {
        private double power;
        private static double payingPrice = SHES.price;
        private double price;

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

        public double PayingPrice
        {
            get { return payingPrice; }
            set
            {
                if (payingPrice != value)
                {
                    payingPrice = value;
                    RaisePropertyChanged("PayingPrice");
                }
            }
        }

        public double Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    RaisePropertyChanged("Price");
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
