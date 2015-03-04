

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public interface ISeleniumControl : IControl
    {
        ControlSearchInfo ControlSearchInfo { get; set; }
        string GetInnerText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetInnerHtml(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        object GetNativeElement(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void ScrollToElement(String elementId);
        int countElementsByXPath(String xPath);
        void ClickElementsByXpath(string xPath);
        void ClickNumberOfElementsByXpath(string xPath);
    }
}
