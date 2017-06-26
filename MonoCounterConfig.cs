using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System;
using System.Reflection;

namespace MonoBench
{
    public class MonoCounterConfig : MonoBehaviour
    {
        static char[] delim = { ',' };
        public static List<Counter> Counters;

        public struct Counter
        {
            public string categoryName;
            public string counterName;
            public Counter(string category, string counter)
            {
                this.categoryName = category;
                this.counterName = counter;
            }
        }

        public static void GetConfig()
        {
            Counters = new List<Counter>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load("MonoConfig.xml");
            XmlNodeList userNodes = xmlDoc.SelectNodes("//Categories/counter");
            foreach (XmlNode userNode in userNodes)
            {
                string category = userNode.Attributes["name"].Value;


                string[] counter = userNode.InnerText.Split(delim);
                for (int i = 0; i < counter.Length; i++)
                {
                    Counters.Add(new Counter(category, counter[i]));
                }
            }
        }
    }
}