using System;
using NUnit.Framework;
using SmartHomeManager.Model;
using SmartHomeManager.ViewModel;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class UtilityViewModelTest
    {
        [Test]
        [TestCase(2)]
        public void CalculatePrice_Test(double value)
        {
            UtilityViewModel utility = new UtilityViewModel();
            Utility mock = new Utility { PayingPrice = 2, Power = 0, Price = 2 };
            UtilityViewModel.Utilities = new System.Collections.ObjectModel.ObservableCollection<Utility>();
            UtilityViewModel.Utilities.Add(mock);

            var retVal = utility.CalculatePrice(value);

            Assert.AreEqual(retVal, -4);
        }

        [Test]
        [TestCase("MikaPrase",5,5,5,Enums.BatteryState.Charging)]
        public void Battery_ConstructorTest(string id, double power, double capacity, double capMin, Enums.BatteryState state)
        {
            Battery baterija = new Battery { Name = id, Capacity = capacity, CapacityMin = capMin, MaxPower = power, State = state };

            Assert.AreEqual(baterija.Name, id);
        }
    }
}
