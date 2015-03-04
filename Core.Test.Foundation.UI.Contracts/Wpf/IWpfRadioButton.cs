

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Wpf
{
   public interface IWpfRadioButton : IWpfControl,IControl
    {
        /// <summary>
        /// Return whether radio Button is "On" or "Off"
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        bool IsOn(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Set the radio Button "On"
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void On(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Set the Radio Button "Off"
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void Off(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
