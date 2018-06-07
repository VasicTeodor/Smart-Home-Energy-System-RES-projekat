using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using NUnit.Framework;
using SmartHomeManager.Model;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class DataIOTest
    {
        [Test]
        [TestCase("UtilityLog.xml", "Utility", "6/6/2018")]
        [TestCase("BatteryLog.xml", "Battery", "6/6/2018")]
        [TestCase("ConsumptionLog.xml", "Consumption", "6/6/2018")]
        [TestCase("SolarPanelsLog.xml", "SolarPanel", "6/6/2018")]
        [TestCase("PriceLog.xml", "Price", "6/6/2018")]
        public void ReadLog_GoodParameters_Test(string fileName, string item, string selectedDate)
        {
            List<KeyValuePair<int, double>> retVal = new List<KeyValuePair<int, double>>();
            DataIO importer = new DataIO();
            retVal = importer.ReadLog(fileName, item, selectedDate);

            Assert.IsNotNull(retVal);
            Assert.IsInstanceOf(typeof(List<KeyValuePair<int, double>>), retVal);
           
        }

        [Test]
        [TestCase("NotExist", "item", "6/6/2018")]
        public void ReadLog_NotExist_Test(string fileName, string item, string selectedDate)
        {
            List<KeyValuePair<int, double>> retVal = new List<KeyValuePair<int, double>>();
            DataIO importer = new DataIO();
            retVal = importer.ReadLog(fileName, item, selectedDate);
            
            Assert.IsTrue(retVal.Count == 0);
        }

        [Test]
        [TestCase("SolarPanelsLog.xml", "SolarPanel", 5.14)]
        [TestCase("UtilityLog.xml", "Utility", 5)]
        [TestCase("ConsumptionLog.xml", "Consumption", 3.41)]
        [TestCase("BatteryLog.xml", "Battery", -2)]
        [TestCase("PriceLog.xml", "Price", 4.22)]
        public void LogService_Test(string fileName, string item, double input)
        {
            DataIO importer = new DataIO();
            importer.logService(fileName, item, input);

            Assert.IsTrue(File.Exists(fileName));
        }

        [Test]
        [TestCase("../../ConfigFiles/ConsumersConfig.xml")]
        public void DeSerializeObject_Test(string fileName)
        {
            DataIO importer = new DataIO();
            ObservableCollection<Consumers> retVal = new ObservableCollection<Consumers>();

            retVal = importer.DeSerializeObject<ObservableCollection<Consumers>>(fileName);

            if (string.IsNullOrEmpty(fileName))
            {
                Assert.IsTrue(retVal == default(ObservableCollection<Consumers>));
            }
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void DeSerializeObject_EmptyOrNull_Test(string fileName)
        {
            DataIO importer = new DataIO();
            ObservableCollection<Consumers> retVal = new ObservableCollection<Consumers>();

            retVal = importer.DeSerializeObject<ObservableCollection<Consumers>>(fileName);

            Assert.IsTrue(retVal == default(ObservableCollection<Consumers>));
           
        }

        //public T DeSerializeObject<T>(string fileName)


    } 
}
