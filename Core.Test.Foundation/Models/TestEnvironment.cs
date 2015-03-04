

using System.Collections.Generic;
using System.Xml;

namespace Core.Test.Foundation.Models
{
    public class TestEnvironment : ITestModel
    {
        public string Name { get; set; }
        public string UrlOrAppPath { get; set; }
        public BuildInfoModel BuildInfoModel { get; set; }
        private static TestEnvironment Default { get; set; }
        public List<User> Users { get; set; }

        private static Dictionary<string, TestEnvironment> environments;

        public TestEnvironment()
        {
            Users = new List<User>();
        }
        static TestEnvironment()
        {
            environments = new Dictionary<string, TestEnvironment>();
        }

        public static TestEnvironment Get(string name)
        {
            return environments[name];
        }

        //public void Load(string xml)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(xml);
        //    XmlNodeList nodes = doc.SelectNodes("//Environments/WebEnvironment");
        //    foreach (XmlNode node in nodes)
        //    {
        //        TestEnvironment env = new TestEnvironment();
        //        env.Load(node);
        //        if (!environments.ContainsKey(env.Name))
        //        {
        //            environments.Add(env.Name, env);
        //        }
        //    }
        //}

        public void LoadWeb(string xml)   //  Added to Load The Information About  the Web
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodes = doc.SelectNodes("//Environments/WebEnvironment");
            foreach (XmlNode node in nodes)
            {
                TestEnvironment env = new TestEnvironment();
                env.Load(node);
                if (!environments.ContainsKey(env.Name))
                {
                    environments.Add(env.Name, env);
                }
            }
        }

        public void LoadDesktop(string xml)  // Added to Load the Information About Desktop
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodes = doc.SelectNodes("//Environments/DesktopEnvironment");
            foreach (XmlNode node in nodes)
            {
                TestEnvironment env = new TestEnvironment();
                env.Load(node);
                if (!environments.ContainsKey(env.Name))
                {
                    environments.Add(env.Name, env);
                }
            }
        }


        private void Load(XmlNode node)
        {
            XmlElement element = (XmlElement)node;
            this.Name = element.GetAttribute("Name");
            this.UrlOrAppPath = element.GetAttribute("UrlOrAppPath");
            XmlNodeList users = element.GetElementsByTagName("User");
            foreach (XmlNode userNode in users)
            {
                XmlElement userEl = (XmlElement)userNode;
                User user = new User();
                user.Load(userEl);
                this.Users.Add(user);
            }
        }
    }
}
