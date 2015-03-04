
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;
using Core.Test.Foundation.UI.Desktop.Muia;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using System.Windows.Automation;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;
    using Core.Test.Foundation.UI.Desktop.Muia;

    /// <summary>
    /// Represents a WPF ListBox to test the user interface (UI) of Windows Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFListBox : WPFControl, IWpfListBox
    {
        /// <summary>
        /// Gets the native list box
        /// </summary>
        private WpfList ListBox
        {
            get
            {
                return NativeElement as WpfList;
            }
        }

        /// <summary>
        /// Select an item at the give index.
        /// </summary>
        /// <param name="itemIndex">Item to select</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SelectItem(int itemIndex, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            }

            var native = this.ListBox.NativeElement as AutomationElement;

            var item = MuiaElement.SearchAllElements(AutomationElement.ClassNameProperty, "ListBoxItem", native, retry);

            MuiaElement.Click(item[itemIndex], true);
        }

        /// <summary>
        /// Check whether items are loaded or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if item exist.</returns>
        public bool ItemExist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetExistMessage(ControlName));
            }

            var native = this.ListBox.NativeElement as AutomationElement;

            var item = MuiaElement.GetElement(AutomationElement.ClassNameProperty, "ListBoxItem", native, retry, true);

            return item != null;
        }

        /// <summary>
        /// Get the items count.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if item exist.</returns>
        public int GetItemsCount(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetItemCountMessage(ControlName));
            }

            this.DisableLogging = true;

            int count = 0;

            if (this.IsVisible(retry, throwOnFail))
            {
                AutomationElement ele = this.ListBox.NativeElement as AutomationElement;

                AutomationElementCollection item = MuiaElement.SearchAllElements(AutomationElement.ClassNameProperty, "ListBoxItem", ele, retry);

                if (item != null)
                {
                    count = item.Count;
                }
            }

            this.DisableLogging = false;

            return count;
        }
    }
}
