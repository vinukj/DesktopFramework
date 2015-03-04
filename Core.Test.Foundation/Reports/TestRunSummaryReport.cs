
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Foundation.Reports
{
    public class TestRunSummaryReport
    {
        public string ModuleName { get; set; }
        public int Passed { get; set; }
        public int Failed { get; set; }
        public int Others { get; set; }
        public int Total
        {
            get
            {
                return Passed + Failed + Others;
            }
        }

        public static Dictionary<string, TestRunSummaryReport> GetRowSummary(TestRunReport run)
        {
            Dictionary<string, TestRunSummaryReport> map = new Dictionary<string, TestRunSummaryReport>();
            foreach (TestCase tcase in run.TestCases)
            {
                TestRunSummaryReport reportModel = null;
                string key = tcase.Context.ModuleName;
                if (map.ContainsKey(key))
                {
                    reportModel = map[key];
                }
                else
                {
                    reportModel = new TestRunSummaryReport();
                    map.Add(key, reportModel);
                }

                reportModel.ModuleName = key;
                foreach (TestIteration ite in tcase.GetIterations())
                {
                    if (ite.Outcome == TestOutcome.PASSED)
                    {
                        reportModel.Passed = reportModel.Passed + 1;
                    }
                    else if (ite.Outcome == TestOutcome.FAILED)
                    {
                        reportModel.Failed = reportModel.Failed + 1;
                    }
                    else
                    {
                        reportModel.Others = reportModel.Others + 1;
                    }
                }
            }
            return map;
        }

        public static Dictionary<string, TestRunSummaryReport> GetRowSummary(TestRunReport run, ResultBy by)
        {
            Dictionary<string, TestRunSummaryReport> map = new Dictionary<string, TestRunSummaryReport>();
            string key = "";
            foreach (TestCase tcase in run.TestCases)
            {
                TestRunSummaryReport reportModel = null;
                switch (by)
                {
                    case ResultBy.Owner:
                        key = tcase.Context.Owner;
                        break;
                    case ResultBy.Priority:
                        key = tcase.Context.Priority.ToString();
                        break;
                    case ResultBy.Module:
                        key = tcase.Context.ModuleName;
                        break;
                    case ResultBy.Category:
                        key = tcase.Context.Category;
                        break;
                }

                if (map.ContainsKey(key))
                {
                    reportModel = map[key];
                }
                else
                {
                    reportModel = new TestRunSummaryReport();
                    map.Add(key, reportModel);
                }

                reportModel.ModuleName = key;
                foreach (TestIteration iteration in tcase.GetIterations())
                {
                    if (iteration.Outcome == TestOutcome.PASSED)
                    {
                        reportModel.Passed = reportModel.Passed + 1;
                    }
                    else if (iteration.Outcome == TestOutcome.FAILED)
                    {
                        reportModel.Failed = reportModel.Failed + 1;
                    }
                    else
                    {
                        reportModel.Others = reportModel.Others + 1;
                    }
                }
            }
            return map;
        }

        public static TestRunSummaryReport GetOverAllSummary(TestRunReport run)
        {
            TestRunSummaryReport reportModel = new TestRunSummaryReport();
            foreach (TestCase tcase in run.TestCases)
            {
                foreach (TestIteration ite in tcase.GetIterations())
                {
                    if (ite.Outcome == TestOutcome.PASSED)
                    {
                        reportModel.Passed = reportModel.Passed + 1;
                    }
                    else if (ite.Outcome == TestOutcome.FAILED)
                    {
                        reportModel.Failed = reportModel.Failed + 1;
                    }
                    else
                    {
                        reportModel.Others = reportModel.Others + 1;
                    }
                }
            }
            return reportModel;
        }
    }
}
