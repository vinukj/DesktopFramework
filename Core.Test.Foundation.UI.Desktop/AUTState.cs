

namespace Core.Test.Foundation.UI.Desktop
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents Application under test state
    /// </summary>
    public class AUTState
    {
        /// <summary>
        /// Initializes static members of the <see cref="AUTState"/> class.
        /// </summary>
        static AUTState()
        {
            IsGood = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the AUT state is good.
        /// </summary>
        /// <value><c>true</c> if this AUT is good; otherwise, <c>false</c>.</value>
        public static bool IsGood { get; set; }

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        public static Process Application { get; internal set; }

        /// <summary>
        /// Gets the application handle.
        /// </summary>
        /// <value>The application handle.</value>
        public static IntPtr ApplicationHandle { get; internal set; }
    }
}
