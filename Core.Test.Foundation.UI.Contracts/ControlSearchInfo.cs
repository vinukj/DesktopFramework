

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Test.Foundation.UI.Contracts
{
    public class ControlSearchInfo
    {
        public String Name { get; set; }
        public String Path { get; set; }
        public ControlPathType PathType { get; set; }

        public ControlSearchInfo()
        {
            PathType = ControlPathType.JQuery;
        }

        public ControlSearchInfo clone()
        {
            ControlSearchInfo newControl = new ControlSearchInfo();
            newControl.Name = this.Name;
            newControl.Path = this.Path;
            return newControl;
        }
    }
}
