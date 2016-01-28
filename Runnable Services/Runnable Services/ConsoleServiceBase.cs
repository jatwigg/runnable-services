using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public class ConsoleServiceBase : ServiceBase, IServiceHost
    {
        public static T Create<T>(T service = null) where T : ConsoleServiceBase
        {
            // new up
            throw new NotImplementedException();
        }

        public void StartHostedService(string[] args = null)
        {
            throw new NotImplementedException();
        }

        public void StopHostedService()
        {
            throw new NotImplementedException();
        }
    }
}
