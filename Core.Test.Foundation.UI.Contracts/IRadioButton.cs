

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IRadioButton : IControl
    {
        bool IsOn(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void On(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void Off(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
