using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    public class ConsumersViewModel : BindableBase
    {
        public static ObservableCollection<Consumers> Consumers { get; set; }
        private Consumers slectedConsumer;

        public ConsumersViewModel()
        {
            Consumers = new ObservableCollection<Consumers>();
            //LoadConsumers();
            Consumers = SHES.devicesList;
        }

        public Consumers SelectedConsumer
        {
            get { return slectedConsumer; }
            set
            {
                slectedConsumer = value;
            }
        }

        public void LoadConsumers()
        {
            lock (Consumers)
            {
                Consumers.Add(new Consumers { Name = "Television", Consumption = 5.43, Id = 1, Image = "" });
                Consumers.Add(new Consumers { Name = "Fridge", Consumption = 3.14, Id = 2, Image = "" });
            }
        }
    }
}
