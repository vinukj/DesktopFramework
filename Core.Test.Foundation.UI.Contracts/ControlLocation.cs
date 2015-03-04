

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Get and set the control location in case the object identification happens via x & y co-ordinates
namespace Core.Test.Foundation.UI.Contracts
{
    public class ControlLocation
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
