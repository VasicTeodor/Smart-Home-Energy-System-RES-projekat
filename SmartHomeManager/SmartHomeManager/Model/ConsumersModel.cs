using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Model
{
    public class ConsumersModel
    {
    }

    public class Consumers : INotifyPropertyChanged
    {
        private int id;
        private double consumption;
        private string name;
        private string image;
        private bool working;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Image
        {
            get { return image; }
            set
            {
                if(image != value)
                {
                    image = value;
                    RaisePropertyChanged("Image");
                }
            }
        }

        public bool Working
        {
            get { return working; }
            set
            {
                if (working != value)
                {
                    working = value;
                    RaisePropertyChanged("Working");
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

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }

        public double Consumption
        {
            get { return consumption; }
            set
            {
                if (consumption != value)
                {
                    consumption = value;
                    RaisePropertyChanged("Consumption");
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
