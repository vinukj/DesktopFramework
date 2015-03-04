
using System;
using System.IO;
using System.Text;
using Core.Test.Foundation.Logger;
using Core.Test.Foundation.Resources;

namespace Core.Test.Foundation.Reports
{
    public class HTMLTestRunReport : ITestRunReport
    {
        public string GetName()
        {
            return "HTML Report";
        }

        public bool Generate()
        {
            LoggerSettings settings = LoggerSettings.Create();
            string logsPath = string.Format("{0}\\{1}", settings.GetRootDirName(), settings.GetCurrentRunFolderName());
            DirectoryInfo rootPath = new DirectoryInfo(logsPath);
            if (!rootPath.Exists)
            {
                return false;
            }

            string indexFile = string.Format("{0}\\{1}", logsPath, "index.html");

            FileInfo f = new FileInfo(indexFile);
            if (f.Exists)
            {
                f.Delete();
            }

            string failedPath = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), "Failed");

            FileInfo[] passedFiles = GetFiles(logsPath);
            FileInfo[] failedFiles = GetFiles(failedPath);
            int total = passedFiles.Length + failedFiles.Length;

            if (total == 0)
            {
                return false;
            }

            SaveCSSFile(logsPath);
            string zeroFormat = "0.00";
            string passRate = zeroFormat;
            double value;
            if (passedFiles.Length > 0)
            {
                value = double.Parse(passedFiles.Length.ToString()) / double.Parse(total.ToString()) * 100;
                passRate = value.ToString("0.00");
            }
            string failedRate = zeroFormat;
            if (failedFiles.Length > 0)
            {
                value = double.Parse(failedFiles.Length.ToString()) / double.Parse(total.ToString()) * 100;
                failedRate = value.ToString("0.00");
            }

            StringBuilder builder = AddSummaryTable(passedFiles.Length, failedFiles.Length, total, passRate, failedRate);

            builder.Append(LoggerUtil.GetNode(TagType.P, "", ""));

            builder.Append(LoggerUtil.GetNode(TagType.label, "Group By:", ""));
            builder.Append(LoggerUtil.GetSelect());
            builder.Append(LoggerUtil.GetNode(TagType.P, "", ""));
            builder.Append(LoggerUtil.StartNode("table", "1", "id", "groupByTable"));
            builder.Append(LoggerUtil.CloseNode("table"));
            builder.Append(LoggerUtil.GetNode(TagType.P, "", ""));
            builder.Append(LoggerUtil.StartNode("table", "1", "id", "testCaseList"));
            builder.Append(LoggerUtil.StartNode("tr"));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Test case name", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Test Id", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Owner", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Priority", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Category", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Module", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Result", ""));
            builder.Append(LoggerUtil.CloseNode("tr"));


     

            string folder = settings.GetCurrentRunFolderName() + "\\";

            foreach (FileInfo file in failedFiles)
            {
                builder.Append(GetHyperLinkRow(file.Name, "Failed", folder));
            }

            foreach (FileInfo file in passedFiles)
            {
                builder.Append(GetHyperLinkRow(file.Name, "Passed", folder));
            }

            builder.Append(LoggerUtil.CloseNode("table"));

