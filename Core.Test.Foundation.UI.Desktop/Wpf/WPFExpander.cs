
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Wpf;
    /// <summary>
    ///  Represents an expander control to test the user interface (UI) of Windows
    ///     Presentation Foundation (WPF) applications.
    /// </summary>
    public class WPFExpander : WPFControl, IWpfExpander
    {
        /// <summary>
        /// Gets a value indicating whether the expander is expanded or not.
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                if (this.EnableLogging)
                {

                    this.WriteMessage(LogGen.GetIsExpandableMessage(ControlName));
                }

                this.DisableLogging = true;

                this.IsVisible(ControlRetry.Avg, true);

                this.DisableLogging = false;

                return this.Expander.Expanded;
            }
        }

        /// <summary>
        /// Gets the native expander instance.
        /// </summary>
        private WpfExpander Expander
        {
            get
            {
                return this.NativeElement as WpfExpander;
            }
        }

        /// <summary>
        /// Expand the expander.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Expand(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetExpandMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.Expander.Expanded = true;
        }

        /// <summary>
        /// Collapse the expander
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Collapse(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetCollapseMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.Expander.Expanded = false;
        }
    }
}