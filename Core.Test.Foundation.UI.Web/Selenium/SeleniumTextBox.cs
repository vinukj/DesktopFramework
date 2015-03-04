//----------------------------------------------------------------------- // 

//<copyright file="SeleniumTextBox.cs" company="Aptean"> // 
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
    public class SeleniumTextBox : SeleniumControl, ISeleniumTextBox
    {
        public void SetText(String text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSetTextMessage(ControlName, text));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    element.Clear();
                    element.SendKeys(text.Trim());
                    this.DisableLogging = true;
                    String actual = element.GetAttribute("value");
                    if (actual.Trim().Equals(text.Trim()))
                    {
                        break;
                    }
                }
                this.DisableLogging = false;
            }
        }

        public void TypeText(String text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            SetText(text, retry, throwOnFail);
        }

        public String GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            String text = "";
            if (element != null)
            {
                text = element.GetAttribute("value");
                if (text.Trim().Length == 0)
                {
                    text = element.Text;
                }
            }
            return text;
        }
    }
}
