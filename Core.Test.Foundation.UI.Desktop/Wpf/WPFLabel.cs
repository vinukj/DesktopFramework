
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;
    

    /// <summary>
    /// Represents a WPF Label to test the user interface (UI) of Windows Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFLabel : WPFControl, IWpfLabel
    {
        /// <summary>
        /// Gets the display text from label
        /// </summary>
        public string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
                if (this.EnableLogging)
                {
                    this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
                }

                this.DisableLogging = true;

                string text = string.Empty;

                if (this.IsVisible(retry, true))
                {
                    text = this.Label.DisplayText;
                }

                this.DisableLogging = false;

                return text;            
        }

        /// <summary>
        /// Gets the native label control
        /// </summary>
        private WpfText Label
        {
            get
            {
                return this.NativeElement as WpfText;
            }
        }
    }
}