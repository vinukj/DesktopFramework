using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Automation;
    using Core.Test.Foundation.UI.Contracts;

    /// <summary>
    /// Represents MUAI button (Wrapper on top of AutomationElement)
    /// </summary>
    public class MuiaButton : MuiaControl, IButton
    {
        /// <summary>
        /// Gets the text
        /// </summary>
        public string Text
        {
            get
            {
                return this.GetAttribute("Name");
            }
        }

        // Added by Amit Tiwari
        public MuiaButton(AutomationElement nativeElement,string ControlName)
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();

            this.NativeElement = nativeElement;
            this.ControlName = ControlName;
        }

    }
}
