using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public abstract class ConsoleServiceBase : ServiceBase, IServiceHost
    {
        public static IServiceHost Create<T>() where T : ConsoleServiceBase, new()
        {
            return new T();
        }

        public abstract void StartHostedService(string[] args = null);

        public abstract void StopHostedService();
    }
}
