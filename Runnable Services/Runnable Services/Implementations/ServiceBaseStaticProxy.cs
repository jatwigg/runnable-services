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
    /// A proxy for the static methods on System.SystemProcess.ServiceBase we use.
    /// </summary>
    internal class ServiceBaseStaticProxy : IServiceBaseStatic
    {
        /// <summary>
        /// Registers the executable for multiple services with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="services">An array of System.ServiceProcess.ServiceBase instances to start. All services must extend System.SystemProcess.ServiceBase.</param>
        /// 
        /// <exception cref="ArgumentException">An element in the array is null.</exception>
        /// <exception cref="ArgumentNullException">Null has been passed in.</exception>
        public void Run(ServiceBase[] services)
        {
            if (services == null)
            {
                throw new ArgumentNullException("Cannot run the services because the array of services supplied is null.");
            }
            if (services.Any(s => s == null))
            {
                throw new ArgumentException("Cannot run the services because the array contains a null element.");
            }
            ServiceBase.Run(services);
        }

        /// <summary>
        /// Registers the executable for a service with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="service">A System.ServiceProcess.ServiceBase to start.</param>
        ///
        /// <exception cref="ArgumentNullException">Null has been passed in.</exception>
        public void Run(ServiceBase service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("Cannot run the service because the service supplied is null.");
            }
            ServiceBase.Run(service);
        }
    }
}