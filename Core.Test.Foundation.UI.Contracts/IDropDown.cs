

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts.Muia;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IDropDown : IControl
    {
        int GetSelectedIndex(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        String GetSelectedItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SelectItem(String item, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SelectItemByIndex(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetAllItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SelectItemByText(IDropDown Control, string VisibleText);
    }
}
