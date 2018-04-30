using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CameraSorter
{
    class Program
    {
        static void Main(string[] args)
        {





            for (int year = 2018; year < 2023; year++)
            {
                for (int month = 1; month < 12; month++)
                {
                    MoveImages(year, month);
                }
            }

        }

        private static void MoveImages(int year, int month)
        {
            var monthFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), string.Format("{0}-{1:00}", year, month));
            var monthFolder = new DirectoryInfo(monthFolderPath);
            if (monthFolder.Exists)
            {
                WorkWithMonthDirectory(monthFolder);
            }
        }
        static string[] extentions = new[] { "jpg", "mp4" };
        private static void WorkWithMonthDirectory(DirectoryInfo monthFolder)
        {
            var files = monthFolder.GetFiles();
            foreach (var file in files)
            {
                var ext = Path.GetExtension(file.Name).Replace(".", string.Empty);
                if (extentions.Contains(ext, StringComparer.CurrentCultureIgnoreCase))
                {
                    var dayDirectory = monthFolder.CreateSubdirectory(file.CreationTime.Day.ToString());
                    var extFolder = dayDirectory.CreateSubdirectory(ext.ToUpper());
                    var newfn = Path.Combine(extFolder.FullName, file.Name);
                    file.MoveTo(newfn);
                }
            }
        }
    }
}
