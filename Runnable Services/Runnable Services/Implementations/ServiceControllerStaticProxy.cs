using Runnable_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Implementations
{
    /// <summary>
    /// A proxy for the staic methods in System.SystemProcess.ServiceController.
    /// </summary>
    internal class ServiceControllerStaticProxy : IServiceControllerStatic
    {
        /// <summary>
        /// Retrieves all the services on the local computer, except for the device driver services.
        /// </summary>
        /// 
        /// <returns>An array of type IServiceController in which each element is associated with a service on the local computer.</returns>
        /// 
        /// <exception cref="System.ComponentModel.Win32Exception">An error occurred when accessing a system API.</exception>
        public IServiceController[] GetServices()
        {
            return System.ServiceProcess.ServiceController.GetServices().Select(sc => new ServiceControllerProxy(sc)).ToArray();
        }
    }
}
