//----------------------------------------------------------------------- // 

//<copyright file="JQSelect.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.JQuery;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQSelect : JQControl, IJQSelect
    {
        public DropDownItem SelectAnyOne(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.DisableLogging = true;
            List<DropDownItem> items = null;
            for (int i = 0; i < retry.GetHashCode(); i++)
            {
                items = this.GetItems(ControlRetry.Super, false);
                if (items.Count > 1)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            this.DisableLogging = false;
            if (items != null && items.Count > 0)
            {
                int index = new Random().Next(items.Count - 1);
                this.WriteMessage(LogGen.GetSelectItemMessage(ControlName, items[index].Name));
                this.SelectByValue(items[index].Value, retry, throwOnFail);
                return items[index];
            }
            else
            {
                return null;
            }
        }
        public void SelectItemByText(String displayText, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectItemMessage(ControlName, displayText));
            this.CheckExistence(retry, throwOnFail);
            SelectByText(displayText, retry, throwOnFail);
        }

        public DropDownItem GetSelectedDDItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            DropDownItem item = new DropDownItem();
            this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSelectedDDItemScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);

            if (value != null)
            {
                String[] val = value.ToString().Split(';');
                if (val.Length >= 2)
                {
                    item.Value = val[0];
                    item.Name = val[1];
                }
            }

            return item;
        }

        public DropDownItem GetSelectedDDItem( bool retryOnFailure, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            int retryCount = 0;
            if (retryOnFailure)
            {
                retryCount = 5;
            }
            this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            DropDownItem item = new DropDownItem();
            this.DisableLogging = true;
            for (int i = 0; i < retryCount; i++)
            {
                item = this.GetSelectedDDItem(retry, throwOnFail);
                if (item.Name.Trim().Length != 0)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
            this.DisableLogging = false;
            return item;
        }


        public bool WaitForItems(int defaultItemCount, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            int count = retry.GetHashCode();
            bool firstLoop = true;
            bool success = false;
            for (int i = 0; i < count; i++)
            {
                if (!firstLoop)
                {
                    this.DisableLogging = true;
                }

                if (GetItems(ControlRetry.Super, false).Count > defaultItemCount)
                {
                    success = true;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }

            }
            if (throwOnFail && !success)
            {
                String message = LogGen.failToLoadItemMessage(ControlName);
                throw new Exception(message);
            }
            return success;
        }

        public List<DropDownItem> GetItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            List<DropDownItem> items = new List<DropDownItem>();
            String script = JQBuilder.GetDDItems(searchInfo.Path);
            this.WriteMessage(LogGen.GetAllItemsFromDopdownMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String value = WebAppState.JSEngine.EvaluateScript(script).ToString();
            String[] options = value.Split(';');
            foreach (String opt in options)
            {
                String[] tokens = opt.Split('}');
                if (tokens.Length != 3)
                {
                    continue;
                }

                if (tokens[0].Trim().Length == 0 && tokens[2].Trim().Length == 0)
                {
                    continue;
                }

                DropDownItem item = new DropDownItem();
                item.Value = tokens[0].Trim();
                item.Title = tokens[1].Trim();
                item.Name = tokens[2].Trim();
                items.Add(item);
            }
            return items;
        }

        public int GetSelectedIndex(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectedIndexMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSelectedIndexScript(searchInfo.Path);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? -1 : int.Parse(obj.ToString());
        }

        public String GetSelectedItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSelectedValueScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? "" : value.ToString();
        }

        public void SelectItem(String item, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectItemMessage(ControlName, item));
            this.CheckExistence(retry, throwOnFail);
            SelectByValue(item, retry, throwOnFail);
        }

        public void SelectItemByIndex(int index, ControlRetry retry,bool throwOnFail)
        {
            this.WriteMessage(LogGen.GetSelectItemByIndexMessage(ControlName, index));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetSelectByIndexScript(searchInfo.Path, index);
            WebAppState.JSEngine.ExecuteScript(script);
        }

        public String GetAllItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetAllItemsFromDopdownMessage(ControlName));
            this.CheckExistence(retry, throwOnFail);
            String script = JQBuilder.GetAllDDValuesScript(searchInfo.Path);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? "" : value.ToString();
        }

        private void SelectByValue(String value, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            bool firstLoop = true;
            bool success = false;
            this.DisableLogging = true;
            for (int i = 0; i < retry.GetHashCode(); i++)
            {
                String script = JQBuilder.GetSelectValueScript(searchInfo.Path, value);
                WebAppState.JSEngine.ExecuteScript(script);
                String s = this.GetSelectedItem(ControlRetry.Super, false);
                if (s == null || !s.Trim().Equals(value))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    success = true;
                    break;
                }
                firstLoop = false;
            }
            this.DisableLogging = false;
            if (throwOnFail)
            {
                Assert.IsTrue(success, String.Format("Failed to select Item:'%s' in '%s', check the ui" +
                        " DD may be readonly or no items in DD .", value, this.searchInfo.Name));
            }
        }

        private void SelectByText(String value, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            bool firstLoop = true;
            bool success = false;
            this.DisableLogging = true;
            for (int i = 0; i < retry.GetHashCode(); i++)
            {
                String script = JQBuilder.GetSelectItemByTextScript(searchInfo.Path, value);
                WebAppState.JSEngine.ExecuteScript(script);
                DropDownItem item = GetSelectedDDItem(ControlRetry.Super, false);
                if (item == null || !item.Name.Trim().Equals(value))
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    success = true;
                    break;
                }
                firstLoop = false;
            }
            this.DisableLogging = false;
            if (throwOnFail)
            {
                Assert.IsTrue(success, String.Format("Failed to select Item:'%s' in '%s', check the ui" +
                        " DD may be readonly or no items in DD .", value, this.searchInfo.Name));
            }
        }

        //Added on 01/16/15
        public string getComboValue(string val, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            //Selecting a value from Combo box
            string eventName = "find('option:contains(" + val + ")')";
            if (this.enableBaseLogging)
            {
                this.WriteMessage(LogGen.GetFireEventMessage(ControlName, eventName));
            }
            this.CheckVisibility(ControlRetry.TooWorst, true);
            String script = JQBuilder.GetFireEventScript(ControlSearchInfo.Path, eventName);
            //Removing the Trigger to make the fire element take jQuery parameters automatically
            string badstring = "trigger('" + eventName + "')";
            script = script.Replace(badstring, eventName);
            script = JQBuilder.GetSelectedValueScript(script);
            Object obj = WebAppState.JSEngine.EvaluateScript(script);
            return obj == null ? "" : obj.ToString();
        }

        public void SelectItemByText(IDropDown Control, string VisibleText)
        {
            throw new NotImplementedException();
        }
    }
}
