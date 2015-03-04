
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Core.Test.Foundation.Reports
{
    public class XMLTestRunReport : ITestRunReport
    {
        public string GetName()
        {
            return "XML Report";
        }

        public bool Generate()
        {
            DateTime endTime = DateTime.Now;
            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement testRun = document.CreateElement("TestRun");
                document.AppendChild(testRun);
                testRun.SetAttribute("id", TestLogsDB.GetRunId());
                string format = "yyyy-MM-dd HH:mm:ss";
                testRun.SetAttribute("StartTime", TestLogsDB.GetStartTime().ToString(format));
                testRun.SetAttribute("EndTime", endTime.ToString(format));
                foreach (KeyValuePair<String, TestCase> entry in TestLogsDB.GetTest())
                {
                    entry.Value.Save(testRun, document);
                }

                string fileName = TestLogsDB.GetTestRunReportFile().Replace(".trr", ".xml");
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
    }
}
