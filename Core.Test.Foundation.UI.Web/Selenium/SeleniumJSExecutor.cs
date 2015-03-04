//----------------------------------------------------------------------- // 

//<copyright file="SeleniumJSExecutor.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumJSExecutor : IJSExecutor
    {
        private Technology technology;
        private RemoteWebDriver driver;
        private Object lastResult;
        bool isLoaded = false;

        public SeleniumJSExecutor()
        {
            this.technology = Technology.Flex;
        }


        public void SetTechnology(Technology t)
        {
            this.technology = t;
        }
        public void Close()
        {
            driver.Close();
        }


        public void ExecuteScript(String script)
        {
            IsReady(ControlRetry.Avg);
            ExecuteRetryScript(script);
        }

        private void ExecuteRetryScript(String script)
        {
            bool retry = true;
            for (int idx = 0; idx < 20; idx++)
            {
                if (!retry)
                {
                    break;
                }
                try
                {
                    lastResult = driver.ExecuteScript(script, "");
                    break;
                }
                catch (Exception exe)
                {

                    if (exe.Message.Contains("Command duration or timeout: 257 milliseconds"))
                    {
                        retry = true;
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        retry = false;
                    }
                }
            }
        }


        public Object GetResult()
        {
            return lastResult;
        }


        public Object EvaluateScript(String script)
        {
            this.IsReady(ControlRetry.Avg);
            this.ExecuteRetryScript(script);
            return lastResult;
        }


        public void SetDriver(Object driver)
        {
            this.driver = (RemoteWebDriver)driver;
        }


        public bool IsReady(ControlRetry retry)
        {

            if (isLoaded)
            {
                return isLoaded;
            }

            int count = retry.GetHashCode();
            bool exception = false;
            for (int idx = 0; idx < count; idx++)
            {
                try
                {

                    if (exception)
                    {
                        exception = false;
                        Thread.Sleep(1000);
                    }
                    lastResult = driver.ExecuteScript("return document.readyState;", "");
                    isLoaded = lastResult.Equals("complete");
                    if (isLoaded)
                    {
                        break;
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    exception = true;
                }
            }

            return isLoaded;
        }
    }
}
