using NUnit.Framework;
using System;
using System.Threading;

namespace SmartHomeManager.UnitTests
{
    [TestFixture]
    public class AddConsumerWindowTest
    {
        [Test]
        public void AddConsumerWindow_Test()
        {
            AutoResetEvent are = new AutoResetEvent(false);
            AddConsumerWindow addWindow = null;

            ThreadExecutor.RunCodeAsSTA(are, delegate
            {
                addWindow = new AddConsumerWindow();
            });

            are.WaitOne();
            Assert.IsInstanceOf(typeof(AddConsumerWindow), addWindow);
        }
    }
    public static class ThreadExecutor
    {
        public static void RunCodeAsSTA(
            AutoResetEvent are,
            ThreadStart originalDelegate)
        {
            Thread thread = new Thread(
                delegate ()
                {
                    try
                    {
                        originalDelegate.Invoke();
                        are.Set();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(String.Format("An error occurred {0}", e.Message));
                    }
                });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}
