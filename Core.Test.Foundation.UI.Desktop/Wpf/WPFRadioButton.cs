

using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Wpf;

namespace Core.Test.Foundation.UI.Desktop.Wpf
{
    public class WPFRadioButton : WPFControl, IWpfRadioButton
    {

        /// <summary>
        /// Gets the native Radio Button.
        /// </summary>
        private WpfRadioButton RadioButton 
        {
            get
            {
                return this.NativeElement as WpfRadioButton;
            }
        }

        /// <summary>
        /// Set the value to true for the given radio Button
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void On(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetCheckRadioButtonMessage(this.ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.RadioButton.Selected = true;
        }


        /// <summary>
        /// Set the value of radio Button Off
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void Off(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetCheckRadioButtonMessage(this.ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.RadioButton.Selected = false;
        }


        /// <summary>
        /// Return whether radio Button is On or Off
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public bool IsOn(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (this.EnableLogging)
            {
                this.WriteMessage(LogGen.GetIsSelectedRadioButtonMessage(this.ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            return this.RadioButton.HasFocus;
        }

    }
}
