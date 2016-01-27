using Runnable_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Implementations
{
    internal class EnvironmentStaticProxy : IEnvironmentStatic
    {
        public bool UserInteractive { get { return Environment.UserInteractive; } }

        public void RegisterProcessExit(Action closeLogic)
        {
            AppDomain.CurrentDomain.ProcessExit += (o, _) => closeLogic(); // this event catches the program exiting by leaving the main method
            ClosingHook.SetConsoleCtrlHandler(c => { closeLogic(); return true; }, true); // this event catches the program ending abruptly
        }
    }
}
