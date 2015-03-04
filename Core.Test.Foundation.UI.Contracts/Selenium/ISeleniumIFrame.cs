

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.Selenium
{
    public interface ISeleniumIFrame : IControl
    {
        void Select(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SelectDefault();
        IFrameSearchInfo FrameSearchInfo { get; set; }
        void MoveToFrame(String frameName, ControlRetry retry = ControlRetry.Expected, bool throwOnFail = true);
        void MoveToTopFrame(ControlRetry retry = ControlRetry.Expected, bool throwOnFail = true);
    }
}
