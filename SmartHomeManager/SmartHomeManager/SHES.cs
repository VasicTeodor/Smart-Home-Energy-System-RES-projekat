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
        public static Enums.PanelState panelState;
        public static double batteryPower = 0;
        public static double solarPnaelsPower = 0;
        public static double batteryCapacity = 0;
        public static double batteryCapacityMin = 0;
        public static double consumersConsumption = 0;
        public static double priceToPay = 0;
        public static double utilityPower = 0;
        public static double price = 7.3;
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
            MakeLog();
        }

        private void BatteryManagement()
        {
            new Thread(() =>
            {
                while (!shutDown)
                {
                    if ((Int32.Parse(DateTime.Now.ToString("mm")) >= 7 && Int32.Parse(DateTime.Now.ToString("mm")) <= 15))// ovo je konverzija za minute, prave vrednosti su 3 i 6
                    {
                        lock (BatteryViewModel.Batteries)
                        {
                             battery.StartChraging();
                        }
                    }
                    else if ((Int32.Parse(DateTime.Now.ToString("mm")) >= 35 && Int32.Parse(DateTime.Now.ToString("mm")) <= 42))// ovo je konverzija za minute, prave vrednosti su 14 i 17
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
                while (!shutDown)
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
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes((ConsumersViewModel.Consumers.Count).ToString());
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

                            if (deviceId != ConsumersViewModel.Consumers.Count)
                            {


                                if (devicesList[deviceId].Working)
                                {
                                    devicesList[deviceId].Consumption = consumption;
                                }
                                else
                                {
                                    devicesList[deviceId].Consumption = 0;
                                }

                                try
                                {
                                    lock (ConsumersViewModel.Consumers)
                                    {
                                        if (ConsumersViewModel.Consumers[deviceId].Working)
                                        {
                                            ConsumersViewModel.Consumers[deviceId].Consumption = consumption;
                                        }
                                        else
                                        {
                                            ConsumersViewModel.Consumers[deviceId].Consumption = 0;
                                        }
                                    }
                                }
                                catch { }

                                wholeConsumption = 0;

                                foreach (var cons in devicesList)
                                {
                                    wholeConsumption += cons.Consumption;
                                }

                                consumersConsumption = wholeConsumption;

                                if (batteryState == Enums.BatteryState.Charging)
                                {
                                    importExportPower = wholeConsumption - solarPnaelsPower + batteryPower;
                                }
                                else if (batteryState == Enums.BatteryState.Discharging)
                                {
                                    importExportPower = wholeConsumption - solarPnaelsPower - batteryPower;
                                }
                                else
                                {
                                    importExportPower = wholeConsumption - solarPnaelsPower;
                                }


                                utilityPower = importExportPower;

                                lock (UtilityViewModel.Utilities)
                                {
                                    priceToPay = utility.CalculatePrice(importExportPower);
                                }
                            }
                            else
                            {
                                if (deviceId == ConsumersViewModel.Consumers.Count)
                                {
                                    lock (UtilityViewModel.Utilities)
                                    {
                                        UtilityViewModel.Utilities[0].PayingPrice = consumption;
                                    }
                                    price = consumption;
                                }
                            }
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
            foreach(var device in devicesList)
            {
                device.Working = false;
            }
        }

        private void MakeLog()
        {
            new Thread(() =>
            {
                while (!shutDown)
                {
                    importer.logService("SolarPanelsLog.xml", "SolarPanel", Math.Round(solarPnaelsPower, 2));

                    importer.logService("UtilityLog.xml", "Utility", importExportPower);

                    importer.logService("ConsumptionLog.xml", "Consumption", wholeConsumption);

                    importer.logService("PriceLog.xml", "Price", priceToPay);

                    if (batteryState == Enums.BatteryState.Charging)
                    {
                        importer.logService("BatteryLog.xml", "Battery", 0 - batteryPower);

                    }
                    else if (batteryState == Enums.BatteryState.Discharging)
                    {
                        importer.logService("BatteryLog.xml", "Battery", batteryPower);
                    }
                    else
                    {
                        importer.logService("BatteryLog.xml", "Battery", 0);
                    }

                    Thread.Sleep(150000);
                }
            }).Start();
        }
    }
}
