using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    public class ConsumersViewModel : BindableBase
    {
        public ObservableCollection<Consumers> Consumers { get; set; }
        private Consumers slectedConsumer;

        public ConsumersViewModel()
        {
            LoadConsumers();
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
            ObservableCollection<Consumers> consumers = new ObservableCollection<Consumers>();

            consumers.Add(new Consumers { Name = "Television", Consumption = 5.43, Id = 1, Image = "" });
            consumers.Add(new Consumers { Name = "Fridge", Consumption = 3.14, Id = 2, Image = "" });

            Consumers = consumers;
        }
    }
}
