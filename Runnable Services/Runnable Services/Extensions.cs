﻿using System;
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
            try
            {
                var m = service.GetType().GetMethod("OnStart", BindingFlags.NonPublic | BindingFlags.Instance);
                m.Invoke(service, new object[] { args });
            }
            catch (System.Reflection.TargetInvocationException e) when (e.InnerException != null)
            {
                throw e.InnerException;
            }
        }

        internal static void OnStop(this ServiceBase service)
        {
            try
            {
                var m = service.GetType().GetMethod("OnStop", BindingFlags.NonPublic | BindingFlags.Instance);
                m.Invoke(service, new object[] { });
            }
            catch (System.Reflection.TargetInvocationException e) when (e.InnerException != null)
            {
                throw e.InnerException;
            }
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
        This needs implementing better.
            */
        private static object _hostLock = new { lockForHosts = true };
        private static Dictionary<int, WeakReference<IServiceHost>> _hosts = new Dictionary<int, WeakReference<IServiceHost>>();

        /// <summary>
        /// Returns an instance of the class hosting this service or null.
        /// </summary>
        /// <returns></returns>
        public static IServiceHost GetHost(this ServiceBase service)
        {
            lock (_hostLock)
            {
                // check if it's in the list and if the object has not been garbage collected
                if (_hosts.ContainsKey(service.GetHashCode()))
                {
                    var wr = _hosts[service.GetHashCode()];
                    IServiceHost sh;
                    if (wr.TryGetTarget(out sh))
                    {
                        return sh; // found it
                    }
                    _hosts.Remove(service.GetHashCode()); // object has been disposed of so remove this key
                }
            }
            return null;
        }

        internal static void RecordHost(this IServiceHost host, ServiceBase service)
        {
            lock (_hostLock)
            {
                IServiceHost dontcare;
                // if it is in the list and not disposed of, theres a problem
                if (_hosts.ContainsKey(service.GetHashCode()) && _hosts[service.GetHashCode()].TryGetTarget(out dontcare))
                {
                    throw new ArgumentException("An instance of this service has already been hosted.");
                }
                else
                {
                    _hosts.Add(service.GetHashCode(), new WeakReference<IServiceHost>(host)); // add to list
                }
            }
        }

        internal static void RemoveHost(this IServiceHost host, ServiceBase service)
        {
            lock (_hostLock)
            {
                IServiceHost outedHost;
                if (_hosts.ContainsKey(service.GetHashCode()) && _hosts[service.GetHashCode()].TryGetTarget(out outedHost) && outedHost == host)
                {
                    _hosts.Remove(service.GetHashCode());
                }
            }
        }
    }
}
