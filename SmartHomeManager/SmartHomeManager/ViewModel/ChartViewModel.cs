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
            string line;
            using (System.IO.StreamReader file = new System.IO.StreamReader("UtilityLog.txt"))
            {
                while((line = file.ReadLine()) != null)
                {
                    string date = line.Split(':')[2].Trim().Split(' ')[0];
                    DateTime myDate = DateTime.Parse(date);
                    if (!Lista.Contains(myDate))
                    {
                        Lista.Add(myDate);
                    }
                }
            }
        }
    }
}
