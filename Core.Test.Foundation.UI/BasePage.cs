

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI
{
    public abstract class BasePage:IPage
    {
        public string PageName { get; set; }
        private IControl pageUniqueControl;
        public abstract IControl GetPageIdentifier();

        /// <summary>
        /// Ensure that the control or Apllication is loaded or not
        /// </summary>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>
        public bool IsLoaded(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            String message = LogGen.GetPageLoadedMessage(this.PageName);
            LogUtil.WriteMessage(message);
            this.pageUniqueControl = this.GetPageIdentifier();
            this.pageUniqueControl.DisableLogging = true;
            bool result = this.pageUniqueControl.IsVisible(retry, throwOnFail);
            this.pageUniqueControl.DisableLogging = true;
            return result;
        }

        /// <summary>
        /// Ensure that the control or Apllication is loaded or not
        /// </summary>
        /// <param name="control">Control to load</param>
        /// <param name="retry">Control retry</param>
        /// <param name="throwOnFail">Throw if the control is not visible</param>

        public bool IsLoaded(IControl control, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            control.DisableLogging = true;
            String message = LogGen.GetPageLoadedMessage(this.PageName);
            LogUtil.WriteMessage(message);
            bool result = control.IsVisible(retry, throwOnFail);
            control.DisableLogging = false;
            return result;
        }
    }
}
