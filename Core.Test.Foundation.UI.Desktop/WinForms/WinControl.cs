
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.WinForms;
using Core.Test.Foundation.UI.Desktop.Muia;

namespace Core.Test.Foundation.UI.Desktop.WinForms
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Automation;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.WinForms;
    using Core.Test.Foundation.UI.Desktop.Muia;
    using MouseButtons = System.Windows.Forms.MouseButtons;// for the Mouse Right Click

    /// <summary>
    /// Represents a Base WPF control to test the user interface (UI) of Windows Presentation Foundation (WPF) applications.
    /// </summary>
    public class WinControl : IWinControl
    {
        /// <summary>
        /// Gets or sets a native UI test Control
        /// </summary>
        internal UITestControl NativeElement { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled or not.
        /// </summary>
        public bool EnableLogging { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether base logging is enabled or not.
        /// </summary>
        public bool DisableLogging { get; set; }

        /// <summary>
        /// Gets or sets the control name.
        /// </summary>
        public string ControlName { get; set; }

        /// <summary>
        /// Returns a collection of all first level children of the current WPFControl
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns a collection of all first level children</returns>
        public IList<IWinControl> GetChilds(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            List<IWinControl> childs = new List<IWinControl>();

            var elements = this.NativeElement.GetChildren();

            int idx = 0;

            foreach (var c in elements)
            {
                idx++;

                IWinControl controls = controls = DesktopControlFactory.CreateWin<WinControl>(c, "Child-" + idx);

                controls.DisableLogging = true;

                childs.Add(controls);
            }

            return childs;
        }

        /// <summary>
        /// Click on the control without using mouse.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void CustomClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            AutomationElement native = this.NativeElement.NativeElement as AutomationElement;

            MuiaControl control = new MuiaControl(native, ControlName);// Added the control name parameter

            control.Click();
        }

        /// <summary>
        /// Click on the control using control coordinates.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void ClickByPoint(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            Point p = this.NativeElement.BoundingRectangle.Location;

            Mouse.Click(p);
        }

        /// <summary>
        /// Click on the control
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Click(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.EnsureClickable();

            Mouse.Click(this.NativeElement);
        }

        /// <summary>
        /// Click the control using mouse
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void ClickByMouse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.EnsureClickable();

            Mouse.Click(this.NativeElement);
        }

        /// <summary>
        /// Double Click the control using mouse
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void DoubleClick(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.EnsureClickable();

            Mouse.DoubleClick(this.NativeElement);
        }

        /// <summary>
        /// Check whether control is visible or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the control is visible</returns>
        public bool IsVisible(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = false)
        {
            bool visible = false;

            try
            {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetVisibleMessage(ControlName));
                }

                visible = this.NativeElement.WaitForControlExist(retry.GetHashCode());

                if (visible)
                {
                    this.NativeElement.WaitForControlEnabled(retry.GetHashCode());

                    visible = this.NativeElement.Exists;
                }
            }
            catch
            {
            }

            if (!visible && throwOnFail)
            {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetVisibleMessage(ControlName));
                }
            }

            return visible;
        }

        /// <summary>
        /// Check whether control is exist or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the control exist</returns>
        public bool Exist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = false)
        {
            bool visible = false;

            try
            {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetExistMessage(ControlName));
                }

                visible = this.NativeElement.WaitForControlExist(retry.GetHashCode());
            }
            catch
            {
            }

            if (!visible && throwOnFail)
            {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetExistMessage(ControlName));
                }
            }

            return visible;
        }

        /// <summary>
        /// Check whether control is enabled or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the control enabled</returns>
        public bool Enabled(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = false)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetEnabledMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return this.NativeElement.Enabled;
        }

        /// <summary>
        /// Get the specified attribute or Property like Class name, Name or Automation ID etc.
        /// </summary>
        /// <param name="attributeName">Attribute name</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the specified attribute.</returns>
        public string GetAttribute(string attributeName, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetGetAttributeMessage(ControlName, attributeName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            object val = null;

            try
            {
                val = this.NativeElement.GetProperty(attributeName);
            }
            catch
            {
            }

            return val == null ? string.Empty : val.ToString();
        }

        /// <summary>
        /// Set the specified attributer value.
        /// </summary>
        /// <param name="attrName">Attribute name</param>
        /// <param name="attrValue">Attribute value</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SetAttribute(string attrName, string attrValue, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSetAttributeMessage(ControlName, attrName, attrValue));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.SetProperty(attrName, attrValue);
        }

        /// <summary>
        /// Focus on the control
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Focus(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetFocusMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.SetFocus();
        }

        /// <summary>
        /// Wait for control to disappear
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the control disappeared</returns>
        public bool WaitForControlNotExist(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetControlNotExistMessage(ControlName, retry));
            }

            this.DisableLogging = true;

            this.IsVisible(ControlRetry.Avg, false);

            this.DisableLogging = false;

            return this.NativeElement.WaitForControlNotExist(retry.GetHashCode());
        }

        /// <summary>
        /// Get the native element
        /// </summary>
        /// <returns>Returns the native element</returns>
        public object GetNativeElement()
        {
            return this.NativeElement;
        } 

        /// <summary>
        /// Returns the control coordinates on the screen
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the control coordinate.</returns>
        public Point GetPoint(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.Exist(retry, throwOnFail);

            return this.NativeElement.BoundingRectangle.Location;
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
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.EnsureClickable();

            Mouse.Click(this.NativeElement, MouseButtons.Right);
        }
    }
}
