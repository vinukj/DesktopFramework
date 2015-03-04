
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test.Foundation
{
    public class TestCaseInfo
    {
        public string Owner { get; set; }
        public int Priority { get; set; }
        public string Category { get; set; }
        public string TestId { get; set; }
        public string ModuleName { get; set; }
        public string TestName { get; set; }

        public TestContext TestContext { get; set; }

        public TestCaseInfo()
        {
            this.TestId = "Not Specified";
            this.ModuleName = "Not Specified";
        }

        public TestCaseInfo(MethodInfo method, Object instance)
        {
            TestName = method.Name;

            TestCaseBase baseObject = (TestCaseBase)instance;

            Object[] modules = baseObject.GetType().GetCustomAttributes(typeof(ModuleAttribute), false);

            if (modules.Length > 0)
            {
                this.ModuleName = (modules[0] as ModuleAttribute).Name;
            }

            MethodInfo info = method;

            Object[] workItem = info.GetCustomAttributes(typeof(WorkItemAttribute), false);

            if (workItem.Length > 0)
            {
                this.TestId = (workItem[0] as WorkItemAttribute).Id.ToString();
            }

            object[] attr = info.GetCustomAttributes(typeof(OwnerAttribute), true);

            if (attr.Count() > 0)
            {
                Owner = (attr[0] as OwnerAttribute).Owner;
            }

            attr = info.GetCustomAttributes(typeof(PriorityAttribute), true);

            if (attr.Count() > 0)
            {
                Priority = (attr[0] as PriorityAttribute).Priority;
            }

            attr = info.GetCustomAttributes(typeof(TestCategoryAttribute), true);

            if (attr.Count() > 0)
            {
                Category = (attr[0] as TestCategoryAttribute).TestCategories[0];
            }
        }
    }
}
