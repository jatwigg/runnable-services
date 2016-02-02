using Runnable_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Implementations
{
    /// <summary>
    /// A proxy for the System.ServiceProcess.ServiceController class.
    /// </summary>
    internal class ServiceControllerProxy : IServiceController
    {
        public ServiceControllerProxy(ServiceController serviceControllerToWrap)
        {
            ServiceController = serviceControllerToWrap;
        }

        public void Stop() => ServiceController.Stop();

        private ServiceController ServiceController { get; }

        public string ServiceName { get { return ServiceController.ServiceName; } }

        public ServiceControllerStatus Status { get { return ServiceController.Status; } }

    }
}
