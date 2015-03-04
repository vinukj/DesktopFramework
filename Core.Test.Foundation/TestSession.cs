

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Test.Foundation
{
    public class TestSession
    {
        public static Config Config { get; set; }

        private static Dictionary<string, Object> db;

        static TestSession()
        {
            db = new Dictionary<string, Object>();
        }

        public static void Start()
        {
            Assert.IsNotNull(Config, "Config cannot be null");
            db = new Dictionary<string, Object>();
        }

        public static void Register(string key, Object value)
        {
            if (db.ContainsKey(key))
            {
                db.Remove(key);
            }
            db.Add(key, value);
        }

        public static void UnRegister(string key)
        {
            if (db.ContainsKey(key))
            {
                db.Remove(key);
            }
        }
        public static T Get<T>(string key)
        {
            if (db == null)
            {
                Start();
            }
            if (!db.ContainsKey(key))
            {
                return default(T);
            }

            return (T)db[key];
        }

        public static void Stop()
        {
            db.Clear();
        }
    }
}
