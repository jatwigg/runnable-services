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
    public class ConsoleServiceHost<T> : IServiceHost where T : ServiceBase, new()
    {
        private T _service;
        // used in case we receive 2 exit signals
        private object _hasCaughtAnExitLock = new { ThisIsTheLockForTheCaughtAnExitBool = true };
        private bool _hasCaughtAnExit = false;
        // used to indicate the current state of the service
        private object _serviceHasStartedLock = new { ThisIsTheLockForHasStartedBool = true };
        private bool _serviceHasStarted = false;

        public ConsoleServiceHost(T serviceInstance = default(T))
        {
            // assign or instantiate service 
            _service = serviceInstance == null ? new T() : serviceInstance;
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
            _service.OnStop();
            return;
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
            _service.OnStart(args);
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
            _service.OnStop();
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
