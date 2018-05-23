using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            ObservableCollection<Battery> bateries = new ObservableCollection<Battery>();

            bateries.Add(new Battery { Name = "Battery A", MaxPower = 5.43, Capacity = 50 });
            bateries.Add(new Battery { Name = "Battery B", MaxPower = 3.14, Capacity = 100 });

            Batteries = bateries;
        }

        public double StartChraging()
        {
            SHES.bateryCharging = true;
            return Batteries[0].Capacity;
        }
    }
}
