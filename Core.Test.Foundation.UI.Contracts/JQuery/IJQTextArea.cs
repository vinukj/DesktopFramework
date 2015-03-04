
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQTextArea : IJQControl, IJQTextBox
    {
        String GetText(bool retryOnFailure, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
