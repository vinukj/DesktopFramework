
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Muia;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI;
    using Core.Test.Foundation.UI.Contracts.Muia;
    using Core.Test.Foundation.Logger;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    using System.Windows;
    using System.Drawing;// for the Mouse Right Click
    /// <summary>
    /// Represents MUAI Control (Wrapper on top of AutomationElement)
    /// </summary>
    public class MuiaControl : IMuiaControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaControl"/> class.
        /// </summary>
        /// <param name="nativeElement">Native Automation element</param>
        public MuiaControl(AutomationElement nativeElement,string ControlName) // Added the Control Name Parameter
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();

            this.NativeElement = nativeElement;

            this.ControlName = ControlName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaControl"/> class.
        /// </summary>
        public MuiaControl()
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();
        }

        /// <summary>
        /// Gets or sets the parent
        /// </summary>
        public object Parent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled or not.
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Gets or sets the control name.
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// Gets or sets the native element.
        /// </summary>
        internal AutomationElement NativeElement { get; set; }

        /// <summary>
        /// Gets or sets the control search option
        /// </summary>
        internal Dictionary<MuiaElementProperty, string> SearchOptions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether base logging is enabled or not.
        /// </summary>
        public bool DisableLogging { get; set; }

        /// <summary>
        /// Click on Control and Expand the Item as well
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        public void Click(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
          //  this.EnableLogging = true;
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.IsVisible(retry, throwOnFail);

            InvokePattern invPattern;
            ExpandCollapsePattern expPattern;
            object objPattern;

            this.NativeElement.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern);
            if (objPattern != null && (objPattern as InvokePattern) != null)
            {
                invPattern = objPattern as InvokePattern;

                invPattern.Invoke();
            }
            else if (true == this.NativeElement.TryGetCurrentPattern(ExpandCollapsePattern.Pattern, out objPattern))
            {
                expPattern = objPattern as ExpandCollapsePattern;
                expPattern.Expand();

            }
            else
            {
                this.NativeElement.SetFocus();
                Keyboard.SendKeys("{ENTER}");
            }
        }

        /// <summary>
        /// Click on Control by mouse
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        public void ClickByMouse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            //this.Click(retry, throwOnFail);
            this.IsVisible(retry, throwOnFail);
            UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA"); // Added By Amit Tiwari
            Mouse.Click(Control);// Added By Amit Tiwari
            
        }

        /// <summary>
        /// Double Click on Control by mouse
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        public void DoubleClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            //if (TestLogger.LoggerEnabled)
            //{
            //    this.WriteMessage(LogGen.GetClickMessage(ControlName));
            //}

            //this.IsVisible(retry, throwOnFail);

            //this.NativeElement.SetFocus();

            //Keyboard.SendKeys("{ENTER}");

            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            //this.Click(retry, throwOnFail);
            this.IsVisible(retry, throwOnFail);
            UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA"); // Added By Amit Tiwari
            Mouse.DoubleClick(Control);// Added By Amit Tiwari


        }

        /// <summary>
        /// Check whether the control is visible or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns true if control is visible</returns>
        public bool IsVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = false)
        {
            bool visible = false;

            if (TestLogger.LoggerEnabled)
            {
                 this.WriteMessage(LogGen.GetVisibleMessage(ControlName));
            };

            this.Search(retry, throwOnFail);

            if (this.NativeElement != null)
            {
                visible = !this.NativeElement.Current.IsOffscreen;
            }

            return visible;
        }

        /// <summary>
        /// Check whether the control is enabled or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns true if control is exist</returns>
        public bool Exist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = false)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetExistMessage(ControlName));
            }

            return this.IsVisible(retry, throwOnFail);
        }

        /// <summary>
        /// Check whether the control is enabled or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns true if control is enabled</returns>
        public bool Enabled(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetEnabledMessage(ControlName));
            }

            this.IsVisible(retry, throwOnFail);

            return this.NativeElement.Current.IsEnabled;
        }

        /// <summary>
        /// Get the specified attribute
        /// </summary>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns attribute value.</returns>
        public string GetAttribute(string attributeName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.IsVisible(retry, throwOnFail);

            MuiaElementProperty pro = MuiaElementProperty.Name;

            if (!Enum.TryParse<MuiaElementProperty>(attributeName, out pro))
            {
                if (TestLogger.LoggerEnabled) //
                {
                    this.WriteMessage(LogGen.GetGetAttributeMessage(ControlName, attributeName));
                }
            }

            AutomationProperty p = MuiaElement.ConvertToAutomationProperty(pro);

            return MuiaElement.GetProperty(this.NativeElement, p); 
        }

        /// <summary>
        /// Set the attribute, this will throw not implementation exception
        /// </summary>
        /// <param name="attrName">Attribute name</param>
        /// <param name="attrValue">Attribute value</param>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        public void SetAttribute(string attrName, string attrValue, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Focus on the control
        /// </summary>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        public void Focus(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled) //
            {
                this.WriteMessage(LogGen.GetFocusMessage(ControlName));
            }

            this.IsVisible(retry, throwOnFail);

            this.NativeElement.SetFocus();
        }

        /// <summary>
        /// Wait for control to disappear.
        /// </summary>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns true if the control disappears.</returns>
        public bool WaitForControlNotExist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetControlNotExistMessage(ControlName, ControlRetry.Super));
            }

            this.DisableLogging = true;

            bool visible = false;

            int count = retry.GetHashCode() / 1000;

            for (int i = 0; i < count; i++)
            {
                visible = this.IsVisible(ControlRetry.Super, false);

                if (!visible)
                {
                    break;
                }
            }

            this.DisableLogging = false;

            return !visible;
        }

        /// <summary>
        /// Get the native element
        /// </summary>
        /// <returns>Returns native element</returns>
        public object GetNativeElement()
        {
            return this.Search(ControlRetry.Avg, true);
        }

        /// <summary>
        /// Get the first level Childs 
        /// </summary>
        /// <returns>Returns the list of control.</returns>
        public List<MuiaControl> GetChilds()
        {
            List<MuiaControl> controls = new List<MuiaControl>();

            var childs = this.NativeElement.FindAll(TreeScope.Children, new PropertyCondition(AutomationElement.IsControlElementProperty, true)).ToList();

            foreach (AutomationElement els in childs)
            {
                MuiaControl muia = new MuiaControl(els,ControlName); // Added the control name parameter
                controls.Add(muia);
            }

            return controls;
        }

        /// <summary>
        /// Search for the control
        /// </summary>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns the automation element.</returns>
        protected AutomationElement Search(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            bool failed = false;

            try
            {
                AutomationElement parent = null;

                if (this.Parent != null && this.SearchOptions.Count > 0)
                {
                    if (this.Parent is UITestControl)
                    {
                        parent = (this.Parent as UITestControl).NativeElement as AutomationElement;
                    }
                    else
                    {
                        parent = this.Parent as AutomationElement;
                    }

                    this.NativeElement = MuiaElement.SearchElement(this.SearchOptions, parent, retry);
                }

                failed = this.NativeElement == null;
            }
            catch
            {
                failed = false;
            }

            if (throwOnFail && failed)
            {
                this.WriteMessage(LogGen.GetSearchMessage());
            }

            return this.NativeElement;
        }

        protected void WriteMessage(string message)
        {
            UI.LogUtil.WriteMessage(message, this);
        }

        public string GetToolTip(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            return "";
        }

        public string GetErrorString(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            return "";
        }

        public ControlLocation GetLocation(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {           
            ControlLocation location = new ControlLocation();            
            return location;
        }

        public bool WaitForNotVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {            
            bool invisible = false;            
            return invisible;
        }


        public void RightClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }
            //this.Click(retry, throwOnFail);
            this.IsVisible(retry, throwOnFail);
            UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA");
            //Right Click on the Control
            Mouse.Click(Control, MouseButtons.Right);
        }

     public  Rectangle GetBoundingRectangel(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
             UITestControl Control=UITestControlFactory.FromNativeElement(this.NativeElement,"UIA");
             return Control.BoundingRectangle;
        }
    }
}
