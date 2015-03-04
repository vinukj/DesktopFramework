
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IJSExecutor
    {
        void SetDriver(object driver);
        void ExecuteScript(string script);
        object GetResult();
        object EvaluateScript(string script);
        bool IsReady(ControlRetry retry);
        void SetTechnology(Technology t);
    }
}
