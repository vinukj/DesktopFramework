

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Core.Test.Foundation;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.Models;
using Core.Test.Foundation.Reports;

namespace Core.Test.Foundation
{
    public class Config
    {
        public string LogsRootPath { get; set; }
        public string TestLogsUrl { get; set; }
        public string Environment { get; set; }
        public bool PushLogsToSplunk { get; set; }
        public bool LoggerEnabled { get; set; }

        private static Dictionary<string, string> settings;
        private static Dictionary<string, string> configKeys;
        private static EmailSettings mailSettings;
        private static bool isRead;

        public Config()
        {
            this.LoggerEnabled = true;
            this.LogsRootPath = "C:\\TestLogs";
        }
        public  string GetAppSettings(string key)
        {
            if (settings.ContainsKey(key))
            {
                return settings[key];
            }
            return "";
        }

        public EmailSettings GetEmailSettings()
        {
            return mailSettings;
        }

        public void Load(string xml)
        {
            if (isRead)
            {
                return;
            }

            PushLogsToSplunk = false;
            XmlDocument documet = new XmlDocument();
            documet.LoadXml(xml);
          
            settings = new Dictionary<string, string>();
            isRead = true;
            try
            {
                XmlElement doc = documet.DocumentElement;
                XmlNodeList nodes = documet.SelectNodes("//AppConfig/AppSettings/Data");
                for (int i = 0; i < nodes.Count; i++)
                {
                    XmlElement controlEl = (XmlElement)nodes[i];
                    string key = controlEl.GetAttribute("key");
                    string value = controlEl.GetAttribute("value");
                    if (!settings.ContainsKey(key))
                    {
                        settings.Add(key, value);
                    }
                }

                XmlElement email = (XmlElement)documet.SelectSingleNode("//AppConfig/EmailSettings");
                if (email != null)
                {
                    mailSettings = new EmailSettings();
                    mailSettings.Load(email);
                }

                ReadConfig();

                foreach (string key in configKeys.Keys)
                {
                    if (settings.ContainsKey(key))
                    {
                        settings.Remove(key);
                    }
                    settings.Add(key, configKeys[key]);
                }


                if (settings.ContainsKey("LogsRootPath"))
                {
                    LogsRootPath = settings["LogsRootPath"];
                    LoggerSettings.SetRootFolder(LogsRootPath);
                }

                if (settings.ContainsKey("EnableLogger"))
                {
                    LoggerEnabled = bool.Parse(settings["EnableLogger"]);
                    TestLogger.LoggerEnabled = LoggerEnabled;
                }

                if (settings.ContainsKey("PushLogsToSplunk"))
                {
                    PushLogsToSplunk = bool.Parse(settings["PushLogsToSplunk"]);
                }

                if (settings.ContainsKey("TestLogUrl"))
                {
                    TestLogsUrl = settings["TestLogUrl"];
                }

                if (settings.ContainsKey("Environment"))
                {
                    Environment = settings["Environment"];
                }

            }
            catch (Exception exe)
            {
            }

            if (mailSettings != null)
            {
                UpdateEmailSettings(mailSettings);
            }
        }

        private void ReadConfig()
        {
            configKeys = new Dictionary<string, string>();
            string fileName = AppDomain.CurrentDomain.BaseDirectory;
            fileName = fileName + "\\Config.txt";
            if (!File.Exists(fileName))
            {
                return;
            }

            StreamReader reader = new StreamReader(fileName);
            string content = reader.ReadToEnd();
            reader.Close();
            string[] tokens = content.Split(',');

            foreach (string token in tokens)
            {
                string[] tk = token.Split('=');
                if (tk.Length >= 2)
                {
                    if (!configKeys.ContainsKey(tk[0].Trim()))
                    {
                        configKeys.Add(tk[0].Trim(), tk[1].Trim());
                    }
                }
            }
        }

        public TestEnvironment GetDesktopActiveEnvironment()
        {
            string name = GetAppSettings(ConfigKeys.DesktopEnvironment);
            return TestEnvironment.Get(name);
        }

        public TestEnvironment GetWebActiveEnvironment()
        {
            string name = GetAppSettings(ConfigKeys.WebEnvironment);
            return TestEnvironment.Get(name);
        }
        private void UpdateEmailSettings(EmailSettings mailSettings)
        {
            if (configKeys.ContainsKey("To"))
            {
                mailSettings.To = configKeys["To"].Split(';');
            }
            if (configKeys.ContainsKey("Subject"))
            {
                mailSettings.Subject = configKeys["Subject"];
            }
            if (configKeys.ContainsKey("IncludeTest"))
            {
                mailSettings.IncludeTest = (IncludeTest)Enum.Parse(typeof(IncludeTest), configKeys["IncludeTest"]);
            }
            if (configKeys.ContainsKey("ResultBy"))
            {
                mailSettings.By = (ResultBy)Enum.Parse(typeof(ResultBy), configKeys["ResultBy"]);
            }
        }
    }
}
