

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public interface ISeleniumSelect:IDropDown
    {
        void SelectByVisibleText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool ReadOnlyState(string state, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetControlType(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
