
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Core.Test.Foundation
{
    public class TestIteration
    {
        private List<String> steps;
        private String message;
        private String testData;

        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }
        public TestOutcome Outcome { get; set; }

        public TestIteration()
        {
            Outcome = TestOutcome.PASSED;
            steps = new List<string>();
            message = "";
            testData = "";
        }

        public List<String> GetSteps()
        {
            return steps;
        }

        public String GetMessage()
        {
            return message;
        }

        public long GetExecutionTime()
        {
            TimeSpan span = EndTime - StartTime;
            long diff = (span.Hours * 60 * 60) + (span.Minutes * 60) + span.Seconds;
            return diff;
        }       

        public void SetTestData(String data)
        {
            testData = data;
        }

        public void SetFailureMessage(String message)
        {
            this.message = message;
            this.Outcome = TestOutcome.FAILED;
        }

        public void AddStep(String step)
        {
            if (steps.Count > 0 && steps[steps.Count - 1].Equals(step))
            {
                return;
            }
            steps.Add(step);
        }

        public void Save(XmlElement root, XmlDocument doc)
        {
            string format = "yyyy-MM-dd HH:mm:ss";
            XmlElement iterationElement = doc.CreateElement("Iteration");
            iterationElement.SetAttribute("Result", this.Outcome.ToString());
            iterationElement.SetAttribute("TestData", this.testData.ToString());
            iterationElement.SetAttribute("IterationStartTime", this.StartTime.ToString(format));
            iterationElement.SetAttribute("IterationEndTime", this.EndTime.ToString(format));
            foreach (String step in steps)
            {
                XmlElement stepElement = doc.CreateElement("Step");
                stepElement.InnerText = step;
                iterationElement.AppendChild(stepElement);
            }
            XmlElement message = doc.CreateElement("Message");
            message.InnerText = GetMessage();
            iterationElement.AppendChild(message);
            root.AppendChild(iterationElement);
        }

        public void Load(XmlNode node)
        {
            XmlElement controlEl = (XmlElement)node;
            this.Outcome = (TestOutcome)Enum.Parse(typeof(TestOutcome), controlEl.GetAttribute("Result")); 
            this.StartTime = DateTime.Parse(controlEl.GetAttribute("IterationStartTime"));
            this.EndTime = DateTime.Parse(controlEl.GetAttribute("IterationEndTime"));
            XmlNodeList nodes = controlEl.GetElementsByTagName("Step");
            for (int i = 0; i < nodes.Count; i++)
            {
                steps.Add(nodes[i].InnerText);
            }
            nodes = controlEl.GetElementsByTagName("Message");
            this.message = nodes[0].InnerText;
        }
    }
}
