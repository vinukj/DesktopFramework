

using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;
using Core.Test.Foundation.UI.Contracts.Muia;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    using System.Collections.Generic;
    using System.Windows.Automation;
    using Core.Test.Foundation.Logger;
    using Core.Test.Foundation.UI.Contracts;
    using Core.Test.Foundation.UI.Contracts.Muia;

    /// <summary>
    /// Represents MUAI TextBox (Wrapper on top of AutomationElement)
    /// </summary>
    public class MuiaTextBox : MuiaControl, IMuiaTextBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaTextBox"/> class.
        /// </summary>
        public MuiaTextBox()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MuiaTextBox" /> class.
        /// </summary>
        /// <param name="nativeElement">Native Automation element</param>
        public MuiaTextBox(AutomationElement nativeElement,string ControlName)
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();
            this.NativeElement = nativeElement;
            this.ControlName = ControlName;
        }

        /// <summary>
        /// Set the text to textbox
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void SetText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
           //  this.EnableLogging = true;
          
            if (TestLogger.LoggerEnabled) // Replacing the EnableLogging with the LoggerEnabled
            {
                this.WriteMessage(LogGen.GetSetTextMessage(ControlName,text)); 
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.SetFocus();

            ValuePattern editValue = (ValuePattern)this.NativeElement.GetCurrentPattern(ValuePattern.Pattern);

            editValue.SetValue(text);
        }

        /// <summary>
        /// Returns the text.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        /// <returns>Returns the text</returns>
        public string GetText(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            TextPattern p = (TextPattern)this.NativeElement.GetCurrentPattern(TextPattern.Pattern);

            string text = p.DocumentRange.GetText(-1);

            return text;
        }

        /// <summary>
        /// Set the text to textbox
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public void TypeText(string text, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            if (TestLogger.LoggerEnabled)//
            {
                this.WriteMessage(LogGen.GetGetTextMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            this.NativeElement.SetFocus();

            ValuePattern editValue = (ValuePattern)this.NativeElement.GetCurrentPattern(ValuePattern.Pattern);

            editValue.SetValue(text);
        }
    }
}
