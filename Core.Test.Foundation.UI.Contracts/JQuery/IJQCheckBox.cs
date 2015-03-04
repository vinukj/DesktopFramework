

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQCheckBox : IJQControl
    {
        void Check(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        bool IsChecked(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
