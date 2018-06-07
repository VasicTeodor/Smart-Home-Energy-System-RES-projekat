using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using SmartHomeManager.Model;
using SmartHomeManager.ViewModel;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class BatteryViewModelTest
    {
        [Test]
        [TestCase("Battey", 5, 1, 4, 6, Enums.BatteryState.Charging)]
        public void Battery_ConstructorTest(string id, double power, double capacity, double capMin, double maxCap, Enums.BatteryState state)
        {
            Battery battery = new Battery { Name = id, Capacity = capacity, CapacityMin = capMin, MaxPower = power, State = state, MaxCapacity = maxCap };

            Assert.AreEqual(battery.Name, id);
            Assert.AreEqual(battery.MaxPower, power);
            Assert.AreEqual(battery.Capacity, capacity);
            Assert.AreEqual(battery.CapacityMin, capMin);
            Assert.AreEqual(battery.MaxCapacity, maxCap);
            Assert.AreEqual(battery.State, state);
        }

    }
}
