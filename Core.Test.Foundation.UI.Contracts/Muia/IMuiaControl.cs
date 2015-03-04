

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Test.Foundation.UI.Contracts.Muia
{
    public interface IMuiaControl:IControl
    {
        Rectangle GetBoundingRectangel(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true);

    }
}
