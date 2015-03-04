

using System;
using System.Collections.Generic;
using Core.Test.Foundation.Reports;

namespace Core.Test.Foundation.Logger
{
    public class TestLogger
    {
        public static bool TestCasesFailed { get; internal set; }       
        public static bool LoggerEnabled { get; set; }

        private static Dictionary<string, string> testCaseAttributes;
        private static LoggerSettings settings;
        private static HtmlLogger logger;       
        private static TestType type = TestType.API;
        private static List<ITestRunReport> reports;

        public TestLogger()
        {
        }

        public static void SetTestType(TestType testType)
        {
            type = testType;
        }

        public static TestType GetTestType()
        {
            return type;
        }

        public static string GetImageFileName()
        {
            if (TestCasesFailed)
            {
                string file = string.Format("{0}/{1}/{2}/{3}", settings.GetImageRelativePath(), settings.GetImageFolderName(), testCaseAttributes["Name"], settings.GetImageFileExtension());
                return file;
            }
            else
            {
                return "";
            }
        }

        public static string GetCurrentRunFolderName()
        {
            return settings.GetCurrentRunFolderName();
        }

        public static string getCurrentRunPath()
        {
            return string.Format("{0}\\{1}", settings.GetRootDirName(), settings.GetCurrentRunFolderName());
        }

        public static bool IsInitialized()
        {
            return testCaseAttributes.ContainsKey("Name");
        }

        public static ILogger GetLogger()
        {
            return GetHtmlLogger();
        }

        public static void Init(Dictionary<string, string> details)
        {
            testCaseAttributes = details;
            verify();
            settings = LoggerSettings.Create();
            logger = new HtmlLogger(settings);
            logger.testCasesAttributes = testCaseAttributes;
            logger.Init();
            TestCasesFailed = false;
            reports = new List<ITestRunReport>();
            reports.Add(new HTMLTestRunReport());
            reports.Add(new XMLTestRunReport());
        }

        public static void Clear()
        {
            if (logger != null)
            {
                try
                {
                    logger.Clear();
                }
                catch (Exception exe)
                {

                }
            }
            testCaseAttributes = null;
            if (!TestLogger.TestCasesFailed)
            {
                TestLogsDB.SetOutcome(TestOutcome.PASSED);
            }

            TestLogsDB.GenerateLogs();
        }

        private static ILogger GetHtmlLogger()
        {
            verify();
            return logger;
        }

        private static void verify()
        {
            if (!testCaseAttributes.ContainsKey("Name"))
                throw new Exception("Please set the test case details, Add test cases 'Name' to Details Dictionary");
            else
                return;
        }
    }
}
