

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Core.Test.Foundation.Logger
{
    internal class HtmlLogger : ILogger
    {
        public Dictionary<string, string> testCasesAttributes;
        private string logFileName = "";
        private LoggerSettings settings;
        private bool logGenerated;
        private string imageFileName;
        private Exception exception;
        private StringBuilder testSteps;
        private StreamWriter logWriter;

        public HtmlLogger(LoggerSettings logSettings)
        {
            SetLogSettings(logSettings);
            testSteps = new StringBuilder();

        }

        public LoggerSettings GetLogSettings()
        {
            return settings;
        }

        public void SetLogSettings(LoggerSettings logSettings)
        {
            settings = logSettings;
        }

        public void Init()
        {
            string folder = new StringBuilder(settings.GetRootDirName()).Append("\\").Append(settings.GetCurrentRunFolderName()).ToString();
            DirectoryInfo dir = new DirectoryInfo(folder);
            if(!dir.Exists)
                dir.Create();

            logFileName = new StringBuilder(folder).Append("\\").Append(testCasesAttributes["Name"]).Append(settings.GetLogFileExtension()).ToString();     

            if(!TestLogger.LoggerEnabled){
                return;
            }
            SaveStyleSheet("");
        }

        public void Write(Exception exception)
        {
            TestLogger.TestCasesFailed = true;
            this.exception = exception;

            if (!TestLogger.LoggerEnabled)
            {
                return;
            }

            WriteImage();
        }

        public void Write(string message)
        {
            if (!TestLogger.LoggerEnabled)
            {
                return;
            }

            testSteps.Append(LoggerUtil.GetRow(message));
            testSteps.Append(Environment.NewLine);
        }

        public void Clear()
        {
            if (!TestLogger.LoggerEnabled)
            {
                return;
            }

            string content = WriteEverything();
            testSteps = new StringBuilder();
            if (TestLogger.TestCasesFailed)
            {
                MoveToFailedFolder(content);
            }
            else
            {
                try
                {
                    logWriter = new StreamWriter(logFileName);
                    logWriter.Write(content);
                    logWriter.Close();
                }
                catch (Exception exception1) { }
            }
        }

        private void MoveToFailedFolder(string content)
        {
            if(content == ""){
                return;
            }

            string failedFolder = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), settings.GetFailedFolderName());
            string newFilePath = string.Format("{0}\\{1}", failedFolder, (new StringBuilder(testCasesAttributes["Name"]).Append(settings.GetLogFileExtension()).ToString()));

            DirectoryInfo dir = new DirectoryInfo(failedFolder);
            if(!dir.Exists)
                dir.Create();

            StreamWriter sw = new StreamWriter(newFilePath);
            sw.Write(content);
            sw.Close();

            SaveStyleSheet(settings.GetFailedFolderName());
        }

        private string GetContent(String file)
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

        private void SaveStyleSheet(string destination)
        {
            string content = GetContent("Core.Test.Foundation.Resources.Stylesheet.css");
            string newFilePath = "";
            if (destination != "")
                newFilePath = string.Format("{0}\\{1}\\{2}\\{3}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), destination, "Stylesheet.css");
            else
                newFilePath = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), "Stylesheet.css");

            StreamWriter sw = new StreamWriter(newFilePath);
            sw.Write(content);
            sw.Close();
        }

        private void WriteImage()
        {
            if (!TestLogger.GetTestType().Equals(TestType.UI))
            {
                return;
            }
            try
            {
                CaptureImage();
            }
            catch (Exception exe) 
            {
                Console.Write(exe.Message);
            }

            string file = string.Format("{0}/{1}/{2}{3}", settings.GetImageRelativePath(), settings.GetImageFolderName(), testCasesAttributes["Name"], settings.GetImageFileExtension());
            imageFileName = LoggerUtil.GetImage(file);
        }

        private string WriteEverything()
        {
            if (logGenerated)
                return "";

            string content = GetContent("Core.Test.Foundation.Resources.Template.htm");
            logGenerated = true;
            string data = "";
            DateTime date = DateTime.Now;
            testCasesAttributes.Add("Log generate time", date.ToString());
            content = content.Replace("TESTCASEDETAILS", LoggerUtil.GetTestCasesDetails(testCasesAttributes));
            content = content.Replace("SYSTEMINFO", LoggerUtil.SystemInfo());
            if (TestLogger.TestCasesFailed)
                content = LogFailureInfo(content);
            else
            {
                data = LoggerUtil.GetRow("Test case passed");
                testSteps.Append(data);
            }
            if (content.Contains("EXCEPTIONINFO"))
            {
                data = LoggerUtil.GetRow("No failures");
                content = content.Replace("EXCEPTIONINFO", data);
                if (TestLogger.GetTestType().Equals(TestType.UI))
                {
                    data = LoggerUtil.GetRow("UI was in expected state");
                    content = content.Replace("IMAGEINFO", data);
                    content = content.Replace("IMAGEPATH", "");
                }
                else
                {
                    content = content.Replace("UI State", "");
                    content = content.Replace("IMAGEINFO", "");
                    content = content.Replace("IMAGEPATH", "");
                }
            }
            data = testSteps.ToString();
            content = content.Replace("STEPSINFO", data);

            return content;
        }

        private string LogFailureInfo(string content)
        {
            string imageFolderPath = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), settings.GetImageFolderName());
            string fileName = string.Format("{0}\\{1}", imageFolderPath, (new StringBuilder(testCasesAttributes["Name"]).Append(settings.GetImageFileExtension()).ToString()));

            string message = "";
            string trace = "";

            if (exception != null)
            {
                message = exception.Message;
            }

            try
            {
                content = content.Replace("EXCEPTIONINFO", LoggerUtil.GetException(message));
                if (TestLogger.GetTestType().Equals(TestType.UI))
                {
                    content = content.Replace("IMAGEPATH", LoggerUtil.GetRow((new StringBuilder("Image location:")).Append(fileName).ToString()));
                    content = content.Replace("IMAGEINFO", imageFileName);
                }
                else
                {
                    content = content.Replace("UI State", "");
                    content = content.Replace("IMAGEINFO", "");
                    content = content.Replace("IMAGEPATH", "");
                }
                testSteps.Append(LoggerUtil.GetRow("Test cases failed"));
                testSteps.Append(Environment.NewLine);
                testSteps.Append(LoggerUtil.GetRow("Exception details"));
                testSteps.Append(Environment.NewLine);
                testSteps.Append(LoggerUtil.GetRow((new StringBuilder("Message:")).Append(message).ToString()));
                testSteps.Append(Environment.NewLine);
                testSteps.Append(LoggerUtil.GetRow((new StringBuilder("StackTrace:")).Append(trace).ToString()));
                testSteps.Append(Environment.NewLine);
            }
            catch (Exception exe)
            {

            }

            return content;
        }

        private string CaptureImage()
        {
            System.Threading.Thread.Sleep(2000);
            string imageFolderPath = string.Format("{0}\\{1}\\{2}", settings.GetRootDirName(), settings.GetCurrentRunFolderName(), settings.GetImageFolderName());
            DirectoryInfo dir = new DirectoryInfo(imageFolderPath);
            if (!dir.Exists)
                dir.Create();
            String s = new StringBuilder(testCasesAttributes["Name"]).Append(settings.GetImageFileExtension()).ToString();

            string fileName = string.Format("{0}\\{1}", imageFolderPath, s);
            ScreenCaptureUtil.CaptureImage(fileName);
            return fileName;
        }

    }
}
