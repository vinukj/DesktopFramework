

using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Desktop.Test.UI.Framework.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation;
using Core.Test.Foundation.Configs;
using Core.Test.Foundation.UI;
using Core.Test.Foundation.UI.Contracts;

namespace Application.Desktop.Test.Suite
{
    
    [DeploymentItem("Configs", "Configs")]
    [DeploymentItem("TestData", "TestData")]
    public abstract class DesktopUITestBase : TestCaseBase
    {
        // Property to get the Page Name
        public string PageName { get; set; }
        private IControl pageUniqueControl;
        public abstract IControl GetPageIdentifier();
        /// <summary>
        /// Bases the cleanup.
        /// </summary>
        [TestCleanup]
        public void BaseCleanup()
        {
            try
            {
              //  this.TestCleanup();

                Playback.StopSession();
            }
            catch
            {
            }
        }

        // Adding the DesktopLoaded Control
        public bool IsDesktopLoaded(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            String message = LogGen.GetPageLoadedMessage(this.PageName);
            LogUtil.WriteMessage(message);
            this.pageUniqueControl = this.GetPageIdentifier();
            this.pageUniqueControl.DisableLogging = true;
            bool result = this.pageUniqueControl.IsVisible(retry, throwOnFail);
            this.pageUniqueControl.DisableLogging = true;
            return result;
        }

        public bool IsDesktopLoaded(IControl control, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            control.DisableLogging = true;
            String message = LogGen.GetPageLoadedMessage(this.PageName);
            LogUtil.WriteMessage(message);
            bool result = control.IsVisible(retry, throwOnFail);
            control.DisableLogging = false;
            return result;
        }
        /// <summary>
        /// Bases the initialize.
        /// </summary>
        [TestInitialize]
        public void BaseInitialize()
        {
            TestConfig.LoadAll();
            base.InitSession(TestType.UI); 
           // this.TestInitialize();
        }
    }
}
