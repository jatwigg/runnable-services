using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Interfaces
{
    public interface IEnvironmentStatic
    {
        bool UserInteractive { get; }

        void RegisterProcessExit(Action closeLogic);
    }
}
