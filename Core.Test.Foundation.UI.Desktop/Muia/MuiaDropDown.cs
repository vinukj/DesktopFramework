
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Muia;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    using Microsoft.VisualStudio.TestTools.UITesting;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Core.Test.Foundation.Logger;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Muia;

    /// <summary>
    /// Represents MUAI Drop down (Wrapper on top of AutomationElement)
    /// </summary>
    public class MuiaDropDown : MuiaControl, IMuiaDropDown
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaDropDown" /> class.
        /// </summary>
        /// <param name="nativeElement">Native Automation element</param>
        public MuiaDropDown(AutomationElement nativeElement,string ControlName) // Added the Control Name
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();

            this.NativeElement = nativeElement;

            this.ControlName = ControlName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaDropDown"/> class.
        /// </summary>
        public MuiaDropDown()
        {
        }

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
        /// Selects the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="retry">The retry.</param>
        /// <param name="throwOFail">Throw exception if the control is not visible</param>
        public void SelectItemByIndex(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled) //
            {
                this.WriteMessage(LogGen.GetSelectItemByIndexMessage(ControlName,index));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            AutomationPattern automationPatternFromElement = MuiaElement.GetSpecifiedPattern(NativeElement, "ExpandCollapsePatternIdentifiers.Pattern");

            ExpandCollapsePattern expandCollapsePattern = NativeElement.GetCurrentPattern(automationPatternFromElement) as ExpandCollapsePattern;

            expandCollapsePattern.Expand();

            expandCollapsePattern.Collapse();

            AutomationElement listItem = NativeElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, this.GetAllItems(ControlRetry.Avg, true)[index]));

            automationPatternFromElement = MuiaElement.GetSpecifiedPattern(listItem, "SelectionItemPatternIdentifiers.Pattern");

            SelectionItemPattern selectionItemPattern = listItem.GetCurrentPattern(automationPatternFromElement) as SelectionItemPattern;

            selectionItemPattern.Select();
        }

        /// <summary>
        /// Selects the item.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="retry">The retry.</param>
        /// <param name="throwOFail">Throw exception if the control is not visible</param>
        public void SelectItem(string value, ControlRetry retry = ControlRetry.Avg, bool throwOFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetSelectItemMessage(ControlName,value));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOFail);

            this.DisableLogging = false;

            AutomationPattern automationPatternFromElement = MuiaElement.GetSpecifiedPattern(NativeElement, "ExpandCollapsePatternIdentifiers.Pattern");

            ExpandCollapsePattern expandCollapsePattern = NativeElement.GetCurrentPattern(automationPatternFromElement) as ExpandCollapsePattern;

            expandCollapsePattern.Expand();

            expandCollapsePattern.Collapse();

            AutomationElement listItem = NativeElement.FindFirst(TreeScope.Subtree, new PropertyCondition(AutomationElement.NameProperty, value));

            automationPatternFromElement = MuiaElement.GetSpecifiedPattern(listItem, "SelectionItemPatternIdentifiers.Pattern");

            SelectionItemPattern selectionItemPattern = listItem.GetCurrentPattern(automationPatternFromElement) as SelectionItemPattern;

            selectionItemPattern.Select();
        }

        /// <summary>
        /// Gets the selected item.
        /// </summary>
        /// <param name="retry">The retry.</param>
        /// <param name="throwOFail">Throw exception if the control is not visible</param>
        /// <returns>Returns selected item</returns>
        public string GetSelectedItem(ControlRetry retry = ControlRetry.Avg, bool throwOFail = true)
        {
            if (TestLogger.LoggerEnabled) //
            {
                this.WriteMessage(LogGen.GetSelectedItemMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOFail);

            this.DisableLogging = false;

            SelectionPattern pattern = NativeElement.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;

            AutomationElement[] selectedItems = pattern.Current.GetSelection();

            return MuiaElement.GetProperty(selectedItems[0], MuiaElement.ConvertToAutomationProperty(MuiaElementProperty.Name));
        }       

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <param name="retry">The retry.</param>
        /// <param name="throwOFail">Throw exception if the control is not visible</param>
        /// <returns>Returns list of items(Strings)</returns>
        public string GetAllItems(ControlRetry retry = ControlRetry.Avg, bool throwOFail = true)
        {
           string items = string.Empty;

           AutomationElement comboBox = this.Search(retry, throwOFail);

           List<string>  itemList = this.GetDDItems(comboBox);

           foreach (string s in itemList)
           {
                items = items+s + ",";
           }              

            return items;
        }

        /// <summary>
        /// Waits for items.
        /// </summary>
        /// <param name="defaultItemCount">The default item count.</param>
        /// <param name="retry">The retry.</param>
        /// <param name="throwOFail">if set to <c>true</c> [throw O fail].</param>
        /// <returns><c>true</c> if items gets populated, <c>false</c> otherwise</returns>
        /// <exception cref="System.NotImplementedException">Not implemented</exception>
        public bool WaitForItems(int defaultItemCount = 0, ControlRetry retry = ControlRetry.Avg, bool throwOFail = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the DD items.
        /// </summary>
        /// <param name="comboBox">The combo box.</param>
        /// <returns>Returns list of items(Strings)</returns>
        private List<string> GetDDItems(AutomationElement comboBox)
        {
            AutomationElementCollection col = comboBox.FindAll(TreeScope.Subtree, new PropertyCondition(AutomationElement.IsSelectionItemPatternAvailableProperty, true));

            List<string> items = new List<string>();

            AutomationPattern automationPatternFromElement = MuiaElement.GetSpecifiedPattern(comboBox, "ExpandCollapsePatternIdentifiers.Pattern");

            AutomationPattern[] supportedPattern = comboBox.GetSupportedPatterns();

            ExpandCollapsePattern expandCollapsePattern = comboBox.GetCurrentPattern(automationPatternFromElement) as ExpandCollapsePattern;

            expandCollapsePattern.Expand();

            expandCollapsePattern.Collapse();

            foreach (AutomationElement element in col)
            {
                string item = MuiaElement.GetProperty(element, AutomationElement.NameProperty);

                if (!string.IsNullOrEmpty(item))
                {
                    items.Add(item);
                }
            }

            return items;
        }

        public void SelectItemByText(IDropDown Control, string VisibleText)
        {
            string items = Control.GetAllItems();
            string[] ItemList = items.Split(',');

            for (int i = 1; i < ItemList.Length; i++)
            {
                if (VisibleText.Equals(Control.GetSelectedItem().TrimEnd()))
                    break;
                Keyboard.SendKeys("{UP}");
            }
            for (int q = 0; q < ItemList.Length; q++)
            {
                if (VisibleText.Equals(Control.GetSelectedItem().TrimEnd()))
                    break;
                Keyboard.SendKeys("{DOWN}");
            }
        }        
    }
}
