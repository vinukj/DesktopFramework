//----------------------------------------------------------------------- // 

//<copyright file="JQTextArea.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.JQuery;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQTextArea : JQControl, IJQTextArea
    {
        public void SetText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSetTextMessage(ControlName, text));
            this.CheckVisibility(retry, throwOnFail);
            this.enableBaseLogging = false;
            this.Focus(retry, throwOnFail);
            this.enableBaseLogging = true;
            string script = JQBuilder.GetSetTextAreaTextScript(searchInfo.Path, text);
            WebAppState.JSEngine.ExecuteScript(script);
        }


        public void TypeText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSetTextMessage(ControlName, text));
            this.CheckVisibility(retry, throwOnFail);
            this.enableBaseLogging = false;
            this.Focus(retry, throwOnFail);
            Thread.Sleep(1000);
            this.enableBaseLogging = true;
            SendKeys.SendWait(text);
        }


        public string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            this.enableBaseLogging = false;
            this.Focus(retry, throwOnFail);
            this.enableBaseLogging = true;
            string script = JQBuilder.GetGetTextAreaTextScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? "" : value.ToString();
        }

        public string GetText(bool retryOnFailure, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            int retryCount = 0;
            if (retryOnFailure)
            {
                retryCount = 5;
            }
            this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            string item = "";
            this.DisableLogging = true;
            for (int i = 0; i < retryCount; i++)
            {
                item = this.GetText(retry, throwOnFail);
                if (item.Trim().Length != 0)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            this.DisableLogging = true;
            return item;
        }
    }
}
