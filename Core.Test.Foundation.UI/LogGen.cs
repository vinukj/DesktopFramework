
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI
{
    public class LogGen
    {
        public static string GetCurrentValueMessage(string controlName)
        {
            return string.Format("Getting the value from the '{0}' control", controlName);
        }

        public static string GetIncreaseValueMessage(string controlName, string value)
        {
            return string.Format("Increase the value of the '{0}' control by {1}", controlName, value);
        }

        public static string GetDecreaseValueMessage(string controlName, double value)
        {
            return string.Format("Decrease the value of the '{0}' control by {1}", controlName, value);
        }

        public static string geMoveMouseMessage(string controlName)
        {
            return string.Format("Moving mouse on '{0}' control", controlName);
        }

        public static string GetPageLoadedMessage(string pageName)
        {
            return string.Format("Checking whether the '{0}' is loaded or not", pageName);
        }

        public static string GetGetUniqueIdMessage(string controlname)
        {
            return string.Format("Generating unique id for '{0}' control", controlname);
        }

        public static string GetGetAttributeMessage(string controlname, string attrName)
        {
            return string.Format("Getting attribute='{0}' from '{1}' control", attrName, controlname);
        }

        public static string GetSetAttributeMessage(string controlname, string attrName, string attrValue)
        {
            return string.Format("Setting {0}='{1}' to '{2}' control", attrName, attrValue, controlname);
        }

        public static string GetGetRowDataMessage(string controlName, int index)
        {
            return string.Format("Get all TD value at row index:{0} from '{1}' control", index, controlName);
        }

        public static string GetGetRowDataMessage(string controlName, string data)
        {
            return string.Format("Get all TD value where row contains string:'{0}' from '{1}' control", data, controlName);
        }

        public static string GetClickTableCellMessage(string controlName, string data)
        {
            return string.Format("Clicking on table('{0}') cell where row contains '{1}'", controlName, data);
        }

        public static string GetSearchTableMessage(string controlName, string data)
        {
            return string.Format("Searching data:'{0}' in '{1}' control", data, controlName);
        }

        public static string GetSelectTableRowMessage(string controlName, int rowIndex)
        {
            return string.Format("Selecting row in '{0}' at row index:'{1}'", controlName, rowIndex);
        }

        public static string GetGetRowCountMessage(string controlName)
        {
            return string.Format("Getting row count from '{0}' table", controlName);
        }

        public static string GetControlVisibleMessage(string controlName, ControlRetry retry)
        {
            return string.Format("'{0}' control is visible even after {1} seconds, please increase the control retry.", controlName, retry.GetHashCode());
        }

        public static string GetControlNotVisibleMessage(string controlName, ControlRetry retry)
        {
            return string.Format("'{0}' control is not visible even after {1} seconds, please verify control attribute or increase the control retry.", controlName, retry.GetHashCode());
        }

        public static string GetControlNotExistMessage(string controlName, ControlRetry retry)
        {
            return string.Format("'{0}' control is not exist even after {1} seconds, please verify control attribute or increase the control retry.", controlName, retry.GetHashCode());
        }

        public static string GetClickMessage(string controlName)
        {
            return string.Format("Clicking on '{0}' control", controlName);
        }

        public static string GetFailedToClickMessage(string controlName)
        {
            return string.Format("Failed to click on '{0}' control", controlName);
        }

        public static string GetInnerHtmlMessage(string controlName)
        {
            return string.Format("Getting inner html of the '{0}' control", controlName);
        }

        public static string GetInnerTextMessage(string controlName)
        {
            return string.Format("Getting inner text of the '{0}' control", controlName);
        }

        public static string GetFireEventMessage(string controlName, string eventName)
        {
            return string.Format("Firing '{0}' event on '{1}' control", eventName, controlName);
        }

        public static string GetVisibleMessage(string controlName)
        {
            return string.Format("Checking whether '{0}' control is visible or not", controlName);
        }

        public static string GetWaitingForDisappearMessage(string controlName)
        {
            return string.Format("Waiting for '{0}' control to Disappear", controlName);
        }

        public static string GetWaitingForDataMessage(string controlName)
        {
            return string.Format("Waiting for data in '{0}' control", controlName);
        }

        public static string GetDataNotLoadedMessage(string controlName, ControlRetry retry)
        {
            return string.Format("Data is not loaded in '{0}' control even after {1} seconds", controlName, retry.GetHashCode());
        }


        public static string GetExistMessage(string controlName)
        {
            return string.Format("Checking whether '{0}' control is exist or not", controlName);
        }

        public static string GetEnabledMessage(string controlName)
        {
            return string.Format("Checking whether '{0}' control is enabled or not", controlName);
        }

        public static string GetSendKeysMessage(string keys, string controlName)
        {
            return string.Format("Sending keys('{0}') to '{1}' control is enabled or not", keys, controlName);
        }

        public static string GetLocationMessage(string controlName)
        {
            return string.Format("Getting location of '{0}' control", controlName);
        }

        public static string GetSetTextMessage(string controlName, string text)
        {
            return string.Format("Setting text:'{0}' to '{1}' control", text, controlName);
        }

        public static string GetGetTextMessage(string controlName)
        {
            return string.Format("Getting text from '{0}' control", controlName);
        }

        public static string GetErrorMessage(string controlName)
        {
            return string.Format("Getting error message from '{0}' control", controlName);
        }

        public static string GetFocusMessage(string controlName)
        {
            return string.Format("Focus on '{0}' control", controlName);
        }

        public static string GetToolTipMessage(string controlName)
        {
            return string.Format("Getting tool tip from '{0}' control", controlName);
        }

        public static string GetPropertiesMessage(string controlName, string property)
        {
            return string.Format("Get the specified property from the give hierarchy, First Control='{0}', Property name='{1}'", controlName, property);
        }

        public static string GetCountMessage(string name)
        {
            return string.Format("Counting number of instance of '{0}'", name);
        }

        public static string GetSearchMessage()
        {
            return "Searching for multiple data in given hierarchy";
        }

        public static string GetSelectedIndexMessage(string controlName)
        {
            return string.Format("Getting selected index from '{0}' control", controlName);
        }

        public static string GetSelectedItemMessage(string controlName)
        {
            return string.Format("Getting selected item from '{0}' control", controlName);
        }

        public static string GetSelectItemMessage(string controlName, string item)
        {
            return string.Format("Selecting item:'{0}' from '{1}' control", item, controlName);
        }

        public static string GetCheckCheckBoxMessage(string controlName)
        {
            return string.Format("Checking checkbox '{0}' control", controlName);
        }

        public static string GetCheckRadioButtonMessage(string controlName)
        {
            return string.Format("Selecting radio button '{0}' control", controlName);
        }

        public static string GetIsSelectedRadioButtonMessage(string controlName)
        {
            return string.Format("Checking whether radio button '{0}' control is selected or not", controlName);
        }

        public static string GetUnCheckCheckBoxMessage(string controlName)
        {
            return string.Format("Un-Checking checkbox '{0}' control", controlName);
        }

        public static string GetSelectItemByIndexMessage(string controlName, int index)
        {
            return string.Format("Selecting item at index:'{0}' in '{1}' control", index, controlName);
        }

        public static string GetAllItemsFromDopdownMessage(string controlName)
        {
            return string.Format("Getting all items from '{0}' control", controlName);
        }

        public static string failToLoadItemMessage(string controlName)
        {
            return string.Format("Item are not loaded in '{0}' control with in expected time", controlName);
        }

        public static string GetCheckBoxIsCheckedMessage(string controlName)
        {
            return string.Format("Checking whether '{0}' control is checked or not", controlName);
        }

        public static string GetCheckBoxCheckMessage(string controlName)
        {
            return string.Format("Check '{0}' control", controlName);
        }

        public static string GetCheckBoxUnCheckMessage(string controlName)
        {
            return string.Format("UnCheck '{0}' control", controlName);
        }

        public static string GetSelectTabMessage(string controlName, int tabIndex)
        {
            return string.Format("Selecting tab at index '{0}' in '{1}' control", tabIndex, controlName);
        }

        public static string GetIsExpandableMessage(string controlName)
        {
            return string.Format("Checking whether '{0}' control is Expandable or not", controlName);
        }

        public static string GetExpandMessage(string controlName)
        {
            return string.Format("Expanding the '{0}' control", controlName);
        }

        public static string GetCollapseMessage(string controlName)
        {
            return string.Format("Collapsing the '{0}' control", controlName);
        }

        public static string GetItemCountMessage(string controlName)
        {
            return string.Format("Counting the Item count of  the '{0}' control", controlName);
        }

        public static string GetItemExistMessage(string controlName, string item)
        {
            return string.Format("Checking whether '{0}' item exit in '{1}' control is exist or not", item, controlName);
        }
    }
}
