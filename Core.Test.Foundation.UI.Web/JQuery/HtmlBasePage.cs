//----------------------------------------------------------------------- // 

//<copyright file="HtmlBasePage.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public abstract class HtmlBasePage : BasePage
    {
        public abstract String GetPageXml();
        protected HtmlControlFactory factory;
        protected void InitPage()
        {
            String pageXml = GetPageXml();
            factory = new HtmlControlFactory();
            factory.Init(pageXml);
        }
    }
}
