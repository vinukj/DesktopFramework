

using System;
using Core.Test.Foundation;
using Core.Test.Foundation.Models;
using Core.Test.Foundation.UI.Contracts.Selenium;
using Core.Test.Foundation.Logger;

namespace Core.Test.Foundation.UI.Desktop
{
    public class Application
    {

        /// <summary>
        /// Start A Web Appliaction
        /// </summary>
        //public static void Start()
        //{
        //    AUTState.IsGood = false;

        //    if (!WebAppState.IsGood)
        //    {
        //        StartApp();
        //        WebAppState.IsGood = true;
        //    }
        //}

        public static void Shutdown(bool logout = false, bool verifyError = true)
        {
            try
            {
                if (verifyError)
                {

                }
                if (logout)
                {

                }
                TestLogger.Clear();
            }
            catch (Exception exe)
            {

            }
        }
    }
}
