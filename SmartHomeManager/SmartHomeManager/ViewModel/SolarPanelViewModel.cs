using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    public class SolarPanelViewModel : BindableBase
    {
        public ObservableCollection<SolarPanel> Panels { get; set; }
        private SolarPanel selectedPanel;

        public SolarPanelViewModel()
        {
            LoadPanels();
        }

        public SolarPanel SelectedPanel 
        {
            get { return selectedPanel; }
            set
            {
                selectedPanel = value;
            }
        }

        public void LoadPanels()
        {
            ObservableCollection<SolarPanel> panels = new ObservableCollection<SolarPanel>();

            panels.Add(new SolarPanel { Name = "Panel A", Power = 5.43 });
            panels.Add(new SolarPanel { Name = "Panel B", Power = 3.14 });

            Panels = panels;
        }
    }
}
