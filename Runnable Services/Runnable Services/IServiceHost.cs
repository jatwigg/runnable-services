using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services
{
    public interface IServiceHost
    {
        void StartHostedService(string[] args = null);
        void StopHostedService();
    }
}
