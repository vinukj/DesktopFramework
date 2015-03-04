

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQNumericalUpDownControl:IJQControl
    {
        string GetValue(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void SetValue(string x, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void TypeValue(string x, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
