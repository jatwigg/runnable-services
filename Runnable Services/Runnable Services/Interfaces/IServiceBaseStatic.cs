﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Interfaces
{
    /// <summary>
    /// Interface containing the static methods on System.SystemProcess.ServiceBase we use to start a service.
    /// </summary>
    public interface IServiceBaseStatic
    {
        void Run(IRunnableService service);
        void Run(IRunnableService[] services);
    }
}
