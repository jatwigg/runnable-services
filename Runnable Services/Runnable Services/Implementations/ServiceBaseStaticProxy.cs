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
    /// Assumes the IRunnableService passed in are actually classes that extend System.SystemProcess.ServiceBase.
    /// </summary>
    internal class ServiceBaseStaticProxy : IServiceBaseStatic
    {
        /// <summary>
        /// Registers the executable for multiple services with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="services">An array of IRunnableService instances, which indicate services to start. All services must extend System.SystemProcess.ServiceBase.</param>
        /// 
        /// <exception cref="ArgumentException">A service does not extend the required class.</exception>
        /// <exception cref="ArgumentNullException">Null has been passed in or an element in the array is null.</exception>
        public void Run(IRunnableService[] services)
        {
            if (services == null || services.Any(s => s == null))
            {
                throw new ArgumentNullException("Cannot run the services because the array of services supplied is null.");
            }

            ServiceBase[] castedServices;
            try
            {
                castedServices = services.Cast<ServiceBase>().ToArray();
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException($"Cannot run the services because an element in the array is not of type `{typeof(ServiceBase).Name}`.");
            }

            ServiceBase.Run(castedServices);
        }

        /// <summary>
        /// Registers the executable for a service with the Service Control Manager (SCM).
        /// </summary>
        /// <param name="service">A System.ServiceProcess.ServiceBase that implements IRunnableService which indicates a service to start.</param>
        ///
        /// <exception cref="ArgumentException">The service does not extend the required class.</exception>
        /// <exception cref="ArgumentNullException">Null has been passed in.</exception>
        public void Run(IRunnableService service)
        {
            var castedService = service as ServiceBase;

            if (service == null)
            {
                throw new ArgumentNullException("Cannot run the service because the service supplied is null.");
            }
            else if (castedService == null)
            {
                throw new ArgumentException($"Cannot run the service because the argument supplied is not of type `{typeof(ServiceBase).Name}`.");
            }

            ServiceBase.Run(service as ServiceBase);
        }
    }
}