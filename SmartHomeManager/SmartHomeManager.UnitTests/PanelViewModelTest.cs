using System;
using NUnit.Framework;
using SmartHomeManager.Model;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class PanelViewModelTest
    {
        [Test]
        [TestCase("SolarPanel", 5, 3)]
        public void Panel_ConstructorTest(string name, double maxPower, double currentPower)
        {
            SolarPanel panel = new SolarPanel { Name = name, MaxPower = maxPower, CurrentPower = currentPower};

            Assert.AreEqual(panel.Name, name);
            Assert.AreEqual(panel.MaxPower, maxPower);
            Assert.AreEqual(panel.CurrentPower, currentPower);
        }
    }
}
