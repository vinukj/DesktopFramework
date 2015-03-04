//----------------------------------------------------------------------- // 

//<copyright file="JQLabel.cs" company="Aptean"> // 
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
    public class JQLabel : JQControl, IJQLabel
    {

        public String GetText(ControlRetry retry, bool throwOnFail)
        {
            this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            String script = JQBuilder.GetGetInnerTextScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? "" : value.ToString();
        }
    }
}
