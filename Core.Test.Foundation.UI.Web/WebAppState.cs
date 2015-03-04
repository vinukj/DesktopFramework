//----------------------------------------------------------------------- // 

//<copyright file="AUTState.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;

namespace Aptean.Test.Foundation.UI
{
    public class WebAppState
    {
        public static IJSExecutor JSEngine { get; set; }

        public static IBrowser Browser { get; set; }

        public static bool IsGood { get; set; }
    }
}
