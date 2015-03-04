

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IControl
    {
        string ControlName { get; set; }
        bool DisableLogging { get; set; }
        bool IsVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool WaitForNotVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool Exist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool Enabled(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void Focus(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        ControlLocation GetLocation(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetErrorString(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void Click(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetToolTip(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void ClickByMouse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void DoubleClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void RightClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
