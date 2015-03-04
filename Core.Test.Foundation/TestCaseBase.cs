

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Test.Foundation.Logger;

namespace Core.Test.Foundation
{
    [TestClass]
    public class TestCaseBase
    {
        public TestContext TestContext { get; set; }

        protected TestCaseInfo testContext;

        private static int count = -1;

        protected TestCaseInfo previousTest;

        /// <summary>
        /// Initialized the Session for the run of test Script
        /// </summary>
        /// <param name="testType">Test type (ex-UI,APT,DB)</param>
        public void InitSession(TestType testType)
        {
            TestLogger.LoggerEnabled = true;
            this.testContext = new TestCaseInfo(this.GetType().GetMethod(TestContext.TestName), this);

            TestCase previousTest = TestLogsDB.GetActiveTest();
            if (previousTest != null && previousTest.Context.TestName.Equals(this.testContext.TestName))
            {
                count++;
            }
            else
            {
                count = 1;
            }

            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("Name", testContext.TestName + "_" + count);

            map.Add("Owner", testContext.Owner);
            int x = testContext.Priority;
            map.Add("Priority", testContext.Priority.ToString());
            map.Add("Category", testContext.Category);
            map.Add("TestId", testContext.TestId);

            TestLogger.Init(map);
            TestLogger.SetTestType(testType);
            TestLogsDB.AddTestRun(testContext);
            TestSession.Register("Context", this.testContext);
        }    
    }
}
