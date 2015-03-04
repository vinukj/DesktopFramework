
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.WinForms;

namespace Core.Test.Foundation.UI.Desktop.WinForms
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.WinForms;

    /// <summary>
    ///  Represents a button control to test the user interface (UI) of Windows Presentation
    ///  Foundation (WPF) applications.
    /// </summary>
    public class WinButton : WinControl, IWinButton
    {
        /// <summary>
        /// Gets the display text 
        /// </summary>
        public string Text
        {
            get 
            {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetSetTextMessage(ControlName, "Setting the value to testbox"));
                }

                this.DisableLogging = true;

                string text = string.Empty;

                if (this.IsVisible(ControlRetry.Avg, true))
                {
                    text = this.Button.DisplayText;
                }

                this.DisableLogging = false;

                return text;
            }
        }

        /// <summary>
        /// Gets the native button element
        /// </summary>
        private WpfButton Button
        {
            get
            {
                return this.NativeElement as WpfButton;
            }
        }

        /// <summary>
        /// Ensures that the control is clickable by scrolling if necessary.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void EnsureClickable(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetClickMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.EnsureClickable();
        }
    }
}
