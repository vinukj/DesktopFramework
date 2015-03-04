
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Core.Test.Foundation.Reports
{
    public class TestRunReport
    {
        public List<TestCase> TestCases { get; set; }

        public DateTime GetEndTime()
        {
            return endTime;
        }

        public DateTime GetStartTime()
        {
            return startTime;
        }

        private DateTime endTime;
        private DateTime startTime;

        /// <summary>
        /// Load the XML
        /// </summary>
        /// <param name="filename">Xml File name</param>
        public void Load(string fileName)
        {
           try{
                XmlDocument document = new XmlDocument();
                document.Load(fileName);
                XmlElement doc = document.DocumentElement;
                XmlNodeList nodes = doc.GetElementsByTagName("TestCase");
                TestCases = new List<TestCase>();
                for(int index=0; index<nodes.Count;index++){
                    TestCase testcase = new TestCase(null);
                    testcase.Load(nodes[index]);
                    TestCases.Add(testcase);
                }

                XmlElement rootNode = doc;
                String format = "yyyy-MM-dd HH:mm:ss";
                this.startTime = DateTime.Parse(rootNode.GetAttribute("StartTime")) ;
                this.endTime = DateTime.Parse(rootNode.GetAttribute("EndTime"));

            }
            catch(Exception exe){
            } 
        }
     
        public void Load()
        {
            startTime = TestLogsDB.GetStartTime();
            endTime = TestLogsDB.GetEndTime();
            TestCases = TestLogsDB.GetTest().Select(item => item.Value).ToList();
        }
    }
}
