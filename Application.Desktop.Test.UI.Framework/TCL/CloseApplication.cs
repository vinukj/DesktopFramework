using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Desktop.Test.UI.Framework.TCL
{   
    public class CloseApplication
    {
        private const int WAIT_UNIT = 2000;

        public static void ByProcessName(string processName)
        {
            Thread.Sleep(WAIT_UNIT);
            Process[] processlist = Process.GetProcessesByName(processName);
            processlist[0].Kill();
        }
    }
}
