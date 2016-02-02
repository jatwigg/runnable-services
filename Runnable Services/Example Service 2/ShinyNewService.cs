using Runnable_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_Service_2
{
    /*
    This example demonstrates a service that defines the entry point for a console application.
        */
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

        protected override void OnStart(string[] args)
        {
            Console.WriteLine("Started service!");
        }
        protected override void OnStop()
        {
            Console.WriteLine("Stopped service!");
        }
    }
}
