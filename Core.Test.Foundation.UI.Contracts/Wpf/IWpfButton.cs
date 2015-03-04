

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Wpf
{
    public interface IWpfButton : IWpfControl,IButton
    {
        /// <summary>
        /// Ensure button is clickable by scrolling if necessary
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void EnsureClickable(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
