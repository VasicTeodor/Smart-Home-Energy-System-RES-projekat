using SmartHomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager
{
    public class MainWindowViewModel : BindableBase
    {
        //vrednosti sa servera primacemo u view modelima!
        public MyICommand<string> NavCommand { get; private set; }
        private SolarPanelViewModel solarPanelViewModel = new SolarPanelViewModel();
        private HomeViewModel homeVIewModel = new HomeViewModel();
        private BatteryViewModel batteryViewModel = new BatteryViewModel();
        private UtilityViewModel utilityViewModel = new UtilityViewModel();
        private ConsumersViewModel consumerViewModel = new ConsumersViewModel();
        private BindableBase currentViewModel;

        public MainWindowViewModel()
        {
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = homeVIewModel;
        }

        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "home":
                    CurrentViewModel = homeVIewModel;
                    break;
                case "solarPanel":
                    CurrentViewModel = solarPanelViewModel;
                    break;
                case "battery":
                    CurrentViewModel = batteryViewModel;
                    break;
                case "utility":
                    CurrentViewModel = utilityViewModel;
                    break;
                case "consumers":
                    CurrentViewModel = consumerViewModel;
                    break;
            }
        }
    }
}
