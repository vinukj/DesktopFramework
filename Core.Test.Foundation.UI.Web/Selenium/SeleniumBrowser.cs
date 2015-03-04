//----------------------------------------------------------------------- // 

//<copyright file="SeleniumBrowser.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aptean.Test.Foundation.UI.Web.JQuery;
using Aptean.Test.Foundation.UI.Web.Selenium;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;
using Keys = System.Windows.Forms.Keys;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumBrowser : ISeleniumBrowser
    {
        private IWebDriver driver;
        private BrowserType type;
        private StartupInfo startupInfo;

        public void Refresh()
        {
            try
            {
                driver.Navigate().Refresh();
                WaitForPageToLoad(ControlRetry.Worst);
            }
            catch (Exception exe)
            {

            }
        }

        public String GetCurrentUrl()
        {
            try
            {
                return driver.Url;
            }
            catch (Exception exe)
            {

            }
            return "";
        }

        public void CleanupCookies()
        {
            try
            {
                driver.Manage().Cookies.DeleteAllCookies();
            }
            catch (Exception exe)
            {

            }
        }

        public void CloseTab(int tabIndex)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabIndex]);
            driver.Close();
        }

        public void SelectTab(int tabIndex)
        {
            driver.SwitchTo().Window(driver.WindowHandles[tabIndex]);
        }

        public void WaitForPageToLoad(ControlRetry retry)
        {
            LogUtil.WriteMessage("Waiting for Page to load");
            for (int i = 0; i < retry.GetHashCode(); i++)
            {
                Object result = ((RemoteWebDriver)driver).ExecuteScript("return document.readyState;");
                if (result != null && result.ToString().Trim().StartsWith("complete"))
                {
                    break;
                }
                else
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Exception exe)
                    {

                    }
                }
            }
        }

        public void SelectFrame(string attributeName, string attributeValue)
        {
            String xpathExpression = "";
            if (!attributeName.ToLower().Equals("xpath"))
            {
                xpathExpression = string.Format("//iframe[@%s='%s']", attributeName, attributeValue);
            }
            else
            {
                xpathExpression = attributeValue;
            }
            IWebElement element = driver.FindElement(By.XPath(xpathExpression));
            driver.SwitchTo().Frame(element);
        }

        public void SwitchToWindow(string windowTitle)
        {
            if (windowTitle == null || windowTitle.Equals("") || windowTitle.Equals("default"))
            {
                driver.SwitchTo().DefaultContent();
            }
            else
            {
                driver.SwitchTo().Window(windowTitle);
            }
        }

        public void Start(Object info)
        {
            this.startupInfo = (StartupInfo)info;
            this.type = startupInfo.BrowserType;
            switch (type)
            {
                case BrowserType.IE:
                    StartIE(startupInfo.Url, this.startupInfo.DriverExePath);
                    break;
                case BrowserType.FF:
                    StartFF(startupInfo.Url);
                    break;
                case BrowserType.Chrome:
                    StartChrome(startupInfo.Url, startupInfo.DriverExePath);
                    break;
                case BrowserType.PhantomJS:
                    StartPhantomJS(startupInfo.Url, startupInfo.DriverExePath);
                    break;
                default:
                    StartIE(startupInfo.Url, this.startupInfo.DriverExePath);
                    break;
            }

            driver.Manage().Window.Maximize();
        }

        private void StartPhantomJS(String url, String driverPath)
        {
        }

        public object GetDriver()
        {
            return driver;
        }

        public bool Stop()
        {
            bool success = false;
            try
            {
                KillProcess();
                driver.Quit();
                success = true;
            }
            catch (Exception exe)
            {
            }

            return success;
        }

        public void Navigate(string url)
        {
            string message = string.Format("Navigating to Url='%s'", url);
            LogUtil.WriteMessage(message);
            driver.Url = url;
            driver.Navigate();
            WaitForPageToLoad(ControlRetry.Worst);
        }

        private void StartIE(String url, string driverDir)
        {
            if (this.startupInfo.ResetAUT)
            {
                KillProcess();
            }

            InternetExplorerOptions op = new InternetExplorerOptions();
            op.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
            op.InitialBrowserUrl = url;
            op.EnableNativeEvents = true;
            driver = new InternetExplorerDriver(driverDir, op);
        }

        private void StartChrome(string url, string dir)
        {
            if (this.startupInfo.ResetAUT)
            {
                KillProcess("chrome");
                KillProcess("chromedriver");
            }
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("test-type");
            driver = new ChromeDriver(dir, options);
            driver.Url = url;
            driver.Navigate();
        }

        private void StartFF(string url)
        {
            if (this.startupInfo.ResetAUT)
            {
                KillProcess("firefox");
            }
            var driverInfo = new BrowserInfo(BrowserType.FF);
            string profilePath = string.Format(driverInfo.ProfilePath, driverInfo.User);
            string[] dir = Directory.GetDirectories(profilePath);
            FirefoxProfile profile = new FirefoxProfile(dir[0]);
            profile.SetPreference("webdriver_enable_native_events", false);
            driver = new FirefoxDriver(profile);
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        private void KillProcess()
        {
            switch (type)
            {
                case BrowserType.IE:
                    KillProcess("IEDriverServer");
                    KillProcess("iexplore");
                    break;
                case BrowserType.FF:
                    break;
                case BrowserType.Chrome:
                    KillProcess("chromedriver");
                    KillProcess("chrome");
                    break;
                default:
                    KillProcess("firefox");
                    break;
            }
        }

        private void KillProcess(String processName){
            Process[] process = Process.GetProcessesByName(processName);
            foreach (Process pr in process)
            {
                try
                {
                    pr.Kill();
                }
                catch (Exception exe) { }
            }          
            
        }

        public void UploadFileManually(IControl browseButton, String fileName)
        {
            IWebElement element = null;
            if (browseButton is SeleniumControl)
            {
                element = (IWebElement)((SeleniumControl)browseButton).GetNativeElement(ControlRetry.Avg, true);
            }
            else
            {
                String id = ((JQControl)browseButton).GetUniqueTestId(ControlRetry.Avg, true);
                By by = By.XPath(string.Format("//*[@testid='%s']", id));
                element = driver.FindElement(by);
            }
            Actions action = new Actions(driver);
            action.DoubleClick(element).Perform();
            Thread.Sleep(2000);
            SendKeys.SendWait(fileName);
            Thread.Sleep(1000);
            SendKeys.SendWait("{TAB}");
            SendKeys.SendWait("{TAB}");
            Thread.Sleep(1000);
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(1000);
        }

        public void DismissAlert(IControl alertActionElement, AlertOption option)
        {
            IWebElement element = null;
            if (alertActionElement is SeleniumControl)
            {
                element = (IWebElement)((SeleniumControl)alertActionElement).GetNativeElement(ControlRetry.Avg, true);
            }
            else
            {
                String id = ((JQControl)alertActionElement).GetUniqueTestId(ControlRetry.Avg, true);
                By by = By.XPath(string.Format("//*[@testid='%s']", id));
                element = driver.FindElement(by);
            }
            Actions action = new Actions(driver);
            action.DoubleClick(element).Perform();
            Thread.Sleep(1000);
            IAlert alert = null;
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    alert = driver.SwitchTo().Alert();
                    break;
                }
                catch (Exception exe)
                {
                    Thread.Sleep(1000);
                }
            }
            if (alert != null)
            {
                if (option.Equals(AlertOption.OK) || option.Equals(AlertOption.Yes))
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
            }
            driver.SwitchTo().DefaultContent();
        }



        public string ValidateAlertMessage()
        {
            IAlert al = driver.SwitchTo().Alert();
            String alertMsg = al.Text;
            al.Accept();
            return alertMsg;
        }
    }
}
