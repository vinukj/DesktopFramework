//----------------------------------------------------------------------- // 

//<copyright file="JQNumericalUpDownControl.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.JQuery;
using System.Windows.Forms;
namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQNumericalUpDownControl : JQControl, IJQNumericalUpDownControl
    {
        public string GetValue(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetCurrentValueMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetCurrentValueScript(this.searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? "0" : value.ToString();
        }

        public void SetValue(string x, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetIncreaseValueMessage(ControlName, x));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSelectValueScript(searchInfo.Path, x);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public void TypeValue(string x, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetIncreaseValueMessage(ControlName, x));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSetTextScript(searchInfo.Path, "");
            WebAppState.JSEngine.ExecuteScript(script);
            this.Focus(retry, throwOnFail);
            Thread.Sleep(1000);
            this.enableBaseLogging = true;
            SendKeys.SendWait(" ");
            SendKeys.SendWait(x);
        }
    }
}
