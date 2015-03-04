
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core.Test.Foundation.Resources
{
    internal class Resource
    {
        public static string GetFileContent(string file)
        {
            string path = String.Format("Core.Test.Foundation.Resources.{0}", file);
            return GetContent(path);

        }

        private static string GetContent(String file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = file;
            string result = "";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
