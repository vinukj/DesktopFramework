

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public enum ControlRetry
    {
        Super = 1,
        Expected = 2,
        Avg = 20,
        AboveAverage = 10,
        AboveAvg = 10,
        Excellent = 5,
        Worst = 60,
        TooWorst = 120
    }
}
