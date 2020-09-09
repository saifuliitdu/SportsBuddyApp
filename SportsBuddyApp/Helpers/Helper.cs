using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SportsBuddy.Helpers
{
    public class Helper
    {
        static string[] patterns = new[]
                     {
                                "*.jpg", "*.jpeg", "*.jpe", "*.jif", "*.jfif", "*.jfi", "*.webp", "*.gif", "*.png",
                                "*.apng", "*.bmp", "*.dib", "*.tiff", "*.tif", "*.svg", "*.svgz", "*.ico", "*.xbm"
                            };
        public static string GetIamgeNameFromUrl(string url)
        {
            var imageNamePart = url.Split('/').ToList().Last();
            var name = imageNamePart.Split('?')[0];
            return name ?? "";
        }

        public static string[] GetFilesFromDirectory(string path, SearchOption options = SearchOption.TopDirectoryOnly)
        {
            if (patterns == null || patterns.Length == 0)
                return Directory.GetFiles(path, "*", options);
            if (patterns.Length == 1)
                return Directory.GetFiles(path, patterns[0], options);
            return patterns.SelectMany(pattern => Directory.GetFiles(path, pattern, options)).Distinct().ToArray();
        }
        public static string GetImage(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            //string path = Server.MapPath("~/images/computer.png");
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            return imageDataURL;
        }
        public static string GetImageBytes(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            //string path = Server.MapPath("~/images/computer.png");
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            string imageBase64Data = Convert.ToBase64String(imageByteData);
            // string imageDataURL = string.Format("data:image/png;base64,{0}", imageBase64Data);
            return imageBase64Data;
        }
        public static byte[] GetImageBytesForZip(string path)
        {
            if (!File.Exists(path))
            {
                return null;
            }
            byte[] imageByteData = System.IO.File.ReadAllBytes(path);
            return imageByteData;
        }

        public static void Log(string logMessage)
        {
            string logDirectory = $"D:/WebApp_CutOutWiz_Logs";
            if (!System.IO.Directory.Exists(logDirectory))
            {
                System.IO.Directory.CreateDirectory(logDirectory);
            }

            string logFile = $"{logDirectory}/{DateTime.Today.ToString("dd-MM-yyy")}_SchedulerLogs.txt";
            if (!System.IO.File.Exists(logFile))
            {
                System.IO.File.Create(logFile).Close();
            }

            using (StreamWriter w = System.IO.File.AppendText(logFile))
            {
                w.Write("\r\nLog Entry : ");
                w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                    DateTime.Now.ToLongDateString());
                w.WriteLine("  :{0}", logMessage);
                w.WriteLine("-------------------------------");
            }
        }
    }
}
