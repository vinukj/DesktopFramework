
namespace Core.Test.Foundation.UI.Desktop
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Automation;
    using Core.Test.Foundation.UI.Contracts;

    /// <summary>
    /// Represents extension methods(Custom controls and ENUM)
    /// </summary>
    public static class ControlExtension
    {        

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>ENUM description attribute</returns>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());

            var attribute
                    = Attribute.GetCustomAttribute(field, typeof(System.ComponentModel.DescriptionAttribute))
                        as System.ComponentModel.DescriptionAttribute;

            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// To the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns>List of AutomationElement.</returns>
        public static List<AutomationElement> ToList(this AutomationElementCollection list)
        {
            var elements = new List<AutomationElement>();

            foreach (AutomationElement obj in list)
            {
                elements.Add(obj);
            }

            return elements;
        }
    }
}
