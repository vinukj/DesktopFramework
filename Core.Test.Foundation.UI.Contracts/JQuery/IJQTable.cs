

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts.JQuery
{
    public interface IJQTable : IJQControl
    {
        bool WaitForData(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void ClickRow(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        int GetRowCount(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        int FindRowIndex(string searchString, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string[] GetRowData(string searchString, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        string[] GetRowData(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void ClickCell(string searchString, int cellIndex, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
        void ClickCell(string searchString, int cellIndex, string extendedPath, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);
    }
}
