using Runnable_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Runnable_Services.Implementations
{
    internal class ClosingHooksProxy : IClosingHooks
    {
        /*
            Code lifted from here: http://stackoverflow.com/a/9897366
            This hooks into WIN32 API and runs code when the console is closed using 'X'/shutdown/ctrl+c etc.
            If application just exits by control flow, this does not fire.
        */

        // Declare the SetConsoleCtrlHandler function as external and receiving a delegate.
        [DllImport("Kernel32")]
        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine for SetConsoleCtrlHandler.
        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages sent to the handler routine.
        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        public void RegisterProcessExit(Action closeLogic)
        {
            AppDomain.CurrentDomain.ProcessExit += (o, _) => closeLogic(); // this event catches the program exiting by leaving the main method
            SetConsoleCtrlHandler(c => { closeLogic(); return true; }, true); // this event catches the program ending abruptly
        }        
    }
}
