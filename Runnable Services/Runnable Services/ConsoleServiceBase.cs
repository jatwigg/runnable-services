using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public class ConsoleServiceBase : ServiceBase
    {
        public ConsoleServiceBase()
        {
            if (this.GetType() == typeof(ConsoleServiceBase))
            {
                throw new Exception("Don't new up an instance of this class, derive your own class from it.");
            }
            Host = new ConsoleServiceHost(this);
        }

        /// <summary>
        /// New up the service and return the host.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IServiceHost Create<T>() where T : ConsoleServiceBase, new()
        {
            var host = new T();
            return host.Host;
        }

        /// <summary>
        /// Get the Host for this service.
        /// </summary>
        public IServiceHost Host { get; private set; }

    }
}
