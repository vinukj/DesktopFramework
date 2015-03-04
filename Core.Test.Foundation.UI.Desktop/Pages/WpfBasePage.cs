

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI.Desktop.Pages
{
    public abstract class WpfBasePage
    {

        /// <summary>
        /// Gets or sets the page name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the page identifier.
        /// </summary>
        public IControl PageIdentifier { get; protected set; }

        /// <summary>
        /// Initialize the page controls
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Close the page
        /// </summary>
        public void Close()
        {
        }

        /// <summary>
        /// Dispose the page.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Check whether the page is loaded or not.
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <returns>Returns true if the page is loaded.</returns>
        //public virtual bool IsLoaded(ControlRetry retry = ControlRetry.Avg)
        //{
        //    this.PageIdentifier.EnableLogging = false;

        //    LogerUtil.Write(string.Format("Checking whether '{0}' is loaded or not", this.Name));

        //    bool result = this.PageIdentifier.IsVisible(retry);

        //    this.PageIdentifier.EnableLogging = true;

        //    return result;
        //}

        ///// <summary>
        /////  Check whether the page is loaded or not.
        ///// </summary>
        ///// <param name="throwOnFail">Throw exception if the control is not visible</param>
        ///// <param name="retry">Control retry.</param>
        ///// <returns>Returns true if the page is loaded.</returns>
        //public virtual bool IsLoaded(bool throwOnFail, ControlRetry retry = ControlRetry.Avg)
        //{
        //    this.PageIdentifier.EnableLogging = false;

        //    LogerUtil.Write(string.Format("Checking whether '{0}' is loaded or not", this.Name));

        //    bool result = this.PageIdentifier.IsVisible(retry);

        //    this.PageIdentifier.EnableLogging = true;

        //    if (throwOnFail && !result)
        //    {
        //        string message = string.Format("'{0}' is not loaded", this.Name);

        //        throw new Exception(message);
        //    }

        //    return result;
        //}

        /// <summary>
        ///  Check whether the page is loaded or not.
        /// </summary>
        /// <param name="retry">Control retry.</param>
        /// <param name="throwOnFail">Throw exception if the control is not visible</param>
        /// <returns>Returns true if the page is loaded.</returns>
        //public virtual bool IsExist(ControlRetry retry = ControlRetry.Worst, bool throwOnFail = false)
        //{
        //    return this.IsLoaded(throwOnFail, retry);
        //}

        /// <summary>
        /// Compare the field
        /// </summary>
        /// <param name="actual">Actual data</param>
        /// <param name="expected">Expected data</param>
        /// <param name="fieldName">Filed name</param>
        public void CompareField(string actual, string expected, string fieldName)
        {
            actual = actual.Trim().Replace(System.Environment.NewLine, string.Empty);
            actual = actual.Trim().Replace("\n\r", string.Empty);
            expected = expected.Trim().Replace(System.Environment.NewLine, string.Empty);
            expected = expected.Trim().Replace("\n\r", string.Empty);

            bool result = actual.ToLower() == expected.ToLower();

            string message = string.Format("{0} didn't  match. Expected = '{1}', Actual = '{2}'", fieldName, expected, actual);

            Assert.IsTrue(result, message);
        }

    }
}
