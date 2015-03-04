//----------------------------------------------------------------------- // 


using System.IO;
using Core.Test.Foundation.Models;

namespace Core.Test.Foundation.Configs
{
  public  class TestConfig
    {
        public static void LoadAll(){
            TestSession.Config = new Config();
            TestSession.Config.Load(GetAppConfigXml());
           // new TestEnvironment().Load(GetEnvXml());
           // Added the LoadWeb() And LoadDesktop() to load the Web and Desktop Elements(Environment,Url) values form the xml to Launch Both the Application
            new TestEnvironment().LoadWeb(GetEnvXml());
            new TestEnvironment().LoadDesktop(GetEnvXml()); 
        }

        private static string GetAppConfigXml()
        {
            string path = "Configs\\AppConfig.xml";
            return Read(path); 
        }

        private static string GetEnvXml()
        {
            string path = "Configs\\EnvironmentConfig.xml";
            return Read(path); 
        }

        private static string Read(string file)
        {
            StreamReader reader = new StreamReader(file);
            string xml = reader.ReadToEnd();
            reader.Close();
            return xml;
        }
    }
}
