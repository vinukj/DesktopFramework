
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;

    /// <summary>
    /// Represents a WPF TextBox to test the user interface (UI) of Windows Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFTextBox : WPFControl, IWpfTextBox
    {
        /// <summary>
        /// Gets the native text box.
        /// </summary>
        private WpfEdit TextBox
        {
            get
            {
                return this.NativeElement as WpfEdit;
            }
        }

        /// <summary>
        /// Set the given text to textbox.
        /// </summary>
        /// <param name="text">Text to set.</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SetText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSetTextMessage(ControlName,text));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.TextBox.Text = text;
        }

        /// <summary>
        /// Get the text from text box.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the text.</returns>
        public string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return this.TextBox.Text;
        }

        /// <summary>
        /// Set the given text to textbox.
        /// </summary>
        /// <param name="text">Text to set.</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void TypeText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetSetTextMessage(ControlName, text));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.TextBox.Text = text;
        }
    }
}
