//----------------------------------------------------------------------- // 

//<copyright file="JQCheckBox.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.JQuery;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQCheckBox : JQControl, IJQCheckBox
    {

        public void Check(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetCheckCheckBoxMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            string script = JQBuilder.GetCheckCheckBoxScript(searchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }


        public void UnCheck(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetUnCheckCheckBoxMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            string script = JQBuilder.GetUnCheckCheckBoxScript(searchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }


        public bool IsChecked(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetCheckBoxIsCheckedMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            string script = JQBuilder.GetCheckedScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            if (value == null)
            {
                return false;
            }

            bool isChecked = false;
            try
            {
                isChecked = bool.Parse(value.ToString());
            }
            catch (Exception exe)
            {
                return false;
            }

            return isChecked;
        }

        public void Check(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            string path = string.Format("{0}:eq({1})", searchInfo.Path, index);
            ControlSearchInfo info = new ControlSearchInfo();
            info.Path = path;
            info.Name = string.Format("{0} at index:", searchInfo.Name, index);
            JQCheckBox control = new JQCheckBox();
            control.ControlSearchInfo = info;
            control.Check(retry, throwOnFail);
        }

        public bool IsChecked(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            string path = string.Format("{0}:eq({1})", searchInfo.Path, index);
            ControlSearchInfo info = new ControlSearchInfo();
            info.Path = path;
            info.Name = string.Format("{0} at index:", searchInfo.Name, index);
            JQCheckBox control = new JQCheckBox();
            control.ControlSearchInfo = info;
            return control.IsChecked(retry, throwOnFail);
        }
    }
}
