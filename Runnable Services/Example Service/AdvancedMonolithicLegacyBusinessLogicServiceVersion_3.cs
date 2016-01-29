using Runnable_Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Example_Service
{
    public class AdvancedMonolithicLegacyBusinessLogicServiceVersion_3 : ServiceBase
    {
        protected override void OnStart(string[] args)
        {
            Console.WriteLine("I am in a console! I've started!");
            File.WriteAllText(@"C:\temp\start.txt", $"This service started at {DateTime.Now.ToShortTimeString()}", Encoding.ASCII);
        }

        protected override void OnStop()
        {
            Console.WriteLine("Goodbye!");
            File.WriteAllText(@"C:\temp\stop.txt", $"This service closed at {DateTime.Now.ToShortTimeString()}", Encoding.ASCII);
        }
    }
}
