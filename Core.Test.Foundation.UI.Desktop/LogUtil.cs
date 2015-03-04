

using System;
using System.Collections.Generic;
using Core.Test.Foundation;
using Core.Test.Foundation.Logger;

namespace Core.Test.Foundation.UI.Desktop
{
    public class LogUtil
    {
        public static void Write(string step)
        {
            TestLogsDB.AddStep(step);
            ILogger loger = TestLogger.GetLogger();
            loger.Write(step);
        }
        /// <summary>
        /// Handle the failure (exception)
        /// </summary>
        /// <param name="message">Exception message that is cathced</param>
        public static void HandleFailure(Exception message)
        {
            TestLogsDB.SetFailureMessage(message.Message);
            ILogger loger = TestLogger.GetLogger();
            loger.Write(message);
           
            Application.Shutdown(false, false);
        }


        private static List<string> GetKnowErrors()
        {
            List<string> knownErros = new List<string>();
            knownErros.Add("Some errors");
            return knownErros;
        }
    }
}
