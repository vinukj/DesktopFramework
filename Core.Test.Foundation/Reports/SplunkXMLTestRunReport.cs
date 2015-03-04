
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.Models;

namespace Core.Test.Foundation.Reports
{
    public class SplunkXMLTestRunReport : ITestRunReport
    {
        string timeFormat = "yyyy-MM-dd HH:mm:ss";

        public bool Generate()
        {
            bool enabled = bool.Parse(TestSession.Config.GetAppSettings(ConfigKeys.PushLogsToSplunk));
            if (!enabled)
            {
                return false;
            }

            TestCase testCases = TestLogsDB.GetActiveTest();
            bool reRun = bool.Parse(TestSession.Config.GetAppSettings(ConfigKeys.ReRunFailed));
            if (reRun)
            {
                if (testCases.GetTestOutcome() != TestOutcome.PASSED)
                {
                    if (!TestLogsDB.RerunProgress)
                    {
                        return true;
                    }
                }
            }
            DateTime endTime = DateTime.Now;

            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement testRun = document.CreateElement("TestRun");
                document.AppendChild(testRun);
                string runId = TestLogsDB.GetRunId();
                testRun.SetAttribute("id", runId);
                string time = TestLogsDB.GetStartTime().ToString(timeFormat);
                testRun.SetAttribute("StartTime", TestLogsDB.GetStartTime().ToString(timeFormat));
                testRun.SetAttribute("EndTime", endTime.ToString(timeFormat));
                Save(testRun, document, testCases);
                String hostName = Dns.GetHostName();

                String name = hostName + "_SplunkTestLog.splog";
                //String name = Guid.NewGuid().ToString() + "_" + "SplunkTestLog.splog";
                String fileName = TestSession.Config.GetAppSettings(ConfigKeys.SplunkLogLocation) + "\\" + name;
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                document.Save(fileName);
            }
            catch (Exception exe)
            {
                return false;
            }
            return true;
        }


        public String GetName()
        {
            return "Splunk Test Run Report";
        }

        public void Save(XmlElement root, XmlDocument doc, TestCase testCase)
        {
            XmlElement testElement = doc.CreateElement("TestCase");
            TestCaseInfo context = testCase.Context;
            testElement.SetAttribute("Name", context.TestName);
            testElement.SetAttribute("StartTime", testCase.StartTime.ToString(timeFormat)); ;
            testElement.SetAttribute("EndTime", testCase.EndTime.ToString(timeFormat));
            int count = testCase.GetIterations().Count;
            TestIteration iteration = testCase.GetIterations()[count - 1];
            Save(testElement, doc, iteration, testCase, count);
            root.AppendChild(testElement);
        }

        public void Save(XmlElement root, XmlDocument doc, TestIteration iteration, TestCase testCase, int count)
        {
            XmlElement testElement = doc.CreateElement("Iteration");
            TestCaseInfo context = testCase.Context;
            testElement.SetAttribute("Priority", context.Priority.ToString());
            testElement.SetAttribute("Owner", context.Owner);
            testElement.SetAttribute("TestId", context.TestId);
            testElement.SetAttribute("Category", context.Category);
            testElement.SetAttribute("Module", context.ModuleName);
            testElement.SetAttribute("Result", testCase.GetTestOutcome().ToString());
            TestEnvironment en = TestSession.Config.GetWebActiveEnvironment();
            testElement.SetAttribute("Project", TestSession.Config.GetAppSettings(ConfigKeys.Project));
            testElement.SetAttribute("Environment", en.Name);
            if (count > 1)
            {
                testElement.SetAttribute("IterationName", context.TestName + "_" + count);
            }
            else
            {
                testElement.SetAttribute("IterationName", context.TestName);
            }
            String buildNo = "";
            BuildInfoModel info = en.BuildInfoModel;
            if (info != null)
            {
                buildNo = info.Version;
            }
            testElement.SetAttribute("BuildNo", buildNo);
            string timeFormat = "yyyy-MM-dd";
            string runDate = TestLogsDB.GetStartTime().ToString(timeFormat);
            testElement.SetAttribute("TestRunDate", runDate);
            timeFormat = "HH:mm:ss";
            string runTime = TestLogsDB.GetStartTime().ToString(timeFormat);
            testElement.SetAttribute("TestRunTime", runTime);
            String text = iteration.GetMessage();
            testElement.SetAttribute("Message", text == null ? "" : text);
            root.AppendChild(testElement);
        }
    }
}
