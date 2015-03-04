//----------------------------------------------------------------------- // 

//<copyright file="SeleniumSelect.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.Selenium;
namespace Aptean.Test.Foundation.UI.Web.Selenium
{
    public class SeleniumSelect : SeleniumControl, ISeleniumSelect
    {

        public int GetSelectedIndex(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectedIndexMessage(ControlName));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                SelectElement dd = new SelectElement(element);
                string selectedValue = dd.SelectedOption.GetAttribute("value");
                IList<IWebElement> options = dd.AllSelectedOptions;
                int index = -1;
                foreach (IWebElement el in options)
                {
                    index++;
                    if (el.GetAttribute("value").Equals(selectedValue))
                    {
                        return index;
                    }
                }
            }
            return -1;
        }


        public string GetSelectedItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectedIndexMessage(ControlName));
            IWebElement element = this.GetElement(retry, throwOnFail);
            string selectedValue = "";
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                SelectElement dd = new SelectElement(element);
                selectedValue = dd.SelectedOption.GetAttribute("value");
            }
            return selectedValue;
        }


        public void SelectItem(string item, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectItemMessage(ControlName, item));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                SelectElement dd = new SelectElement(element);
                dd.SelectByValue(item);
            }
        }


        public void SelectItemByIndex(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectItemByIndexMessage(ControlName, index));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                SelectElement dd = new SelectElement(element);
                dd.SelectByIndex(index);
            }
        }


        public string GetAllItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            string s = " ";
            if (element != null)
            {
                SelectElement SelectElement = new SelectElement(element);
                IList<IWebElement> options = SelectElement.AllSelectedOptions;
                foreach (IWebElement el in options)
                {
                    s += el.GetAttribute("value") + ",";
                }
            }
            return s.Substring(0, s.Length - 1);
        }


        public void SelectByVisibleText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectItemMessage(ControlName, text));
            IWebElement element = this.GetElement(retry, throwOnFail);
            this.CheckVisibility(element, retry, throwOnFail);
            if (element != null)
            {
                SelectElement SelectElement = new SelectElement(element);
                SelectElement.SelectByText(text);
            }
        }


        public void SelectItemByText(IDropDown Control, string VisibleText)
        {
            throw new NotImplementedException();
        }
    }
}
