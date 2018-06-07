using System;
using NUnit.Framework;
using SmartHomeManager.Model;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class ConsumersViewModelTest
    {
        [Test]
        [TestCase(1, 3.23, "Consumer", false)]
        public void Consumers_ConstructorTest(int id, double consumption, string name, bool working)
        {
            Consumers consumer = new Consumers { Id = id, Consumption = consumption, Name = name, Working = working };

            Assert.AreEqual(consumer.Id, id);
            Assert.AreEqual(consumer.Consumption, consumption);
            Assert.AreEqual(consumer.Name, name);
            Assert.AreEqual(consumer.Working, working);
        }
        
    }
}
