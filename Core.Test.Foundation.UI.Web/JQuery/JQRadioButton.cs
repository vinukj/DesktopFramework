//----------------------------------------------------------------------- // 

//<copyright file="JQRadioButton.cs" company="Aptean"> // 
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
    public class JQRadioButton : JQControl, IJQRadioButton
    {

        public bool IsOn(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetIsSelectedRadioButtonMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetCheckedScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            if (value == null)
            {
                return false;
            }

            bool isCheked = false;
            try
            {
                isCheked = bool.Parse(value.ToString());
            }
            catch (Exception exe)
            {
                return false;
            }

            return isCheked;
        }

        public void On(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetCheckRadioButtonMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetCheckRadioButtonScript(searchInfo.Path);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public void Off(Foundation.UI.Contracts.ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException();
        }
    }
}
