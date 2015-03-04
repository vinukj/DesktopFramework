

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.Reports;

namespace Core.Test.Foundation
{
    public class TestLogsDB
    {
        public static bool RerunProgress;
        private static Dictionary<string, TestCase> test;
        private static TestCase activeTest;
        private static TestIteration activeIteration;
        private static DateTime startTime;
        private static DateTime endTime;
        private static String runId;
        private static Dictionary<String, ITestRunReport> reports;
        private static bool initialised;

        static TestLogsDB()
        {
            test = new Dictionary<String, TestCase>();
            startTime = DateTime.Now;
            runId = Guid.NewGuid().ToString();
            reports = new Dictionary<String, ITestRunReport>();
            ITestRunReport report = new HTMLTestRunReport();
            reports.Add(report.GetName(), report);
            report = new XMLTestRunReport();
            reports.Add(report.GetName(), report);
            report = new SplunkXMLTestRunReport();
            reports.Add(report.GetName(), report);

            initialised = true;
        }


        public static Dictionary<string, TestCase> GetTest()
        {
            return test;
        }
        public static TestCase GetActiveTest()
        {
            return activeTest;
        }

        public static DateTime GetStartTime()
        {
            return startTime;
        }

        public static String GetRunId()
        {
            return runId;
        }

        public static DateTime GetEndTime()
        {
            return endTime;
        }

        public static void RemoveTest(string name)
        {
            if (test.ContainsKey(name))
            {
                test.Remove(name);
            }
        }

        public static void SetOutcome(TestOutcome outcome)
        {
        }

        public static void AddTestRun(TestCaseInfo context)
        {
            if (!test.ContainsKey(context.TestName))
            {
                TestCase newTest = new TestCase(context);
                test.Add(context.TestName, newTest);
                activeTest = newTest;
                activeTest.StartTime = DateTime.Now;
            }

            activeIteration = activeTest.addIteration();
            activeIteration.StartTime = DateTime.Now;
        }

        public static TestCase GetTestCase(string name)
        {
            if (!test.ContainsKey(name))
            {
                return null;
            }
            else
            {
                return test[name];
            }
        }

        public static void AddStep(String step)
        {
            activeIteration.AddStep(step);
        }

        public static void SetFailureMessage(String message)
        {
            activeTest.EndTime = DateTime.Now;
            activeIteration.SetFailureMessage(message);
            activeIteration.EndTime = DateTime.Now;
        }

        public static void SetTestOutcome(TestOutcome outcome)
        {
            activeIteration.Outcome = outcome;
            activeTest.EndTime = DateTime.Now;
            activeIteration.EndTime = DateTime.Now;
        }

        public static String GetTestRunReportFile()
        {
            LoggerSettings settings = LoggerSettings.Create();
            StringBuilder builder = new StringBuilder();
            builder.Append(settings.GetRootDirName());
            builder.Append("\\");
            builder.Append(settings.GetCurrentRunFolderName());
            builder.Append("\\");
            builder.Append(Environment.UserName);
            builder.Append("_");
            builder.Append(settings.GetCurrentRunFolderName());
            builder.Append(".trr");
            return builder.ToString();
        }

        public static void GenerateLogs()
        {
            endTime = DateTime.Now;
            activeTest.EndTime = endTime;
            activeIteration.EndTime = endTime;
            foreach (KeyValuePair<string, ITestRunReport> entry in reports)
            {
                try
                {
                    entry.Value.Generate();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void RegisterReporter(ITestRunReport reporter)
        {
            if (!reports.ContainsKey(reporter.GetName()))
            {
                reports.Add(reporter.GetName(), reporter);
            }
        }

        public static void RemoveAllReports()
        {
            reports.Clear();
        }

    }
}
