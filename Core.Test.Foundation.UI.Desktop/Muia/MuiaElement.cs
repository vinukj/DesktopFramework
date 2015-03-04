
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Core.Test.Foundation.UI.Contracts; 

    /// <summary>
    /// Represents a Utility class for AutomationElement
    /// </summary>
    public class MuiaElement
    {
        /// <summary>
        /// Searches the element.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <param name="parent">The parent.</param>
        /// <param name="retry">The retry.</param>
        /// <returns>Returns AutomationElement.</returns>
        public static AutomationElement SearchElement(Dictionary<MuiaElementProperty, string> properties, AutomationElement parent = null, ControlRetry retry = ControlRetry.Avg)
        {
            if (parent == null)
            {
                parent = AutomationElement.RootElement;
            }

            int index = 0;

            if (properties.ContainsKey(MuiaElementProperty.Instance))
            {
                index = int.Parse(properties[MuiaElementProperty.Instance]);
            }

            List<PropertyCondition> add = GetCondition(properties);

            AutomationElement element = null;

            int count = retry.GetHashCode() % 1000;

            for (int i = 0; i < count; i++)
            {
                if (index == 0)
                {
                    element = parent.FindFirst(TreeScope.Descendants, add[0]);
                }
                else
                {
                    if (add.Count > 1)
                    {
                        element = parent.FindAll(TreeScope.Descendants, new AndCondition(add.ToArray()))[index];
                    }
                    else
                    {
                        element = parent.FindAll(TreeScope.Descendants, add[0])[index];
                    }
                }

                if (element == null && count > 1)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            return element;
        }

        /// <summary>
        /// Gets the specified pattern.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="patternName">Name of the pattern.</param>
        /// <returns>Returns AutomationPattern.</returns>
        public static AutomationPattern GetSpecifiedPattern(AutomationElement element, string patternName)
        {
            AutomationPattern[] supportedPattern = element.GetSupportedPatterns();

            foreach (AutomationPattern pattern in supportedPattern)
            {
                if (pattern.ProgrammaticName == patternName)
                {
                    return pattern;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the property.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="property">The property.</param>
        /// <returns>Returns the property value</returns>
        public static string GetProperty(AutomationElement element, AutomationProperty property)
        {
            try
            {
                object obj = element.GetCurrentPropertyValue(property);

                if (obj != null)
                {
                    return obj.ToString();
                }
            }
            catch 
            { 
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the properties.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>Returns the all the property values</returns>
        public static string GetProperties(AutomationElement element)
        {
            AutomationProperty[] ps = element.GetSupportedProperties();

            string properties = string.Empty;

            foreach (AutomationProperty property in ps)
            {
                try
                {
                    object obj = element.GetCurrentPropertyValue(property);

                    if (obj == null)
                    {
                        continue;
                    }

                    string name = "[" + property.ProgrammaticName.Replace("AutomationElementIdentifiers.", string.Empty) + "].[" + obj.ToString() + "]";

                    properties += name + ",";
                }
                catch 
                { 
                }
            }

            return properties;
        }

        /// <summary>
        /// Clicks the specified element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="mouseClick">if set to <c>true</c> [mouse click].</param>
        public static void Click(object element, bool mouseClick = false)
        {
            if (element is AutomationElement && !mouseClick)
            {
                AutomationElement control = element as AutomationElement;

                InvokePattern invPattern;

                object objPattern;

                control.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern);

                if (objPattern != null && (objPattern as InvokePattern) != null)
                {
                    invPattern = objPattern as InvokePattern;

                    invPattern.Invoke();
                }
                else
                {
                    control.SetFocus();

                    Keyboard.SendKeys("{ENTER}");
                }
            }
            else if (element is AutomationElement && mouseClick)
            {
                AutomationElement control = element as AutomationElement;

                var codeduiEl = UITestControlFactory.FromNativeElement(control, "UIA");

                Mouse.Click(codeduiEl);
            }
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="retry">The retry.</param>
        /// <param name="findFirst">if set to <c>true</c> [find first].</param>
        /// <returns>Returns AutomationElement.</returns>
        public static AutomationElement GetElement(AutomationProperty property, string value, ControlRetry retry = ControlRetry.Avg, bool findFirst = false)
        {
            PropertyCondition condition = new PropertyCondition(property, value);

            AutomationElement element = null;

            int retries = retry.GetHashCode() % 1000;

            for (int i = 0; i < retries; i++)
            {
                var parentElement = AutomationElement.RootElement;

                if (!findFirst)
                {
                    var col = parentElement.FindAll(TreeScope.Descendants, condition);

                    if (col.Count > 0)
                    {
                        element = col[0];
                    }
                }
                else
                {
                    element = parentElement.FindFirst(TreeScope.Descendants, condition);
                }

                if ((element == null) && retries > 1)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            return element;
        }

        /// <summary>
        /// Gets the element.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="retry">The retry.</param>
        /// <param name="findFirst">if set to <c>true</c> [find first].</param>
        /// <returns>Returns AutomationElement.</returns>
        public static AutomationElement GetElement(AutomationProperty property, string value, AutomationElement parentElement = null, ControlRetry retry = ControlRetry.Avg, bool findFirst = false)
        {
            if (parentElement == null)
            {
                parentElement = AutomationElement.RootElement;
            }

            PropertyCondition condition = new PropertyCondition(property, value);

            AutomationElement element = null;

            int retries = retry.GetHashCode() % 1000;

            for (int i = 0; i < retries; i++)
            {
                if (!findFirst)
                {
                    var col = parentElement.FindAll(TreeScope.Descendants, condition);

                    if (col.Count > 0)
                    {
                        element = col[0];
                    }
                }
                else
                {
                    element = parentElement.FindFirst(TreeScope.Descendants, condition);
                }

                if ((element == null) && retries > 1)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            return element;
        }

        /// <summary>
        /// Searches all elements.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="parentElement">The parent element.</param>
        /// <param name="retry">The retry.</param>
        /// <returns>Returns AutomationElementCollection.</returns>
        public static AutomationElementCollection SearchAllElements(AutomationProperty property, string value, AutomationElement parentElement = null, ControlRetry retry = ControlRetry.Avg)
        {
            if (parentElement == null)
            {
                parentElement = AutomationElement.RootElement;
            }

            PropertyCondition condition = new PropertyCondition(property, value);

            AutomationElementCollection element = null;

            int retries = retry.GetHashCode() % 1000;

            for (int i = 0; i <= retries; i++)
            {
                element = parentElement.FindAll(TreeScope.Descendants, condition);

                if ((element == null || element.Count == 0) && retries > 1)
                {
                    System.Threading.Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }

            return element;
        }

        /// <summary>
        /// Converts to automation property.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Returns AutomationProperty.</returns>
        internal static AutomationProperty ConvertToAutomationProperty(MuiaElementProperty name)
        {
            switch (name)
            {
                case MuiaElementProperty.AutomationId:
                    return AutomationElement.AutomationIdProperty;
                case MuiaElementProperty.Name:
                    return AutomationElement.NameProperty;
                case MuiaElementProperty.Class:
                    return AutomationElement.ClassNameProperty;
                case MuiaElementProperty.Type:
                    return AutomationElement.ControlTypeProperty;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Gets the condition.
        /// </summary>
        /// <param name="properties">The properties.</param>
        /// <returns>Returns the list of property condition.</returns>
        private static List<PropertyCondition> GetCondition(Dictionary<MuiaElementProperty, string> properties)
        {
            List<PropertyCondition> cond = new List<PropertyCondition>();

            foreach (KeyValuePair<MuiaElementProperty, string> keyVal in properties)
            {
                if (keyVal.Key != MuiaElementProperty.Instance)
                {
                    cond.Add(new PropertyCondition(ConvertToAutomationProperty(keyVal.Key), keyVal.Value));
                }
            }

            return cond;
        }
    }
}
