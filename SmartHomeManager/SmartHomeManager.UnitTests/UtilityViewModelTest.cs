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
        [TestCase(3.5)]
        [TestCase(-4)]
        [TestCase(-3.54)]
        [TestCase(0)]
        public void CalculatePrice_GoodParameters_Test(double value)
        {
            UtilityViewModel utility = new UtilityViewModel();
            Utility u = new Utility { PayingPrice = 2, Power = 0, Price = 0 };
            UtilityViewModel.Utilities = new System.Collections.ObjectModel.ObservableCollection<Utility>();
            UtilityViewModel.Utilities.Add(u);

            var retVal = utility.CalculatePrice(value);

            Assert.AreEqual(retVal, - value * u.PayingPrice);
        }

        [Test]
        [TestCase(2)]
        public void CalculatePrice_SetPower_Test(double value)
        {
            UtilityViewModel utility = new UtilityViewModel();
            Utility u = new Utility { PayingPrice = 2, Power = 0, Price = 0 };
            UtilityViewModel.Utilities = new System.Collections.ObjectModel.ObservableCollection<Utility>();
            UtilityViewModel.Utilities.Add(u);

            utility.CalculatePrice(value);
            Assert.AreEqual(value, u.Power);
        }

        [Test]
        [TestCase(5.43, 4, 2)]
        public void Utility_ConstructorTest(double power, double payingPrice, double price)
        {
            Utility utility = new Utility { Power = power, PayingPrice = payingPrice, Price = price};

            Assert.AreEqual(utility.Power, power);
            Assert.AreEqual(utility.PayingPrice, payingPrice);
            Assert.AreEqual(utility.Price, price);
        }
    }
}
