using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Interfaces
{
    /// <summary>
    /// Interface containing the static functions we use from the class System.ServiceProcess.ServiceController.
    /// </summary>
    public interface IServiceControllerStatic
    {
        IServiceController[] GetServices(); // we wrap the service controllers in our own class
    }
}
