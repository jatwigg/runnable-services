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

        /// <summary>
        /// Returns an instance of the class hosting this service or null.
        /// </summary>
        /// <returns></returns>
        public static IServiceHost GetHost()
        {
            throw new NotImplementedException();
        }
    }
}
