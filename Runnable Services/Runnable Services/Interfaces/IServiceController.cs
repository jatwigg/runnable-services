using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Interfaces
{
    /// <summary>
    /// Interface containing the methods we use in System.SystemProcess.ServiceController instances.
    /// </summary>
    public interface IServiceController
    {
        string ServiceName { get; }
        ServiceControllerStatus Status { get; }
        void Stop();
    }
}
