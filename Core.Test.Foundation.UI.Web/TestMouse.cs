//----------------------------------------------------------------------- // 

//<copyright file="TestMouse.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Aptean.Test.Foundation.UI.Contracts;

namespace Aptean.Test.Foundation.UI
{
    public class TestMouse
    {
        public static void Click(IControl control, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            throw new NotImplementedException("Mouse Click in not implemented");
        }
    }
}
