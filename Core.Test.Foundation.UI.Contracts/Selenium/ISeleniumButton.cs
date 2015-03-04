

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public interface ISeleniumButton:IButton
    {
        void SendKeys(String text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
