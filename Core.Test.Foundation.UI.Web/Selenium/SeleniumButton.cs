//----------------------------------------------------------------------- // 

//<copyright file="SeleniumButton.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumButton : SeleniumControl, ISeleniumButton
    {
        public void SendKeys(String text, ControlRetry retry, bool throwOnFail)
        {
            this.WriteMessage(LogGen.GetSendKeysMessage(text, ControlName));
            IWebElement element = GetElement(retry, throwOnFail);
            if (CheckVisibility(element, retry, throwOnFail))
            {
                element.SendKeys(text);
            }
        }
    }
}
