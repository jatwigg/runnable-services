using Runnable_Services.Implementations;
using Runnable_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public class ConsoleServiceHost : IServiceHost
    {
        private ServiceBase _service;
        // used in case we receive 2 exit signals
        private object _hasCaughtAnExitLock = new { ThisIsTheLockForTheCaughtAnExitBool = true };
        private bool _hasCaughtAnExit = false;
        // used to indicate the current state of the service
        private object _serviceHasStartedLock = new { ThisIsTheLockForHasStartedBool = true };
        private bool _serviceHasStarted = false;

        public ConsoleServiceHost(ServiceBase serviceInstance)
        {
            if (serviceInstance == null)
            {
                throw new ArgumentNullException("Service cannot be null.");
            } 
            _service = serviceInstance;
            // if this is a console, close the service if it's installed and running and subscribe to the exit events
            closeRunningService(_service.ServiceName);
            ClosingHooks.RegisterProcessExit(closeLogic);

            // attach the host for retrieval in the service if needed
            this.RecordHost(_service);
        }

        ~ConsoleServiceHost()
        {
            this.RemoveHost(_service);
        }

        /// <summary>
        /// Creates instance of service and instance of this class.
        /// </summary>
        /// <typeparam name="T">Service Type</typeparam>
        /// <returns></returns>
        public static ConsoleServiceHost Create<T>() where T : ServiceBase, new()
        {
            return new ConsoleServiceHost(new T());
        }

        /// <summary>
        /// Attempts to close a service by name if it's running.
        /// </summary>
        /// <param name="serviceName"></param>
        private void closeRunningService(string serviceName)
        {
            var service = ServiceController.GetServices().FirstOrDefault(s => s.ServiceName == serviceName);
            if (service?.Status == System.ServiceProcess.ServiceControllerStatus.Running)
            {
                service.Stop();
            }
        }

        private void closeLogic()
        {
            lock (_hasCaughtAnExitLock)
            {
                if (_hasCaughtAnExit)
                {
                    // exit has already been handled
                    return;
                }
                else
                {
                    _hasCaughtAnExit = true; // we have caught the exit
                }
            }

            // still here? then perform the exit logic
            lock (_serviceHasStartedLock)
            {
                if (!_serviceHasStarted)
                {
                    return; // apparently the service is not started
                }
                _serviceHasStarted = false;
            }

            // do stop logic
            if (_service.IsHosted())
            {
                _service.OnStop();
            }
            else
            {
                _service.Stop();
            }
        }

        public void StartHostedService(string[] args = null)
        {
            // ensure that we only start it once
            lock (_serviceHasStartedLock)
            {
                if (_serviceHasStarted)
                {
                    return; // already started
                }
                _serviceHasStarted = true;
            }

            // start service as console app or service
            if (args == null)
            {
                args = new string[] { };
            }

            if (_service.IsHosted())
            {
                _service.OnStart(args);
                Console.WriteLine("Service started. Press <Enter> to exit . . . ");
                Console.ReadLine();
            }
            else
            {
                ServiceBase.Run(_service);
            }
        }

        public void StopHostedService()
        {
            // ensure that we only start it once
            lock (_serviceHasStartedLock)
            {
                if (!_serviceHasStarted)
                {
                    return; // not running
                }
                _serviceHasStarted = false;
            }

            // start service as console app or service
            if (_service.IsHosted())
            {
                _service.OnStop();
            }
            else
            {
                _service.Stop();
            }
        }

        public bool IsServiceUp
        {
            get
            {
                lock (_serviceHasStartedLock)
                {
                    return _serviceHasStarted;
                }
            }
        }
        public IClosingHooks ClosingHooks { get; set; } = new ClosingHooksProxy();
        public IServiceBaseStatic ServiceBase { get; set; } = new ServiceBaseStaticProxy();
        public IServiceControllerStatic ServiceController { get; set; } = new ServiceControllerStaticProxy();
    }
}
