//----------------------------------------------------------------------- // 

//<copyright file="JQControl.cs" company="Aptean"> // 
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

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQControl : IJQControl
    {
        protected ControlSearchInfo searchInfo;

        public JQControl()
        {
            this.ControlSearchInfo = new ControlSearchInfo();
            this.enableBaseLogging = true;
            this.DisableLogging = false;
        }

        protected bool enableBaseLogging;
        public string GetInnerText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetInnerTextMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = "";
            script = JQBuilder.GetGetInnerTextScript(searchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString().Trim();
        }

        public string GetInnerHtml(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetInnerHtmlMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetInnerHtmlScript(searchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString();
        }

        public void EventClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetEventClickScript(ControlSearchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public string GetAttribute(string attributeName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetGetAttributeMessage(ControlName, attributeName));
            }
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetGetAttributeScript(searchInfo.Path, attributeName);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString();
        }

        public void SetAttribute(string attributeName, string attributeValue, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetSetAttributeMessage(ControlName, attributeName, attributeValue));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetSetAttributeScript(searchInfo.Path, attributeName, attributeValue);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public string GetUniqueTestId(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetGetUniqueIdMessage(ControlName));
            }
            if (!this.CheckExistence(retry, throwOnFail))
            {
                return null;
            }

            /*String oldId = getAttribute("id", ControlRetry.Avg, true); */
            String oldId = "";
            if (oldId == null || oldId.Trim().Length == 0)
            {
                oldId = Guid.NewGuid().ToString();
                String id = oldId;
                int count = retry.GetHashCode();
                bool result = false;
                for (int i = 0; i < count; i++)
                {
                    SetAttribute("testid", id, ControlRetry.Super, false);
                    String expected = GetAttribute("testid", ControlRetry.Super, false);
                    if (id.Equals(expected.Trim()))
                    {
                        result = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }

                if (throwOnFail && !result)
                {
                    String msg = LogGen.GetControlNotVisibleMessage(ControlName, retry);
                    throw new Exception(msg);
                }
            }
            return oldId;
        }

        public ControlSearchInfo ControlSearchInfo
        {
            get { return searchInfo; }
            set { searchInfo = value; }
        }

        public void FireEvent(string eventName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetFireEventMessage(ControlName, eventName));
            }
            bool traceAction = this.enableBaseLogging;
            this.enableBaseLogging = false;
            bool exist = this.Exist(retry, throwOnFail);
            this.enableBaseLogging = traceAction;
            if (exist)
            {
                String script = JQBuilder.GetFireEventScript(ControlSearchInfo.Path, eventName);
                WebAppState.JSEngine.ExecuteScript(script);
            }
        }

        public int Size(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetCountMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetElementCountScript(searchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            int count = obj == null ? 0 : int.Parse(obj.ToString());
            return count;
        }

        public void Click(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            string path = string.Format("{0}:eq{1})", searchInfo.Path, index);
            ControlSearchInfo info = new ControlSearchInfo();
            info.Path = path;
            info.Name = string.Format("%s at index:", searchInfo.Name, index);
            JQControl control = new JQControl();
            control.ControlSearchInfo = info;
            control.Click(retry, throwOnFail);
        }

        public string GetInnerText(bool includeChild, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetInnerTextMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = "";
            if (includeChild)
            {
                script = JQBuilder.GetGetInnerTextScript(searchInfo.Path);
            }
            else
            {
                script = JQBuilder.GetGetInnerTextScriptIgnoreChild(searchInfo.Path);
            }
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString().Trim();
        }
        protected void WriteMessage(string message)
        {
            LogUtil.WriteMessage(message, this);
        }

        public string ControlName
        {
            get { return this.ControlSearchInfo.Name; }
            set { this.ControlSearchInfo.Name = value; }
        }

        public bool DisableLogging { get; set; }

        public bool IsVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetVisibleMessage(ControlName));
            }

            bool traceAction = this.enableBaseLogging;
            this.enableBaseLogging = false;
            bool exist = this.Exist(retry, throwOnFail);
            this.enableBaseLogging = traceAction;
            int count = retry.GetHashCode();
            bool visible = false;

            if (exist)
            {
                for (int idx = 0; idx < count; idx++)
                {
                    try
                    {
                        String script = JQBuilder.GetVisibleScript(ControlSearchInfo.Path);
                        Object obj = WebAppState.JSEngine.EvaluateScript(script);
                        String val = obj == null ? "false" : obj.ToString();
                        visible = bool.Parse(val);
                        bool displayed = true;
                        if (visible && displayed)
                        {
                            break;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    catch (Exception exe)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }

            if (throwOnFail && !visible)
            {
                String msg = LogGen.GetControlNotVisibleMessage(ControlName, retry);
                throw new Exception(msg);
            }

            return visible;
        }

        public bool WaitForNotVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetWaitingForDisappearMessage(ControlName));
            }

            int count = retry.GetHashCode();
            bool visible = false;
            this.DisableLogging = true;
            for (int idx = 0; idx < count; idx++)
            {
                try
                {
                    visible = this.isVisibleNew(ControlRetry.Super, false);
                    String script = JQBuilder.GetGetAttributeScript(ControlSearchInfo.Path, "style");
                    Object obj = WebAppState.JSEngine.EvaluateScript(script);
                    bool displayTypeNone = false;
                    if (obj != null)
                    {
                        displayTypeNone = obj.ToString().Contains("display: none;");
                    }

                    if (!visible || displayTypeNone)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception exe)
                {
                    break;
                }
            }
            this.DisableLogging = false;
            if (visible && throwOnFail)
            {
                String msg = LogGen.GetControlVisibleMessage(ControlName, retry);
                throw new Exception(msg);
            }

            return !visible;
        }

        public bool Exist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetExistMessage(ControlName));
            }
            int count = retry.GetHashCode();
            bool visible = false;
            String script = JQBuilder.GetElementCountScript(ControlSearchInfo.Path);
            for (int idx = 0; idx < count; idx++)
            {
                try
                {
                    Object obj = WebAppState.JSEngine.EvaluateScript(script);
                    String val = obj == null ? "0" : obj.ToString();
                    String result = val.ToString();
                    if (int.Parse(result) >= 1)
                    {
                        visible = true;
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception exe)
                {
                    Thread.Sleep(1000);
                }
            }

            if (throwOnFail && !visible)
            {
                String msg = LogGen.GetControlNotVisibleMessage(ControlSearchInfo.Name, retry);
                throw new Exception(msg);
            }

            return visible;
        }

        public bool Enabled(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetEnabledMessage(ControlName));
            }
            CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetDisabledScript(ControlSearchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? false : !bool.Parse(obj.ToString());
        }

        public void Focus(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetFocusMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetFocusScript(ControlSearchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public ControlLocation GetLocation(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetLocationMessage(ControlName));
            }
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetLocationScript(ControlSearchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            String[] tok = obj.ToString().Split(',');
            ControlLocation p = new ControlLocation();
            p.X = double.Parse(tok[0]);
            p.Y = double.Parse(tok[1]);
            p.Width = double.Parse(tok[2]);
            p.Height = double.Parse(tok[3]);
            return p;
        }

        public string GetErrorString(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            return "";
        }

        public void Click(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetClickScript(ControlSearchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public string GetToolTip(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            return null;
        }

        public void ClickByMouse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            CheckVisibility(retry, throwOnFail);
            TestMouse.Click(this, retry, throwOnFail);
        }

        protected void CheckVisibility(ControlRetry retry, bool throwOnFail)
        {
            this.enableBaseLogging = false;
            this.IsVisible(retry, throwOnFail);
            this.enableBaseLogging = true;
        }

        protected bool CheckExistence(ControlRetry retry, bool throwOnFail)
        {
            this.enableBaseLogging = false;
            bool result = this.Exist(retry, throwOnFail);
            this.enableBaseLogging = true;
            return result;
        }

        private bool isVisibleNew(ControlRetry retry, bool throwOnFail)
        {

            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetVisibleMessage(ControlName));
            }

            bool traceAction = this.enableBaseLogging;
            this.enableBaseLogging = false;
            bool exist = this.Exist(retry, throwOnFail);
            this.enableBaseLogging = traceAction;
            int count = retry.GetHashCode();
            bool visible = false;

            if (exist)
            {
                for (int idx = 0; idx < count; idx++)
                {
                    try
                    {
                        String script = JQBuilder.GetVisibleScript(ControlSearchInfo.Path);
                        Object obj = WebAppState.JSEngine.EvaluateScript(script);
                        String val = obj == null ? "false" : obj.ToString();
                        visible = bool.Parse(val);
                        if (visible)
                        {
                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                    catch (Exception exe)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

            if (throwOnFail && !visible)
            {
                String msg = LogGen.GetControlNotVisibleMessage(ControlName, retry);
                throw new Exception(msg);
            }

            return visible;
        }

        //Added on 01/16/15
        public string GetColorAttribute(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetGetAttributeMessage(ControlName, "color"));
            }
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetCSSColorScript(searchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString();
        }


        public void DoubleClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException();
        }

        public void RightClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException();
        }
    }
}
