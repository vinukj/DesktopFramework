

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Desktop.Test.UI.Framework.Utilities
{
    class CoreAppConfig
    {
        /// <summary>
        /// Gets the performance report path.
        /// </summary>
        /// <value>The performance report path.</value>
        public static string PerfReportPath
        {
            get
            {
                string path = ConfigurationManager.AppSettings.Get("PerfReportPath");

                if (string.IsNullOrEmpty(path))
                {
                    path = "Default";
                }

                return path;
            }
        }

        /// <summary>
        /// Gets a value indicating whether loggers is enabled or not.
        /// </summary>
        /// <value><c>true</c> if logger is enabled; otherwise, <c>false</c>.</value>
        public static bool EnableLogger
        {
            get
            {
                string val = ConfigurationManager.AppSettings.Get("EnableLogger");

                bool enable = false;

                bool.TryParse(val, out enable);

                return enable;
            }
        }
    }
}
