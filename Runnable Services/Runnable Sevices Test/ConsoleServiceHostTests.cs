using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Runnable_Services;
using Microsoft.QualityTools.Testing.Fakes;

namespace Runnable_Sevices_Test
{
    [TestClass]
    public class ConsoleServiceHostTests
    {
        [TestMethod]
        public void TestStartOnlyGetsCalledOnce()
        {
            using (ShimsContext.Create())
            {
                int startIterations = 0;

                var service = new System.ServiceProcess.Fakes.ShimServiceBase
                {
                    OnStartStringArray = args =>
                    {
                        startIterations++;
                    }
                };

                var host = new ConsoleServiceHost(service);

                Assert.AreEqual(0, startIterations);
                host.StartHostedService();
                Assert.AreEqual(1, startIterations);
                host.StartHostedService();
                Assert.AreEqual(1, startIterations);
                host.StartHostedService();
                Assert.AreEqual(1, startIterations);
                host.StartHostedService();
                Assert.AreEqual(1, startIterations);
                host.StartHostedService();
                Assert.AreEqual(1, startIterations);
            }
        }

        [TestMethod]
        public void TestStopOnlyGetsCalledOnce()
        {
            using (ShimsContext.Create())
            {
                int stopIterations = 0;

                var service = new System.ServiceProcess.Fakes.ShimServiceBase
                {
                    OnStop = () =>
                    {
                        stopIterations++;
                    }
                };

                var host = new ConsoleServiceHost(service);

                Assert.AreEqual(0, stopIterations);
                host.StartHostedService();
                Assert.AreEqual(0, stopIterations);

                host.StopHostedService();
                Assert.AreEqual(1, stopIterations);
                host.StopHostedService();
                Assert.AreEqual(1, stopIterations);
                host.StopHostedService();
                Assert.AreEqual(1, stopIterations);
                host.StopHostedService();
                Assert.AreEqual(1, stopIterations);
            }
        }

        [TestMethod]
        public void TestServiceStartsWhenExpected()
        {
            using (ShimsContext.Create())
            {
                bool startCodeRan = false;

                var service = new System.ServiceProcess.Fakes.ShimServiceBase
                {
                    OnStartStringArray = (args) =>
                    {
                        startCodeRan = true;
                    }
                };

                var host = new ConsoleServiceHost(service);

                Assert.IsFalse(startCodeRan);

                host.StopHostedService();
                Assert.IsFalse(startCodeRan);

                host.StartHostedService();
                Assert.IsTrue(startCodeRan);
            }
        }

        [TestMethod]
        public void TestServiceStopsWhenExpected()
        {
            using (ShimsContext.Create())
            {
                bool stopCodeRan = false;

                var service = new System.ServiceProcess.Fakes.ShimServiceBase
                {
                    OnStop = () =>
                    {
                        stopCodeRan = true;
                    }
                };

                var host = new ConsoleServiceHost(service);

                Assert.IsFalse(stopCodeRan);

                // not running yet so shouldn't stop
                host.StopHostedService();
                Assert.IsFalse(stopCodeRan);

                host.StartHostedService();
                Assert.IsFalse(stopCodeRan);

                host.StopHostedService();
                Assert.IsTrue(stopCodeRan);
            }
        }

        [TestMethod]
        public void TestServiceIsServiceUpWorks()
        {
            using (ShimsContext.Create())
            {
                var service = new System.ServiceProcess.Fakes.ShimServiceBase
                {

                };

                var host = new ConsoleServiceHost(service);

                Assert.IsFalse(host.IsServiceUp);

                host.StopHostedService();
                Assert.IsFalse(host.IsServiceUp);
                host.StopHostedService();
                Assert.IsFalse(host.IsServiceUp);

                host.StartHostedService();
                Assert.IsTrue(host.IsServiceUp);

                host.StartHostedService();
                Assert.IsTrue(host.IsServiceUp);
                host.StartHostedService();
                Assert.IsTrue(host.IsServiceUp);
                host.StartHostedService();
                Assert.IsTrue(host.IsServiceUp);

                host.StopHostedService();
                Assert.IsFalse(host.IsServiceUp);
                host.StopHostedService();
                Assert.IsFalse(host.IsServiceUp);

                host.StartHostedService();
                Assert.IsTrue(host.IsServiceUp);
                host.StopHostedService();
                Assert.IsFalse(host.IsServiceUp);
            }
        }
    }
}
