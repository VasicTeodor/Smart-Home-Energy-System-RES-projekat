using SmartHomeManager.Model;
using SmartHomeManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHomeManager
{
    public class SHES
    {
        public static DataIO importer = new DataIO();
        public static Enums.BatteryState batteryState;
        public static double batteryPower = 0;
        public static double solarPnaelsPower = 0;
        public static double batteryCapacity = 0;
        public static double batteryCapacityMin = 0;
        public static bool shutDown = false;
        private double wholeConsumption = 0;
        private double importExportPower = 0;
        public static ObservableCollection<Consumers> devicesList = new ObservableCollection<Consumers>();

        private BatteryViewModel battery;
        private UtilityViewModel utility;
        private SolarPanelViewModel solar;
        private ConsumersViewModel consumers;

        public SHES(BatteryViewModel batteryViewModel, UtilityViewModel utilityViewModel, SolarPanelViewModel solarPanelViewModel, ConsumersViewModel consumersViewModel)
        {
            battery = batteryViewModel;
            utility = utilityViewModel;
            solar = solarPanelViewModel;
            consumers = consumersViewModel;

            LoadDevices();

            BatteryManagement();
            createListener();
        }

        private void BatteryManagement()
        {
            new Thread(() =>
            {
                while (!shutDown)
                {
                    if (Int32.Parse(DateTime.Now.ToString("HH")) >= 0 && Int32.Parse(DateTime.Now.ToString("HH")) <= 20)
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                             battery.StartChraging();
                        }
                    }
                    else if (Int32.Parse(DateTime.Now.ToString("HH")) >= 11 && Int32.Parse(DateTime.Now.ToString("HH")) <= 12)
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                            battery.StartDischraging();
                        }
                    }
                    else
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                            battery.Idlle();
                        }
                    }

                    Thread.Sleep(1000);
                }

            }).Start();
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(ConsumersViewModel.Consumers.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Objekat_1:272"

                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji
                            int deviceId = Int32.Parse((incomming.Split('_', ':'))[1]);
                            double consumption = double.Parse((incomming.Split('_', ':'))[2]);
                            
                            devicesList[deviceId].Consumption = consumption;

                            try
                            {
                                lock (ConsumersViewModel.Consumers)
                                {
                                    ConsumersViewModel.Consumers[deviceId].Consumption = consumption;
                                }
                            }
                            catch { }

                            foreach(var cons in devicesList)
                            {
                                wholeConsumption += cons.Consumption;
                            }

                            if (batteryState == Enums.BatteryState.CHARGING)
                            {
                                importExportPower = consumption - solarPnaelsPower + batteryPower;
                            }
                            else
                            {
                                importExportPower = consumption - solarPnaelsPower - batteryPower;
                            }

                            LogService("ConsumptionLog.txt", "Consumption", consumption);

                            if(batteryState == Enums.BatteryState.CHARGING)
                            {
                                LogService("BatteryLog.txt","Battery", 0 - batteryPower);
                            }
                            else
                            {
                                LogService("BatteryLog.txt", "Battery", batteryPower);
                            }

                            LogService("SolarPanelsLog.txt", "SolarPanel", solarPnaelsPower);

                            LogService("UtilityLog.txt", "Utility", importExportPower);
                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        private void LoadDevices()
        {
            devicesList = importer.DeSerializeObject<ObservableCollection<Consumers>>("../../ConfigFiles/ConsumersConfig.xml");
        }

        private void LogService(string fileName, string item, double input)
        {
            string logTxt = $"{item} - Value: {input} TimeStamp: {DateTime.Now.ToString()}";
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, true))
            {
                file.WriteLine(logTxt);
            }
        }
    }
}
