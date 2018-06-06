using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.ViewModel
{
    class ChartViewModel : BindableBase
    {
        public static ObservableCollection<DateTime> Lista = new ObservableCollection<DateTime>();

        public ChartViewModel()
        {
        }
    }
}
