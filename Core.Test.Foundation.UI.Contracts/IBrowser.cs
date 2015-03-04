//----------------------------------------------------------------------- // 


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public interface IBrowser
    {
        /// <summary>
        ///switch to Browser Window
        /// </summary>
        /// <param name="windowTitle">titile of the Window</param>
        void SwitchToWindow(string windowTitle);

        /// <summary>
        ///To Start or launch the Browser
        /// </summary>
        /// <param name="startupInfo">Browser StartupInfo(like Browsertype,Url,DriverExPath</param>
        void Start(object startupInfo);

        /// <summary>
        ///To Navigate to Given Url
        /// </summary>
        /// <param name="url"> the Url that you want to Navigate</param>
        void Navigate(string url);
        void SelectFrame(string attributeName, string attributeValue);

        /// <summary>
        ///Return the Web Driver object
        /// </summary>
        object GetDriver();

        /// <summary>
        ///To Clean up the Cookies
        /// </summary>
        void CleanupCookies();

        /// <summary>
        ///Return true if the process is killed (Browser process ex-IE,Chrome)
        /// </summary>
        bool Stop();

        /// <summary>
        ///To Refresh the WebDriver and agin navigate to it
        /// </summary>
        void Refresh();

        /// <summary>
        ///Return the Curent Url
        /// </summary>
        string GetCurrentUrl();

        /// <summary>
        ///Use to select the tab in Browser
        /// </summary>
        /// <param name="tabIndex"> pass the index of tab that you wannt to select<param>
        void SelectTab(int tabIndex);

        /// <summary>
        ///Use to Close the tab in Browser
        /// </summary>
        /// <param name="tabIndex"> pass the index of tab that you wannt to Close<param>
        void CloseTab(int tabIndex);

        /// <summary>
        ///Wait for the Web page to Load
        /// </summary>
        /// <param name="retry">Control retry<param>
        void WaitForPageToLoad(ControlRetry retry);
    }
}
