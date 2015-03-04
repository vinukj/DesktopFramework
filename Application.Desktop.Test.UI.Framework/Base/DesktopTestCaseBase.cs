
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Application.Desktop.Test.UI.Framework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Test.Foundation;
using Core.Test.Foundation.Logger;

namespace Application.Desktop.Test.UI.Framework.Base
{
    public class DesktopTestCaseBase : TestCaseBase
    {

        /// <summary>
        /// The is performance test
        /// </summary>
        private bool isPerformanceTest = false;

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        /// <value>The context.</value>
      //  public static TestContext Context { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is performance test.
        /// </summary>
        /// <value><c>true</c> if this instance is performance test; otherwise, <c>false</c>.</value>
        public bool IsPerformanceTest
        {
            get
            {
                return this.isPerformanceTest;
            }

            set
            {
                this.isPerformanceTest = value;
            }
        }

        /// <summary>
        /// Gets or sets the test context which provides
        /// information about and functionality for the current test run.
        /// </summary>
        /// <value>The test context.</value>
        //public TestContext TestContext { get; set; }
      //  protected TestCaseInfo testContext; // To Store the Test case Information
        /// <summary>
        /// Tests the initialize.
        /// </summary>
        [TestInitialize]
        public void TestInitialize()
        {
            TestLogger.Clear();
            // Calling up the  InitSession() From TestCaseBase.cs Which is Doing the same Functionality As Commented Below
            TestCaseBase testcasebase = new TestCaseBase();
            this.InitSession(TestType.UI);
           // //int count=1; // added for the Name of Testcase
           //
            
           // Context = this.TestContext;

           // TestSession.Start();

           // TestSession.UnRegister(SessionConstants.TestContext);

           //// Dictionary<string, string> info = this.GetTestCaseInfo();   
           // this.testContext = new TestCaseInfo(this.GetType().GetMethod(TestContext.TestName), this); //

           // // Craeting the Dictionary for the Testcase Attribute //

           // Dictionary<string, string> map = new Dictionary<string, string>();
           // map.Add("Name", testContext.TestName + "_" +1);

           // map.Add("Owner", testContext.Owner);
           // int x = testContext.Priority;
           // map.Add("Priority", testContext.Priority.ToString());
           // map.Add("Category", testContext.Category);
           // map.Add("TestId", testContext.TestId);

           // if (!Playback.IsInitialized)
           // {
           //     Playback.Initialize();
           // }

           // if (CoreAppConfig.EnableLogger)
           // {
           //    // TestLogger.TestCasesFailed= info; // Commented Because Its ALready getting Assigned in  the init() function Which got Called Below

           //     TestLogger.Init(map);
           // }
        }

        /// <summary>
        /// Tests the cleanup.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (Playback.IsInitialized)
            {
                Playback.Cleanup();
            }
            
            if (this.isPerformanceTest)
            {
                try
                {
                    this.GeneratePerformanceReport();
                    //Commented for time Being
                   // LogerUtil.Write("Performance log report is generated successfully");
                }
                catch (Exception exe)
                {
                   // LogerUtil.Write(string.Format("Failed to generate test log: '{0}'", exe.Message));
                }
            }

            TestLogger.Clear();
        }

        /// <summary>
        /// Gets the current logs folder.
        /// </summary>
        /// <returns>Returns the current logs folder</returns>
        protected string GetCurrentLogsFolder()
        {
            string path = string.Empty;

          //  path = TestLogger.CurrentRunPath;
            path = TestLogger.getCurrentRunPath();

            return path;
        }

        /// <summary>
        /// Gets the test case info.
        /// </summary>
        /// <returns>Returns test cases attribute dictionary</returns>
        //private Dictionary<string, string> GetTestCaseInfo()
        //{
        //    Dictionary<string, string> details = new Dictionary<string, string>();

        //    bool isvarient = this.TestContext.DataRow != null;

        //    if (!isvarient || this.TestContext.DataRow.Table.Rows.Count <= 1)
        //    {
        //        details.Add("Name", this.TestContext.TestName);
        //    }
        //    else
        //    {
        //        details.Add("Name", this.TestContext.TestName + "_" + this.GetCurrentIteration());
        //    }

        //    details.Add("Type", "UI");

        //    MethodInfo info = this.GetType().GetMethod(TestContext.TestName);

        //    object[] attr = info.GetCustomAttributes(typeof(OwnerAttribute), true);

        //    if (attr.Count() > 0)
        //    {
        //        details.Add("Owner", (attr[0] as OwnerAttribute).Owner);
        //    }

        //    attr = info.GetCustomAttributes(typeof(PriorityAttribute), true);

        //    if (attr.Count() > 0)
        //    {
        //        details.Add("Priority", (attr[0] as PriorityAttribute).Priority.ToString());
        //    }

        //    attr = info.GetCustomAttributes(typeof(ProcessAttribute), true);

        //    if (attr.Count() > 0)
        //    {
        //        details.Add("Process", (attr[0] as ProcessAttribute).Value.ToString());
        //    }

        //    return details;
        //}

        /// <summary>
        /// Gets the current iteration.
        /// </summary>
        /// <returns>Returns current iteration</returns>
        private int GetCurrentIteration()
        {
            int curIteration = this.TestContext.DataRow == null ? 0 : this.TestContext.DataRow.Table.Rows.IndexOf(this.TestContext.DataRow);

            return curIteration;
        }

        /// <summary>
        /// Generates the performance report.
        /// </summary>
        private void GeneratePerformanceReport()
        {
            int totalIteration = this.TestContext.DataRow == null ? 1 : this.TestContext.DataRow.Table.Rows.Count;

            if (this.GetCurrentIteration() == totalIteration - 1)
            {
                string path = this.GetCurrentLogsFolder();
            }
        }

        /// <summary>
        /// Creates the performance report folder.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="message">The message.</param>
        /// <returns><c>true</c> if creations is successful, <c>false</c> otherwise</returns>
        private bool CreatePerfReportFolder(string path, ref string message)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                message = string.Empty;

                return true;
            }
            catch (Exception exe)
            {
                message = exe.Message;

                return false;
            }
        }


    }
}
