

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Foundation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ModuleAttribute:Attribute
    {
        public string Name { get; set; }
    }
}
