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
        public static MyICommand TurnOnCommand { get; set; }
        public static MyICommand TurnOffCommand { get; set; }
        public static MyICommand RemoveCommand { get; set; }
        private Consumers slectedConsumer;

        public ConsumersViewModel()
        {
            Consumers = new ObservableCollection<Consumers>();
            TurnOnCommand = new MyICommand(OnTurnOn, CanTurnOn);
            TurnOffCommand = new MyICommand(OnTurnOff, CanTurnOff);
            RemoveCommand = new MyICommand(OnRemove, CanRemove);
            Consumers = SHES.devicesList;
        }

        public Consumers SelectedConsumer
        {
            get { return slectedConsumer; }
            set
            {
                slectedConsumer = value;
                TurnOffCommand.RaiseCanExecuteChanged();
                TurnOnCommand.RaiseCanExecuteChanged();
                RemoveCommand.RaiseCanExecuteChanged();
            }
        }

        private bool CanTurnOn()
        {
            return (SelectedConsumer != null && SelectedConsumer.Working == false);
        }

        private bool CanTurnOff()
        {
            return (SelectedConsumer != null && SelectedConsumer.Working == true);
        }

        private bool CanRemove()
        {
            return SelectedConsumer != null;
        }

        private void OnRemove()
        {
            var remove = SelectedConsumer;
            Consumers.Remove(remove);
            SHES.devicesList.Remove(remove);
            //SHES.importer.SerializeObject(Consumers );  DODATI PISANJE U CONFIG FAJL NOVIH UREDJAJA
        }

        private void OnTurnOff()
        {
            int id1 = Consumers.IndexOf(SelectedConsumer);
            int id2 = SHES.devicesList.IndexOf(SelectedConsumer);
            SHES.devicesList[id2].Working = false;
            Consumers[id1].Working = false;
        }

        private void OnTurnOn()
        {
            int id1 = Consumers.IndexOf(SelectedConsumer);
            int id2 = SHES.devicesList.IndexOf(SelectedConsumer);
            SHES.devicesList[id2].Working = true;
            Consumers[id1].Working = true;
        }
    }
}
