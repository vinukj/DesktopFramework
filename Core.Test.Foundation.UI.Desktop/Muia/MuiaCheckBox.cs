using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    public class MuiaCheckBox : MuiaControl, ICheckBox
    {

         /// <summary>
        /// Initializes a new instance of the <see cref="MuiaTextBox" /> class.
        /// </summary>
        /// <param name="nativeElement">Native Automation element</param>
        public MuiaCheckBox(AutomationElement nativeElement, string ControlName)
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();
            this.NativeElement = nativeElement;
            this.ControlName = ControlName;
        }

        public bool IsChecked(ControlRetry retry, bool throwOnFail)
        {
            bool State=false;

            if (TestLogger.LoggerEnabled) // Replacing the EnableLogging with the LoggerEnabled
            {
                this.WriteMessage(LogGen.GetCheckBoxIsCheckedMessage(ControlName));
            }

            this.DisableLogging = true;

            this.IsVisible(retry, throwOnFail);

            this.DisableLogging = false;

            Object objPattern;
            TogglePattern togPattern;
            if (true ==this.NativeElement.TryGetCurrentPattern(TogglePattern.Pattern, out objPattern))
            {
                togPattern = objPattern as TogglePattern;
                State= togPattern.Current.ToggleState == ToggleState.On;
            }
            return State;
        }

       

        public void UnCheck(ControlRetry retry, bool throwOnFail)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetCheckBoxUnCheckMessage(ControlName));
            }
            this.IsVisible(retry, throwOnFail);
            Object objPattern;
            TogglePattern togPattern;
            if (true == this.NativeElement.TryGetCurrentPattern(TogglePattern.Pattern, out objPattern))
            {
                togPattern = objPattern as TogglePattern;
                if (togPattern.Current.ToggleState == ToggleState.On)
                {
                    UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA"); // Added By Amit Tiwari
                    Mouse.Click(Control);// Added By Amit Tiwari
                }
            }  
           
        }


        public void Check(ControlRetry retry, bool throwOnFail)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetCheckCheckBoxMessage(ControlName));
            }
            //this.Click(retry, throwOnFail);
            this.IsVisible(retry, throwOnFail);
            Object objPattern;
            TogglePattern togPattern;
            if (true == this.NativeElement.TryGetCurrentPattern(TogglePattern.Pattern, out objPattern))
            {
                togPattern = objPattern as TogglePattern;
                if (togPattern.Current.ToggleState == ToggleState.Off)
                {
                    UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA"); // Added By Amit Tiwari
                    Mouse.Click(Control);// Added By Amit Tiwari
                }
            }  
           
            
        }
    }
}
