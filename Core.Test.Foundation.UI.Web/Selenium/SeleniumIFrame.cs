//----------------------------------------------------------------------- // 

//<copyright file="SeleniumIFrame.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;
using OpenQA.Selenium;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumIFrame : SeleniumControl, ISeleniumIFrame
    {
        public void Select(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(string.Format("Selecting frame '{0}'", FrameSearchInfo.GetFullName()));
            IFrameSearchInfo nextFrame = FrameSearchInfo.Clone();
            WebAppState.Browser.SwitchToWindow("default");
            while (nextFrame != null)
            {
                if (SelectFrame(nextFrame, retry, throwOnFail))
                {
                    nextFrame = nextFrame.NextFrame;
                    if (nextFrame != null)
                    {
                        nextFrame = nextFrame.Clone();
                    }
                }
                else
                {
                    this.WriteMessage(String.Format("Failed to select frame '{0}'", FrameSearchInfo.GetFullName()));
                    break;
                }
            }
        }

        public void SelectDefault()
        {
            WebAppState.Browser.SwitchToWindow("default");
        }

        public IFrameSearchInfo FrameSearchInfo { get; set; }

        private bool SelectFrame(IFrameSearchInfo nextFrame, ControlRetry retry, bool throwOnFail)
        {
            SeleniumControl control = new SeleniumControl();
            ControlSearchInfo info = new ControlSearchInfo();
            info.Name = nextFrame.Name;
            info.Path = nextFrame.Path;
            info.PathType = ControlPathType.Selenium;
            control.ControlSearchInfo = info;
            bool success = control.Exist(retry, throwOnFail);
            if (success) 
            {
                WebAppState.Browser.SelectFrame("xpath", info.Path);
            }
            return success;
        }

        //Added on 01/16/15
        public void MoveToFrame(String frameName, ControlRetry retry = ControlRetry.Expected, bool throwOnFail = true)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            driver.SwitchTo().Frame(frameName);
        }

        //Added on 01/16/15
        public void MoveToTopFrame(ControlRetry retry = ControlRetry.Expected, bool throwOnFail = true)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            driver.SwitchTo().DefaultContent();
        }
    }
}
