
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Foundation.Utils
{
    public class StringUtil
    {
        static string db = "abcdefghijklmnopqrstuvwxyz1234567890";

        static int seed = 0;

        public static string GetRandomName(int length)
        {
            seed++;

            string name = string.Empty;

            Random r = new Random();

            int charIndex = 0;

            for (int i = 0; i < length; i++)
            {
                charIndex = r.Next(0, db.Length - 1);

                name += db[charIndex];
            }

            return name;
        }
    }
}
