using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VYT.Models
{
    public static class FileUtil
    {
        private static string _currentLocation;

        public static string CurrentLocation
        {
            get
            {
                if (string.IsNullOrEmpty(_currentLocation))
                {
                    var path = Assembly.GetExecutingAssembly().Location ?? "";
                    var i = path.LastIndexOf("\\");
                    _currentLocation = path.Substring(0, i);
                }
                return _currentLocation;
            }
        }
        public static string NextAvailableFilename(string path)
        {
            if (!File.Exists(path))
            {
                return path;
            }
            if (Path.HasExtension(path))
            {
                return GetNextFilename(path.Insert(path.LastIndexOf(Path.GetExtension(path)), "({0})"));
            }
            return GetNextFilename(path + "({0})");
        }

        private static string GetNextFilename(string pattern)
        {
            string tmp = string.Format(pattern, 1);
            if (tmp == pattern)
                throw new ArgumentException("The pattern must include an index place-holder", "pattern");

            if (!File.Exists(tmp))
                return tmp;
            int min = 1, max = 2;
            while (File.Exists(string.Format(pattern, max)))
            {
                min = max;
                max *= 2;
            }
            while (max != min + 1)
            {
                int pivot = (max + min) / 2;
                if (File.Exists(string.Format(pattern, pivot)))
                    min = pivot;
                else
                    max = pivot;
            }

            return string.Format(pattern, max);
        }

        public static void DeleteFolder(string folder)
        {
            try
            {
                if (Directory.Exists(folder))
                {
                    Directory.Delete(folder, true);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.SetAttributes(filePath, FileAttributes.Normal);
                    File.Delete(filePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
