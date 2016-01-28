using Runnable_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_Service_2
{
    public class ShinyNewService : ConsoleServiceBase
    {
        public static void Main(string[] args)
        {
            // Add a static Main method to your service and add the following code.
            // Make sure to set your app as a 'Console Application' and set this Main as the startup object.

            IServiceHost serviceInstance = Create<ShinyNewService>();

            // start the service
            serviceInstance.StartHostedService();

            // stop the service
            serviceInstance.StopHostedService();
        }
    }
}
