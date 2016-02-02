using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Interfaces
{
    public interface IClosingHooks
    {
        void RegisterProcessExit(Action closeLogic);
    }
}