            string content = GetFileContent("ReportsTemplate.htm");
            content = content.Replace("DETAILS", builder.ToString());
            string duplicate = content;
            CopyToTestLogs(duplicate, settings);
            content.Replace(folder, "");
            StreamWriter writer = new StreamWriter(indexFile);
            writer.Write(content);
            writer.Close();
            return true;
        }

        private static void CopyToTestLogs(string content, LoggerSettings settings)
        {
            string logsPath = settings.GetRootDirName();
            DirectoryInfo rootPath = new DirectoryInfo(logsPath);
            if (!rootPath.Exists)
            {
                return;
            }

            string indexFile = string.Format("{0}\\{1}", logsPath, "index.html");

            FileInfo f = new FileInfo(indexFile);
            if (f.Exists)
            {
                f.Delete();
            }

            StreamWriter writer = new StreamWriter(indexFile);
            writer.Write(content);
            writer.Close();

            SaveCSSFile(logsPath);
        }

        private static StringBuilder AddSummaryTable(int passed, int failed, int total, string passRate, string failedRate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(LoggerUtil.GetNode(TagType.h1, " Test Run Report", ""));
            builder.Append(LoggerUtil.StartNode("table", "0"));
            builder.Append(LoggerUtil.StartNode("tr"));

            builder.Append(LoggerUtil.StartNode("td"));
            builder.Append(LoggerUtil.StartNode("table", "1"));
            builder.Append(LoggerUtil.StartNode("tr"));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Result", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "Count", ""));
            builder.Append(LoggerUtil.GetNode(TagType.th, "%", ""));
            builder.Append(LoggerUtil.CloseNode("tr"));
            builder.Append(GetRow("Passed", passed.ToString(), passRate));
            builder.Append(GetRow("Failed", failed.ToString(), failedRate));
            builder.Append(GetRow("Total", total.ToString(), ""));
            builder.Append(LoggerUtil.CloseNode("table"));
            builder.Append(LoggerUtil.StartNode("td"));
            builder.Append(LoggerUtil.CloseNode("td"));
            builder.Append(LoggerUtil.StartNode("td"));
            builder.Append("<canvas class=\"pieChart\" width=\"100\" height=\"100\"></>");
            builder.Append(LoggerUtil.CloseNode("td"));
            builder.Append(LoggerUtil.CloseNode("tr"));
            builder.Append(LoggerUtil.CloseNode("table"));
            return builder;
        }

        private static string GetFileContent(string fileName)
        {
            return Resource.GetFileContent(fileName); ;
        }

        private static void SaveCSSFile(string logsPath)
        {
            try
            {
                string cssFile = "ReportStylesheet.css";
                FileInfo f = new FileInfo(logsPath + "\\" + cssFile);
                if (f.Exists)
                {
                    return;
                }
                string content = GetFileContent(cssFile);
                StreamWriter writer = new StreamWriter(logsPath + "\\" + cssFile);
                writer.Write(content);
                writer.Close();
            }
            catch (Exception exe)
            {

            }
        }

        private static string GetRow(string header, string count, string rate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(LoggerUtil.StartNode("tr"));
            builder.Append(LoggerUtil.StartNode("td"));
            if (header == "Passed")
            {
                builder.Append(string.Format("<a href=\"#\" id=\"passedLink\" onclick=\"passedClick()\">{0}</>", header));
            }
            else if (header == "Failed")
            {
                builder.Append(string.Format("<a  href=\"#\" id=\"failedLink\" onclick=\"failedClick()\">{0}</>", header));
            }
            else
            {
                builder.Append(string.Format("<a  href=\"#\" id=\"totalLink\" onclick=\"totalClick()\">{0}</>", header));
            }
            builder.Append(LoggerUtil.CloseNode("td"));
            //builder.Append(LoggerUtil.GetNode(TagType.td, count, ""));
            builder.Append(string.Format("<td class=\"data\">{0}</td>", count));

            builder.Append(LoggerUtil.GetNode(TagType.td, rate, ""));
            builder.Append(LoggerUtil.CloseNode("tr"));
            return builder.ToString();
        }

        private static string GetHyperLinkRow(string fileName, string label, string folder)
        {
            string testName = GetTestName(fileName.Replace(".html", ""));
            TestCase testCase = TestLogsDB.GetTestCase(testName);
            StringBuilder builder = new StringBuilder();
            builder.Append(LoggerUtil.StartNode("tr", "class", label));
            builder.Append(LoggerUtil.StartTd("left"));
            string labelTd = "";
            if (label.Equals("Passed"))
            {
                builder.Append(string.Format("<a href=\"{0}\">{1}</>", folder + "\\" + fileName, fileName.Replace(".html", "")));
                labelTd = "<td style=\"background-color:green\"></>";
            }
            else
            {
                builder.Append(string.Format("<a href=\"{0}\">{1}</>", folder + "\\" + "Failed\\" + fileName, fileName.Replace(".html", "")));
                labelTd = "<td style=\"background-color:red\"></>";
            }
            builder.Append(LoggerUtil.CloseNode("td"));
            builder.Append(LoggerUtil.GetNode(TagType.td, testCase.Context.TestId, ""));
            builder.Append(LoggerUtil.GetNode(TagType.td, testCase.Context.Owner, ""));
            builder.Append(LoggerUtil.GetNode(TagType.td, testCase.Context.Priority.ToString(), ""));
            builder.Append(LoggerUtil.GetNode(TagType.td, testCase.Context.Category, ""));
            builder.Append(LoggerUtil.GetNode(TagType.td, testCase.Context.ModuleName, ""));
            builder.Append(labelTd);
            builder.Append(LoggerUtil.CloseNode("tr"));
            return builder.ToString();
        }

        public static string GetTestName(string fileName)
        {
            string[] tokens = fileName.Split('_');
            bool canConvert = true;
            try
            {
                int.Parse(tokens[tokens.Length - 1]);
            }
            catch (Exception exe)
            {
                canConvert = false;
            }
            if (tokens.Length > 1 && canConvert)
            {
                int index = tokens.Length - 1;
                int length = fileName.Length - (tokens[index].Length + 1);
                return fileName.Substring(0, length);
            }
            else
            {
                return fileName;
            }
        }

        public static FileInfo[] GetFiles(string dirName)
        {
            DirectoryInfo dir = new DirectoryInfo(dirName);
            if (!dir.Exists)
            {
                return new FileInfo[] { };
            }
            return dir.GetFiles("*.html");
        }

        public static FileInfo[] GetFailedFiles()
        {
            LoggerSettings settings = LoggerSettings.Create();
            string failedPath = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), "Failed");
            return GetFiles(failedPath);
        }
    }
}
