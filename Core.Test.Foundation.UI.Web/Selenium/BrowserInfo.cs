//----------------------------------------------------------------------- // 

//<copyright file="BrowserInfo.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts.Selenium;

namespace Aptean.Test.Foundation.UI.Web.Selenium
{

    internal class BrowserInfo
    {
        public string ProfilePath { get; set; }

        public BrowserInfo(BrowserType type)
        {
            switch (type)
            {
                case BrowserType.IE:

                    this.ExeName = "iexplore";

                    break;

                case BrowserType.FF:

                    this.ExeName = "firefox";
                    if (OsVersion < 6)
                    {
                        ProfilePath = @"C:\Documents and Settings\{0}\Application Data\Mozilla\Firefox\Profiles";
                    }
                    else
                    {
                        ProfilePath = @"C:\Users\{0}\AppData\Roaming\Mozilla\Firefox\Profiles\";
                    }

                    break;

                case BrowserType.Chrome:

                    this.ExeName = "googlechrome";

                    this.ExePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

                    if (OsVersion < 6)
                    {
                        ProfilePath = @"C:\Documents and Settings\{0}\Local Settings\Application Data\Google\Chrome\User Data";
                    }
                    else
                    {
                        ProfilePath = @"C:\Users\{0}\AppData\Local\Google\Chrome\User Data\";
                    }
                    break;

                case BrowserType.Safari:

                    this.ExeName = "Safari";

                    this.ExePath = @"C:\Program Files (x86)\Safari\Safari.exe";

                    if (OsVersion < 6)
                    {
                        ProfilePath = @"C:\Documents and Settings\{0}\Local Settings\Application Data\Google\Chrome\User Data";
                        //"C:\Program Files (x86)\Safari\"
                    }
                    else
                    {
                        //ProfilePath = @"C:\Users\{0}\AppData\Local\Google\Chrome\User Data\";
                        ProfilePath = @"C:\Users\{0}\AppData\Local\Apple Computer\Safari\";
                       // C:\Users\vjagtap\AppData\Local\Apple Computer\Safari
                    }
                    break;
            }
        }

        public string User
        {
            get
            {
                string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

                userName = userName.Split('\\')[1];

                return userName;
            }
        }

        private int OsVersion
        {
            get
            {
                System.OperatingSystem osInfo = System.Environment.OSVersion;

                int ver = osInfo.Version.Major;

                return ver;
            }
        }

        public string ExeName { get; set; }

        public string ExePath { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(ExePath))
            {
                return string.Format("*{0} {1}", ExeName, ExePath);
            }
            else
            {
                return string.Format("*{0}", ExeName);
            }
        }
    }
}
