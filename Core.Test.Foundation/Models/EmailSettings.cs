
using System;
using System.Xml;
using Core.Test.Foundation.Reports;

namespace Core.Test.Foundation.Models
{
    public class EmailSettings
    {
        public string[] To { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }

        public string SMTPServer { get; set; }

        public IncludeTest IncludeTest { get; set; }
        public bool Enabled { get; set; }
        public ResultBy By { get; set; }

        public void Load(XmlElement ele)
        {
            this.From = ele.SelectSingleNode("From").InnerText;
            this.To = ele.SelectSingleNode("To").InnerText.Split(';');
            this.Password = ele.SelectSingleNode("Password").InnerText;
            this.Subject = ele.SelectSingleNode("Subject").InnerText;
            this.SMTPServer = ele.SelectSingleNode("SMTPServer").InnerText;
            this.IncludeTest = (IncludeTest)Enum.Parse(typeof(IncludeTest), ele.SelectSingleNode("IncludeTest").InnerText);
            this.Enabled = bool.Parse(ele.SelectSingleNode("Enabled").InnerText);
            this.By = (ResultBy)Enum.Parse(typeof(ResultBy), ele.SelectSingleNode("ResultBy").InnerText);
        }
    }
}
