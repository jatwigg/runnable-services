using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public static class Extensions
    {
        internal static void OnStart(this ServiceBase service, string[] args)
        {
            var m = service.GetType().GetMethod("OnStart", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(service, new object[] { args });
        }

        internal static void OnStop(this ServiceBase service)
        {
            var m = service.GetType().GetMethod("OnStop", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(service, new object[] { });
        }

        /// <summary>
        /// Returns true if the service is hosted in an IServiceHost. 
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static bool IsHosted(this ServiceBase service)
        {
            return Environment.UserInteractive;
        }

        /*
        This needs improvement.
            */
        private static object _hostLock = new { lockForHosts = true };
        private static Dictionary<int, IServiceHost> _hosts = new Dictionary<int, IServiceHost>();

        /// <summary>
        /// Returns an instance of the class hosting this service or null.
        /// </summary>
        /// <returns></returns>
        public static IServiceHost GetHost(this ServiceBase service)
        {
            lock (_hostLock)
            {
                if (_hosts.ContainsKey(service.GetHashCode()))
                {
                    return _hosts[service.GetHashCode()];
                }
            }
            return null;
        }

        internal static void RecordHost(ServiceBase service, IServiceHost host)
        {
            lock(_hostLock)
            {
                if (_hosts.ContainsKey(service.GetHashCode()))
                {
                    throw new ArgumentException("An instance of this service has already been hosted.");
                }
                else
                {
                    _hosts.Add(service.GetHashCode(), host);
                }
            }
        }
    }
}
