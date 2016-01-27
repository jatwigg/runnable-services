using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    /// <summary>
    /// Implement this to run your service as a user. Typically these methods just call the service's protected methods.
    /// </summary>
    public interface IRunnableService
    {
        void StartServiceAsUser(string[] args);
        void StopServiceAsUser();
        void Stop();
        string ServiceName { get; }
    }
}
