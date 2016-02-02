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
        //public ConsoleServiceBase()
        //{
        //    throw new Exception("Don't new up an instance of this class, derive your own class from it and .Create() it.");
        //}

        public static IServiceHost Create<T>() where T : ConsoleServiceBase, new()
        {
            var host = new ConsoleServiceHost<T>();
            return host;
        }
    }
}
