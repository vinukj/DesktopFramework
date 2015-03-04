

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface ILabel:IControl
    {
        string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
