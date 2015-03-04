

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface ICheckBox : IControl
    {
        bool IsChecked(ControlRetry retry, bool throwOnFail);
        void Check(ControlRetry retry, bool throwOnFail);
        void UnCheck(ControlRetry retry, bool throwOnFail);
    }
}
