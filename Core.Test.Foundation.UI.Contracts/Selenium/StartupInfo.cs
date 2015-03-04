
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public class StartupInfo
    {
        public BrowserType BrowserType { get; set; }
        public string Url { get; set; }
        public string DriverExePath { get; set; }
        public bool ResetAUT { get; set; }
    }
}
