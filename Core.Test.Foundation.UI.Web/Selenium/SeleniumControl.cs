//----------------------------------------------------------------------- // 

//<copyright file="SeleniumControl.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Web.JQuery;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumControl : ISeleniumControl
    {
        private IWebElement nativeElement;
        public SeleniumControl(IWebElement element)
        {
            this.searchInfo = new ControlSearchInfo();
            this.enableBaseLogging = true;
            nativeElement = element;
        }

        protected ControlSearchInfo searchInfo;
        protected bool enableBaseLogging;
        public SeleniumControl()
        {
            this.searchInfo = new ControlSearchInfo();
            this.enableBaseLogging = true;
        }

        public ControlSearchInfo ControlSearchInfo
        {
            get { return searchInfo; }
            set { searchInfo = value; }
        }

        public string GetInnerText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetInnerTextMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return "";
            }
            return element.Text;
        }

        public bool ReadOnlyState(string state, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return false;
            }
            CheckVisibility(element, retry, throwOnFail);
            if (element.GetAttribute("data-read-only") == state)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GetControlType(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return "";
            }
            CheckVisibility(element, retry, throwOnFail);
            return element.GetAttribute("data-type");
        }

        public string GetInnerHtml(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetInnerTextMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return "";
            }

            return element.GetAttribute("innerHTML");
        }

        public object GetNativeElement(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = GetElement(retry, throwOnFail);
            return element;
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
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return false;
            }
            bool visible = CheckVisibility(element, retry, throwOnFail);

            return visible;
        }

        public bool WaitForNotVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.DisableLogging = true;
            int count = retry.GetHashCode();
            bool invisible = false;
            for (int i = 0; i < count; i++)
            {
                bool result = IsVisible(ControlRetry.Super, false);
                if (!result)
                {
                    invisible = true;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            return invisible;
        }

        public bool Exist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetExistMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return false;
            }
            return element != null;
        }

        public bool Enabled(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetEnabledMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return false;
            }
            return element.Enabled;
        }

        public void Focus(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetFocusMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);

            if (element == null)
            {
                return;
            }
            CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                element.SendKeys(Keys.Tab);
            }
        }

        public ControlLocation GetLocation(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetLocationMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            CheckVisibility(element, retry, throwOnFail);
            ControlLocation location = new ControlLocation();
            if (element == null)
            {
                return location;
            }
            Point p = element.Location;
            location.Height = element.Size.Height;
            location.Width = element.Size.Width;
            location.X = p.X;
            location.Y = p.Y;
            return location;
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
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return;
            }
            CheckVisibility(element, retry, throwOnFail);
            bool failedToClick = false;
            try
            {
                element.Click();
            }
            catch (Exception exe)
            {
                failedToClick = true;
            }
            if (failedToClick && throwOnFail)
            {
                throw new Exception(LogGen.GetFailedToClickMessage(ControlName));
            }
        }

        public string GetToolTip(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return "";
            }
            CheckVisibility(element, retry, throwOnFail);
            return element.GetAttribute("title");
        }

        public void ClickByMouse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            IWebElement element = GetElement(retry, throwOnFail);
            if (element == null)
            {
                return;
            }
            CheckVisibility(element, retry, throwOnFail);
            /*WebDriver driver =(WebDriver) AUTState.browser.getDriver();
            Actions action = new Actions(driver); 
            action.click(element).perform();*/
            TestMouse.Click(this, retry, throwOnFail);
        }

        protected IWebElement GetElement(ControlRetry retry, bool throwOnFail)
        {
            if (nativeElement != null)
            {
                return nativeElement;
            }
            IWebElement element = null;
            //AUTState.browser.waitForPageToLoad(ControlRetry.Worst);
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            By by;
            int count = 1;
            if (this.searchInfo.PathType.Equals(ControlPathType.JQuery))
            {
                String controlId = GetId(retry, throwOnFail);
                if (controlId == null)
                {
                    if (throwOnFail)
                    {
                        String msg = LogGen.GetControlNotVisibleMessage(ControlName, retry);
                        throw new Exception(msg);
                    }
                    else
                    {
                        return null;
                    }
                }
                by = By.XPath(string.Format("//*[@testid='{0}']", controlId));
            }
            else
            {
                count = retry.GetHashCode();
                by = By.XPath(this.searchInfo.Path);
            }

            for (int i = 0; i < count; i++)
            {
                try
                {
                    //by = By.Id("divStaticHeader");
                    element = driver.FindElement(by);
                    break;
                }
                catch (Exception exc)
                {
                    Thread.Sleep(1000);
                }
            }

            return element;
        }

        public List<SeleniumControl> GetChilds(string path = ".//*")
        {
            IWebElement ele = GetElement(ControlRetry.Avg, false);
            ReadOnlyCollection<IWebElement> childs = ele.FindElements(By.XPath(path));
            List<SeleniumControl> childsToReturn = new List<SeleniumControl>();
            foreach (IWebElement element in childs)
            {
                SeleniumControl control = new SeleniumControl(element);
                childsToReturn.Add(control);
            }
            return childsToReturn;
        }
        private string GetId(ControlRetry retry, bool throwOnFail)
        {
            string id = "";
            JQControl control = new JQControl();
            control.DisableLogging = true;
            control.ControlSearchInfo = this.searchInfo;
            id = control.GetUniqueTestId(retry, throwOnFail);
            return id;
        }

        protected void CheckVisibility(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = GetElement(retry, throwOnFail);
            if (element != null)
            {
                CheckVisibility(element, retry, throwOnFail);
            }
        }


        protected bool CheckVisibility(IWebElement element, ControlRetry retry, bool throwOnFail)
        {
            bool visible = false;
            try
            {
                if (element != null)
                {
                    int count = retry.GetHashCode();
                    for (int i = 0; i < count; i++)
                    {
                        if (element.Displayed)
                        {
                            visible = true;
                            break;
                        }
                        else
                        {
                            Thread.Sleep(1000);
                        }
                    }
                }

            }
            catch (NoSuchElementException exe)
            {
                visible = false;
            }
            catch (Exception exe)
            {
                visible = false;
            }

            if (throwOnFail && !visible)
            {
                String msg = LogGen.GetControlNotVisibleMessage(ControlName, retry);
                throw new Exception(msg);
            }

            return visible;
        }

        //Added on 01/16/15
        public void ScrollToElement(String elementId)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            System.Threading.Thread.Sleep(500);
            ((IJavaScriptExecutor)driver).ExecuteScript("document.getElementById('" + elementId + "').scrollIntoView(true);");
        }

        //Added on 01/16/15
        public int countElementsByXPath(String xPath)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath(xPath));
            return elements.Count;
        }

        //Added on 01/16/15
        public void ClickElementsByXpath(string xPath)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            IWebElement elements = driver.FindElement(By.XPath(xPath));
            elements.Click();
        }

        //Added on 01/16/15
        public void ClickNumberOfElementsByXpath(string xPath)
        {
            IWebDriver driver = (IWebDriver)WebAppState.Browser.GetDriver();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath(xPath));
            for (int i = 0; i < elements.Count; i++)
            {
                elements.ElementAt(i).Click();
            }
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
