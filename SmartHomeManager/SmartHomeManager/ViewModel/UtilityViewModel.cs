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
        public static ObservableCollection<Utility> Utilities { get; set; }
        private Utility utility;

        public UtilityViewModel()
        {
            Utilities = new ObservableCollection<Utility>();
            LoadUtilities();
            //Utilities[0].PayingPrice = SHES.price;
        }

        public Utility Utility
        {
            get { return utility; }
            set
            {
                utility = value;
            }
        }

        private void LoadUtilities()
        {
            lock(Utilities)
            {
                Utilities = SHES.importer.DeSerializeObject<ObservableCollection<Utility>>("../../ConfigFiles/UtilityConfig.xml");
            }
        }

        public double CalculatePrice( double importExportPower)
        {
            double retVal;

            Utilities[0].Power = importExportPower;

            retVal = Utilities[0].PayingPrice * importExportPower;

            if(retVal < 0)
            {
                retVal = 0 - retVal;
            }
            else
            {
                retVal = 0 - retVal;
            }

            Utilities[0].Price = retVal;

            return retVal;
        }
    }
}
