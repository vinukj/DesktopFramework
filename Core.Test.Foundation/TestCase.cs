

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Core.Test.Foundation
{
    public class TestCase
    {
        private List<TestIteration> iterations;
        public TestCaseInfo Context { get; set; }

        public DateTime EndTime { get; set; }
        public DateTime StartTime { get; set; }

        public List<TestIteration> GetIterations()
        {
            return iterations;
        }
        public TestCase(TestCaseInfo context)
        {
            this.iterations = new List<TestIteration>();
            this.Context = context;
        }

        /// <summary>
        /// Return the Test Out come (Ex-Passed,Failed or Aborted
        /// </summary>
        public TestOutcome GetTestOutcome()
        {
            foreach (TestIteration iteration in iterations)
            {
                if (iteration.Outcome == TestOutcome.ABORTED)
                {
                    return TestOutcome.ABORTED;
                }

                if (iteration.Outcome == TestOutcome.FAILED)
                {
                    return TestOutcome.FAILED;
                }
            }
           
            return TestOutcome.PASSED;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="root">Xml Element</param>
        /// <param name="doc">XmlDocumnet Class Object</param>
        /// <param name="fieldName">Filed name</param>
        public void Save(XmlElement root, XmlDocument doc)
        {
            XmlElement testElement = doc.CreateElement("TestCase");
            testElement.SetAttribute("Name", Context.TestName);
            string format = "yyyy-MM-dd HH:mm:ss";
            testElement.SetAttribute("StartTime", this.StartTime.ToString(format));
            testElement.SetAttribute("EndTime", this.EndTime.ToString(format));
            testElement.SetAttribute("Priority", Context.Priority.ToString());
            testElement.SetAttribute("Owner", Context.Owner);
            testElement.SetAttribute("TestId", Context.TestId);
            testElement.SetAttribute("Category", Context.Category);
            testElement.SetAttribute("Module", Context.ModuleName);
            testElement.SetAttribute("Result", GetTestOutcome().ToString());

            foreach (TestIteration iteration in iterations)
            {
                iteration.Save(testElement, doc);
            }

            root.AppendChild(testElement);
        }

        /// <summary>
        /// Return Iteration For the Test case
        /// </summary>
        /// <param name="root">Xml Element</param>
        public TestIteration addIteration()
        {
            TestIteration iteration = new TestIteration();
            iterations.Add(iteration);
            return iteration;
        }

        /// <summary>
        /// Load data for the Specific xml node or tag
        /// </summary>
        /// <param name="node">Xml node </param>
        public void Load(XmlNode node)
        {
            XmlElement controlEl = (XmlElement)node;
            this.Context = new TestCaseInfo();
            this.Context.Category = controlEl.GetAttribute("Category");
            this.Context.Owner = controlEl.GetAttribute("Owner");
            this.Context.TestId = controlEl.GetAttribute("TestId");
            this.Context.Priority = int.Parse(controlEl.GetAttribute("Priority"));
            this.Context.ModuleName = controlEl.GetAttribute("Module");
            this.Context.TestName = controlEl.GetAttribute("Name");
            this.StartTime = DateTime.Parse(controlEl.GetAttribute("StartTime"));
            this.EndTime = DateTime.Parse(controlEl.GetAttribute("EndTime"));
            XmlNodeList nodes = controlEl.GetElementsByTagName("Iteration");
            int length = nodes.Count;
            for (int i = 0; i < length; i++)
            {
                TestIteration ite = new TestIteration();
                ite.Load(nodes[i]);
                iterations.Add(ite);
            }
        }
    }
}
