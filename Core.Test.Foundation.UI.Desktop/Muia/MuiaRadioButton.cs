using Microsoft.VisualStudio.TestTools.UITesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI.Desktop.Muia
{
    public class MuiaRadioButton : MuiaControl, IRadioButton
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="MuiaRadioButton" /> class.
        /// </summary>
        /// <param name="nativeElement">Native Automation element</param>
        public MuiaRadioButton(AutomationElement nativeElement, string ControlName)
        {
            this.SearchOptions = new Dictionary<MuiaElementProperty, string>();
            this.NativeElement = nativeElement;
            this.ControlName = ControlName;
        }

        public bool IsOn(ControlRetry retry, bool throwOnFail)
        {
            bool State = false;
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetCheckCheckBoxMessage(ControlName));
            }
            this.IsVisible(retry, throwOnFail);
            Object objPattern;
            SelectionItemPattern selectPattern;

            this.NativeElement.TryGetCurrentPattern(SelectionItemPattern.Pattern, out objPattern);
            if (objPattern != null && (objPattern as SelectionItemPattern) != null)
            {
                selectPattern = objPattern as SelectionItemPattern;
                 State = selectPattern.Current.IsSelected;
            }
            return State;
        }

        public void Off(ControlRetry retry, bool throwOnFail)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetCheckBoxUnCheckMessage(ControlName));
            }
            this.IsVisible(retry, throwOnFail);
            Object objPattern;
            SelectionItemPattern selectPattern;

            this.NativeElement.TryGetCurrentPattern(InvokePattern.Pattern, out objPattern);
            //objPattern= (InvokePattern)this.NativeElement.GetCurrentPattern(InvokePattern.Pattern);
            if (objPattern != null && (objPattern as InvokePattern) != null)
            {
                selectPattern = objPattern as SelectionItemPattern;

                selectPattern.Select();
            }
        }


        public void On(ControlRetry retry, bool throwOnFail)
        {
            if (TestLogger.LoggerEnabled)
            {
                this.WriteMessage(LogGen.GetCheckCheckBoxMessage(ControlName));
            }
            this.IsVisible(retry, throwOnFail);
           // Object objPattern;
            //SelectionItemPattern selectPattern;
            Point point=new Point();
            this.NativeElement.TryGetClickablePoint(out point);
           // System.Drawing.Point DrawingPoint=new System.Drawing.Point((int)point.X,(int)point.Y);
           //// Mouse.Hover(
           // Mouse.Click(DrawingPoint);
            UITestControl Control = UITestControlFactory.FromNativeElement(this.NativeElement, "UIA");
            Mouse.Click(Control);
            Control.DrawHighlight();
            //objPattern= (InvokePattern)this.NativeElement.GetCurrentPattern(InvokePattern.Pattern);
            //if (objPattern != null && (objPattern as SelectionItemPattern) != null)
            //{
            //    selectPattern = objPattern as SelectionItemPattern;
            //    bool value=  selectPattern.Current.IsSelected;
            //    selectPattern.Select();
            //}
        }
    }
}
