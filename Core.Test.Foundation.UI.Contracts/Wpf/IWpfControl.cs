

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Core.Test.Foundation.UI.Contracts.Wpf
{
    public interface IWpfControl : IControl
    {
        /// <summary>
        /// Click the control using control coordinates
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void ClickByPoint(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Click the control without using mouse
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void CustomClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Returns the control location on the screen
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns> Returns the control location on the screen</returns>
        Point GetPoint(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Returns the first level children's 
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the first level children's </returns>
        IList<IWpfControl> GetChilds(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
