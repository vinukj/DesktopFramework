
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;

    /// <summary>
    /// Represents a check box control to test the user interface (UI) of Windows
    /// Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFCheckBox : WPFControl, IWpfCheckBox
    {
        /// <summary>
        /// Gets the native checkbox
        /// </summary>
        private WpfCheckBox CheckBox
        {
            get
            {
                return this.NativeElement as WpfCheckBox;
            }
        }

        /// <summary>
        /// Check the checkbox
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Check(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetCheckCheckBoxMessage(ControlName));
            }

            this.DisableLogging = true;

            if (this.IsVisible(ControlRetry.Avg, throwOnFail))
            {
                if (!this.CheckBox.Checked)
                {
                    this.CheckBox.EnsureClickable();

                    this.CheckBox.Checked = true;
                }
            }

            this.DisableLogging = false;
        }

        /// <summary>
        /// Un-Check the checkbox
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void UnCheck(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetUnCheckCheckBoxMessage(ControlName));
            }

            this.DisableLogging = true;

            if (this.IsVisible(ControlRetry.Avg, throwOnFail))
            {
                if (this.CheckBox.Checked)
                {
                    this.CheckBox.EnsureClickable();

                    this.CheckBox.Checked = false;
                }
            }

            this.DisableLogging = false;
        }

        /// <summary>
        /// Returns a value which indicates whether checkbox is checked or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns true if the checkbox is checked</returns>
        public bool IsChecked(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetCheckBoxIsCheckedMessage(ControlName));
            }

            this.DisableLogging = true;

            bool chcked = false;

            if (this.IsVisible(ControlRetry.Avg, throwOnFail))
            {
                this.CheckBox.EnsureClickable();

                chcked = this.CheckBox.Checked;
            }

            this.DisableLogging = false;

            return chcked;
        }
    }
}
