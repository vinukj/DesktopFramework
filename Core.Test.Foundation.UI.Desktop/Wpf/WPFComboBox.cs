
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;
using Core.Test.Foundation.UI.Desktop.Muia;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;
    using Core.Test.Foundation.UI.Desktop.Muia;
    using Core.Test.Foundation.Logger;

    /// <summary>
    ///  Represents a combo box control to test the user interface (UI) of Windows Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFComboBox : WPFControl, IWpfComboBox
    {
        /// <summary>
        /// Gets or sets the selected index (Select an item by index)
        /// </summary>
        /// <value>The index of the selected item.</value>
        public int GetSelectedIndex(ControlRetry retry = ControlRetry.Avg, bool throwOFail = true)
        {
            List<string> items = new List<string>(this.GetAllItems(ControlRetry.Avg, true).Split(','));

            string selected = this.GetSelectedItem(ControlRetry.Super, true);

            return items.IndexOf(selected);
        }

        /// <summary>
        /// Gets the Native coded UI dropdown.
        /// </summary>
        private WpfComboBox DropDown
        {
            get
            {
                return this.NativeElement as WpfComboBox;
            }
        }

        /// <summary>
        /// Select specified value.
        /// </summary>
        /// <param name="value">Value to select</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SelectItem(string value, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            }

            this.Select(value, retry, throwOnFail);

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            if (this.DropDown.SelectedItem != value)
            {
                this.WriteMessage(string.Format("___Failed to select value='{0}' in '{1}' control, so re trying again", value, this.ControlName));

                System.Threading.Thread.Sleep(2000);

                this.Select(value, retry, throwOnFail);
            }
        }

        /// <summary>
        /// Returns the selected item
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns> Returns selected item</returns>
        public string GetSelectedItem(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return this.DropDown.SelectedItem;
        } 
        
        /// <summary>
        /// Returns the list of items in the dropdown
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the list of items</returns>
        public string GetAllItems(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetAllItemsFromDopdownMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            AutomationElement el = this.DropDown.NativeElement as AutomationElement;

            MuiaDropDown mdd = new MuiaDropDown(el,ControlName);// Added the Control Name

            mdd.EnableLogging = false;

            return mdd.GetAllItems(retry, throwOnFail);
        }

        /// <summary>
        /// Wait for item to load.
        /// </summary>
        /// <param name="defaultItemCount">Default available items count</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the items gets loaded</returns>
        public bool WaitForItems(int defaultItemCount = 0, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled) //
            {
                this.WriteMessage(LogGen.GetWaitingForDataMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            AutomationElement el = this.DropDown.NativeElement as AutomationElement;

            MuiaDropDown mdd = new MuiaDropDown(el,ControlName); // Added the Control Name

            mdd.EnableLogging = false;

            int retryCount = retry.GetHashCode() / 1000;

            bool itemExist = false;

            for (int idx = 0; idx < retryCount; idx++)
            {
                string items = mdd.GetAllItems(retry, throwOnFail);

                if (items.Split(',').Length < defaultItemCount)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    itemExist = true;

                    break;
                }
            }

            return itemExist;
        }

        /// <summary>
        /// Select specified value.
        /// </summary>
        /// <param name="value">Value to select</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        private void Select(string value, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            if (this.DropDown.SelectedItem != value)
            {
                this.DropDown.SelectedItem = value;
            }
        }

        /// <summary>
        /// Select specified value.
        /// </summary>
        /// <param name="index">index of the item</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SelectItemByIndex(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            
        }

        /// <summary>
        /// Select specified value.
        /// </summary>
        /// <param name="item">check the item</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public  bool ItemExist(string item, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetItemExistMessage(ControlName, item));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return GetAllItems(retry, true).Contains(item);
        }

        /// <summary>
        /// Select specified value.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public int GetItemsCount(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetItemCountMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return GetAllItems(retry, true).Split(',').Length;
        }


        public void SelectItemByText(IDropDown Control, string VisibleText)
        {
            throw new System.NotImplementedException();
        }
    }
}
