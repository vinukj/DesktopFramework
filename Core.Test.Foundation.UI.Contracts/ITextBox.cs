

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface ITextBox : IControl
    {
        void SetText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void TypeText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
