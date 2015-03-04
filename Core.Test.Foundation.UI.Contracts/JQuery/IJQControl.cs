

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQControl:IControl
    {
        string GetInnerText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetInnerHtml(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetAttribute(string attributeName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SetAttribute(string attributeName, string attributeValue, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetUniqueTestId(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        ControlSearchInfo ControlSearchInfo { get; set; }
        void FireEvent(string eventName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        int Size(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void Click(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void EventClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetInnerText(bool includeChild, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetColorAttribute(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
