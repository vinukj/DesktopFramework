

using System;
using System.IO;

namespace Core.Test.Foundation.Logger
{
    public class LoggerSettings
    {
        private static LoggerSettings settings;
        public static LoggerSettings Create()
        {
            if (settings == null)
            {
                settings = new LoggerSettings();
            }

            return settings;
        }

        private LoggerSettings()
        {
            DateTime date = DateTime.Now;
            CurrentRunFolderName = date.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        public string GetImageFolderName()
        {
            return ImageFolderName;
        }

        public string GetFailedFolderName()
        {
            return FailedFolderName;
        }

        public string GetImageRelativePath()
        {
            return ImageRelativePath;
        }

        public string GetImageFileExtension()
        {
            return ImageFileExtension;
        }

        public string GetLogFileExtension()
        {
            return LogFileExtension;
        }

        public string GetRootDirName()
        {
            DirectoryInfo dr = new DirectoryInfo(RootDirName);
            if (RootDirName == null || RootDirName.Equals("ProjectPath") || !dr.Exists)
            {
                RootDirName = GetDefaultPath();
            }
            return RootDirName;
        }

        public string GetCurrentRunFolderName()
        {
            return CurrentRunFolderName;
        }

        public static void SetRootFolder(string root)
        {
            RootDirName = root;
            DirectoryInfo dr = new DirectoryInfo(root);
            if (!dr.Exists)
            {
                try
                {
                    dr.Create();                  
                }
                catch
                {
                    RootDirName = GetDefaultPath();
                }
            }
        }

        private static string GetDefaultPath()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "\\TestLogs";
            return fileName;
        }

        private readonly string ImageFolderName = "Images";
        private readonly string FailedFolderName = "Failed";
        private readonly string ImageRelativePath = "..";
        private readonly string ImageFileExtension = ".png";
        private readonly string LogFileExtension = ".html";
        private static string RootDirName = "C:\\TestLogs";
        private readonly string CurrentRunFolderName;
    }
}
