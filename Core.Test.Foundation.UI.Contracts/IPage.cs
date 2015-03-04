

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IPage
    {
        string PageName { get; set; }
        bool IsLoaded(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool IsLoaded(IControl control, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
