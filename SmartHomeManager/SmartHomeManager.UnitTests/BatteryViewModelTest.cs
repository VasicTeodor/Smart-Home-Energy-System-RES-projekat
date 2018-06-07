using System;
using NUnit.Framework;
using SmartHomeManager.Model;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class BatteryViewModelTest
    {
        [Test]
        [TestCase("Battey", 5, 1, 4, Enums.BatteryState.Charging)]
        public void Battery_ConstructorTest(string id, double power, double capacity, double capMin, Enums.BatteryState state)
        {
            Battery battery = new Battery { Name = id, Capacity = capacity, CapacityMin = capMin, MaxPower = power, State = state };

            Assert.AreEqual(battery.Name, id);
            Assert.AreEqual(battery.MaxPower, power);
            Assert.AreEqual(battery.Capacity, capacity);
            Assert.AreEqual(battery.CapacityMin, capMin);
            Assert.AreEqual(battery.State, state);
        }
    }
}
