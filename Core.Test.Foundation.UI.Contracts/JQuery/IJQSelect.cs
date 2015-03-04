

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQSelect : IDropDown
    {
        List<DropDownItem> GetItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        DropDownItem GetSelectedDDItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SelectItemByText(string displayText, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        DropDownItem SelectAnyOne(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        DropDownItem GetSelectedDDItem(bool retryOnFailure, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool WaitForItems(int defaultItemCount, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string getComboValue(string val, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
