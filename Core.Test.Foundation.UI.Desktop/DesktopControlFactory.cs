

using Core.Test.Foundation.UI.Desktop.Muia;
using Core.Test.Foundation.UI.Desktop.WinForms;
using Core.Test.Foundation.UI.Desktop.Wpf;

namespace Core.Test.Foundation.UI.Desktop
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Core.Test.Foundation.UI.Desktop.Muia;
    using Core.Test.Foundation.UI.Desktop.Wpf;
    using Core.Test.Foundation.UI.Desktop.WinForms;
    /// <summary>
    /// Represents Factory to create instance of Custom control and wrapping around with Coded UI Controls
    /// </summary>
    public class DesktopControlFactory
    {
        /// <summary>
        /// Creates the win.
        /// </summary>
        /// <typeparam name="T">Win Control Type</typeparam>
        /// <param name="nativeElement">The native element.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Returns the instance of specified custom control</returns>
        public static T CreateWin<T>(UITestControl nativeElement, string controlName = "") where T : WinControl
        {
            T control = Activator.CreateInstance<T>();

            control.NativeElement = nativeElement;

            control.ControlName = controlName;

            control.EnableLogging = true;

            return control;
        }

        /// <summary>
        /// Creates the WPF.
        /// </summary>
        /// <typeparam name="T">WPF Control Type</typeparam>
        /// <param name="nativeElement">The native element.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Returns the instance of specified custom control.</returns>
        public static T CreateWpf<T>(UITestControl nativeElement, string controlName = "") where T : WPFControl
        {
            T control = Activator.CreateInstance<T>();

            control.NativeElement = nativeElement;

            control.ControlName = controlName;

            control.EnableLogging = true;

            return control;
        }

        /// <summary>
        /// Creates the MUIA.
        /// </summary>
        /// <typeparam name="T">WPF Control Type</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="property">The property.</param>
        /// <param name="value">The value.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Returns the instance of specified custom control</returns>
        public static T CreateMuia<T>(object parent, MuiaElementProperty property, string value, string controlName = "") where T : MuiaControl
        {
            T control = Activator.CreateInstance<T>();

            control.Parent = parent;

            control.ControlName = controlName;

            control.SearchOptions.Add(property, value);

            control.EnableLogging = true;

            return control;
        }

        /// <summary>
        /// Creates the MUIA.
        /// </summary>
        /// <typeparam name="T">WPF Control Type</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="properties">The properties.</param>
        /// <param name="controlName">Name of the control.</param>
        /// <returns>Returns the instance of specified custom control</returns>
        public static T CreateMuia<T>(object parent, Dictionary<MuiaElementProperty, string> properties, string controlName = "") where T : MuiaControl
        {
            T control = Activator.CreateInstance<T>();

            control.Parent = parent;

            control.ControlName = controlName;

            control.SearchOptions = properties;

            control.EnableLogging = true;

            return control;
        }
    }
}
