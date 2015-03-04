
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.UI.Contracts;

namespace Core.Test.Foundation.UI
{
    public class LogUtil
    {

        static LogUtil()
        {
            previousMsg = "";
        }
        private static String previousMsg;

        public static void WriteMessage(String message)
        {
            if (previousMsg.Equals(message))
            {
                return;
            }
            previousMsg = message;
            TestLogsDB.AddStep(message);
            Console.WriteLine(message);

            try
            {
                ILogger logger = TestLogger.GetLogger();
                logger.Write(message);
            }
            catch (Exception exe)
            {
            }
        }

        private static void WriteMessage(Exception message)
        {
            TestLogsDB.SetFailureMessage("Exception:" + message.Message);
            Console.WriteLine("Exception:" + message.Message);

            try
            {
                ILogger logger = TestLogger.GetLogger();
                logger.Write(message);
            }
            catch (Exception exe)
            {
            }
        }

        public static void WriteMessage(Object message)
        {
            WriteMessage((Exception)message);
        }

        public static void WriteMessage(string message, IControl control)
        {
            if (control.DisableLogging)
            {
                return;
            }
            WriteMessage(message);
        }
    }
}
