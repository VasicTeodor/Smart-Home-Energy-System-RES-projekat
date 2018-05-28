using SmartHomeManager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    public class UtilityViewModel : BindableBase
    {
        public ObservableCollection<Utility> Utilities { get; set; }
        private Utility utility;

        public UtilityViewModel()
        {
            LoadUtilities();
        }

        public Utility Utility
        {
            get { return utility; }
            set
            {
                utility = value;
            }
        }

        public void LoadUtilities()
        {
            ObservableCollection<Utility> utilities = new ObservableCollection<Utility>();

            utilities.Add(new Utility { Power = 2341.42, Price = 100.10  });

            Utilities = utilities;
        }
    }
}
