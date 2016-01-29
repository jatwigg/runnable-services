using Runnable_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Here we wrap an existing service in a console.
            // To get this to work you need to change the project type to 'Console Application' in properties
            // and declare a static Main methos in a class and set that as the startup object!

            // create the host for our type of service. If you want to instantiate the service first and pass
            // that as a parameter, thats fine.
            var host = new ConsoleServiceHost<AdvancedMonolithicLegacyBusinessLogicServiceVersion_3>();
            
            // this call will start the service. You can pass in an array of strings to simulate command line arguments if you like.
            host.StartHostedService(new string[] { "Hello Service!" });

            Console.WriteLine("Press any key to stop the service.");
            Console.ReadKey();

            // A call to stop will stop the service.
            host.StopHostedService();

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
