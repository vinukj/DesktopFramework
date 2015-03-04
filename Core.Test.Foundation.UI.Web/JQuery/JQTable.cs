//----------------------------------------------------------------------- // 

//<copyright file="JQTable.cs" company="Aptean"> // 
//Copyright (c) Aptean. All rights reserved. // 
//<author> Vinay Jagtap K </author> // <date>10/11/2014 11:02:08 AM</date> // </copyright> 
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Aptean.Test.Foundation.UI.Contracts;
using Aptean.Test.Foundation.UI.Contracts.JQuery;

namespace Aptean.Test.Foundation.UI.Web.JQuery
{
    public class JQTable : JQControl, IJQTable
    {
        public bool WaitForData(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetWaitingForDataMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            int count = 0;
            for (int i = 0; i < retry.GetHashCode(); i++)
            {
                count = GetRowCount(ControlRetry.Super, false);
                if (count > 0)
                {
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            if (throwOnFail && count <= 0)
            {
                string msg = LogGen.GetDataNotLoadedMessage(ControlName, retry);
                throw new Exception(msg);
            }
            return count > 0;
        }

        public void ClickCell(string searchstring, int cellIndex, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            ClickCell(searchstring, cellIndex, "", retry, throwOnFail);
        }

        public void ClickCell(string searchstring, int cellIndex, string extendedPath, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetClickTableCellMessage(ControlName, searchstring));
            this.CheckVisibility(retry, throwOnFail);
            string rowPath = "";
            if (extendedPath != null && extendedPath.Trim().Length > 0)
            {
                rowPath = string.Format(searchInfo.Path + " " + "tr:contains('{0}') td:eq({1}) {2}",
                        searchstring, cellIndex, extendedPath);
            }
            else
            {
                rowPath = string.Format(searchInfo.Path + " " + "tr:contains('{0}') td:eq({1})",
                        searchstring, cellIndex);
            }
            JQControl button = GetControl(rowPath, searchstring);
            button.Click(retry, throwOnFail);
        }

        public void ClickRow(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSelectTableRowMessage(ControlName, index));
            this.CheckVisibility(retry, throwOnFail);
            string rowPath = string.Format(searchInfo.Path + " " + "tr:eq({0})", index);
            JQControl button = GetControl(rowPath, "Row At Index " + index);
            button.Click(retry, throwOnFail);
        }

        public int GetRowCount(ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetGetRowCountMessage(ControlName));
            this.CheckVisibility(retry, throwOnFail);
            string rowPath = searchInfo.Path + " tr";
            string script = JQBuilder.GetElementCountScript(rowPath);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? 0 : int.Parse(value.ToString());
        }

        public int FindRowIndex(string searchstring, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetSearchTableMessage(ControlName, searchstring));
            this.CheckVisibility(retry, throwOnFail);
            string rowPath = string.Format(searchInfo.Path + " " + "tr:contains('{0}')", searchstring);
            string script = JQBuilder.GetElementIndexScript(rowPath);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? -1 : int.Parse(value.ToString());
        }

        public string[] GetRowData(string searchstring, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetGetRowDataMessage(ControlName, searchstring));
            this.DisableLogging = true;
            int index = FindRowIndex(searchstring, retry, throwOnFail);
            string[] data = GetRowData(index, retry, throwOnFail);
            this.DisableLogging = false;
            return data;
        }

        public string[] GetRowData(int index, ControlRetry retry = ControlRetry.Avg, bool throwOnFail = true)
        {
            this.WriteMessage(LogGen.GetGetRowDataMessage(ControlName, index));
            this.CheckVisibility(retry, throwOnFail);
            string rowPath = string.Format(searchInfo.Path + " " + "tbody tr:eq({0}) td", index);
            string script = JQBuilder.GetGetRowDataScript(rowPath);
            Object value = WebAppState.JSEngine.EvaluateScript(script);
            return value == null ? null : value.ToString().Split(',');
        }

        private JQControl GetControl(string path, string name)
        {
            JQButton button = new JQButton();
            button.DisableLogging = true;
            ControlSearchInfo info = new ControlSearchInfo();
            info.Path = path;
            info.Name = name;
            button.ControlSearchInfo = info;
            return button;
        }
    }
}
