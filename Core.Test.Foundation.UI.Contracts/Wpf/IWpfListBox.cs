

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Wpf
{
    public interface IWpfListBox : IWpfControl
    {
        /// <summary>
        /// Returns the item count
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the item count</returns>
        int GetItemsCount(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Select items at the given index
        /// </summary>
        /// <param name="itemIndex">Item index</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        void SelectItem(int itemIndex, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

        /// <summary>
        /// Returns a value which indicates whether items are visible or not
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns a value which indicates whether items are visible or not</returns>
        bool ItemExist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
