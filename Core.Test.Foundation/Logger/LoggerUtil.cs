

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Core.Test.Foundation.Logger
{
    public class LoggerUtil
    {
        public LoggerUtil()
        {
        }

        public static string StartNode(string tag)
        {
            return string.Format("<{0}>", tag);
        }

        public static string StartNode(string tag, string attrName, string attrValue)
        {
            return string.Format("<{0}{1}=\"{2}\">", tag, attrName, attrValue);
        }

        public static string StartTd(string alignment)
        {
            return string.Format("<td align=\"{0}\">", alignment);
        }

        public static string StartNode(string tag, string border)
        {
            return string.Format("<{0} border=\"{1}\">", tag, border);
        }

        public static string StartNode(string tag, string border, string attr, string attrVal)
        {
            return string.Format("<{0} border=\"{1}\" {2}=\"{3}\">", tag,border,attr,attrVal);
        }

        public static string CloseNode(string tag)
        {
            return string.Format("</{0}>", tag);
        }

        public static string GetNode(TagType tag, string innerText, string color)
        {
            string style = string.Format("style=\"background-color: {0};\"", color);
            if (color == null)
                return string.Format("<{0}>{1}</{2}>", tag.ToString(), innerText, tag.ToString());
            else
                return string.Format("<{0} {1}>{2}</{3}>", tag.ToString(), style, innerText, tag.ToString());
        }

        public static string GetSelect()
        {
            return "<select onchange=\"changeGroup()\"></select>";
        }

        public static string GetNode(TagType tag, string attrName, string attrValue, string border)
        {
            return string.Format("<{0} border=\"{1}\" {2}=\"{3}\"/>", tag.ToString(), border, attrName, attrValue, tag.ToString());
        }

        public static string GetRow(string inerext)
        {
            StringBuilder build = new StringBuilder();
            build.Append(StartNode(TagType.tr.ToString()));
            build.Append(GetNode(TagType.td, "", ""));
            build.Append(GetNode(TagType.td, inerext, ""));
            build.Append(CloseNode(TagType.tr.ToString()));
            return build.ToString();
        }

        public static string GetImage(string src)
        {
            string imgae = string.Format("<img hideFocus=\"\" style=\"outline-width: medium; outline-style: none; outline-color: invert;\" src=\"{0}\" width=\"950\" height=\"950\">", src);
            StringBuilder build = new StringBuilder();
            build.Append(StartNode(TagType.td.ToString()));
            build.Append(CloseNode(TagType.td.ToString()));
            build.Append(StartNode(TagType.td.ToString()));
            build.Append(imgae);
            build.Append(CloseNode(TagType.td.ToString()));
            return build.ToString();
        }

        public static string GetException(string exception)
        {
            string tag = TagType.td.ToString();
            exception = string.Format("<{0} class=\"Exception\">{1}</{2}>", tag, exception, tag);
            StringBuilder build = new StringBuilder();
            build.Append(StartNode(TagType.tr.ToString()));
            build.Append(GetNode(TagType.td, "", ""));
            build.Append(exception);
            build.Append(CloseNode(TagType.td.ToString()));
            return build.ToString();
        }

        public static string GetTestCasesDetails(Dictionary<string, string> map)
        {
            StringBuilder build = new StringBuilder();

            foreach (KeyValuePair<string, string> keyVal in map)
            {
                build.Append(GetRow((new StringBuilder(keyVal.Key).Append(":").Append(keyVal.Value).ToString())));
            }

            return build.ToString();
        }

        public static string SystemInfo()
        {
            StringBuilder build = new StringBuilder();
            string hostName = "";
            try
            {
                hostName = Dns.GetHostName();
            }
            catch (Exception exe)
            {
            }
            build.Append(GetRow("Computer Name:" + hostName));
            return build.ToString();
        }
    }
}
