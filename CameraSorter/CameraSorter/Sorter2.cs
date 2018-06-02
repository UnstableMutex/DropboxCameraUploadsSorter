using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraSorter
{
    class Sorter2
    {
        public void Sort()
        {
            var date = new DateTime(2018, 5, 1);
            var enddate = new DateTime(2023, 1, 1);


            while (date < enddate)
            {
                date = date.AddDays(1);
                MoveImages(date);

            }

        }
        static string[] extentions = new[] { "jpg", "mp4" };
        private void MoveImages(DateTime date)
        {
            var dateFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), date.ToString("yyy-MM-dd"));
            if (!Directory.Exists(dateFolderPath))
            {
                return;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(dateFolderPath);
                var files = di.GetFiles();
                foreach (var file in files)
                {
                    if (extentions.Contains(file.Extension.Replace(".", string.Empty), StringComparer.CurrentCultureIgnoreCase))
                    {
                        MoveFile(file);
                    }
                }
                files = di.GetFiles();
                if (!files.Any())
                {
                    di.Delete();
                }
            }
        }

        private void MoveFile(FileInfo file)
        {
            var fn = Path.GetFileNameWithoutExtension(file.Name);
            var ext = Path.GetExtension(file.Name).Replace(".", string.Empty).ToUpper();
            //2018-05-31 014.JPG
            var dt = fn.Substring(0, 10);
            var date = DateTime.ParseExact(dt, "yyyy-MM-dd", CultureInfo.CurrentCulture);
            var monthFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), date.ToString("yyyy-MM"));
            DirectoryInfo di = new DirectoryInfo(monthFolderPath);
            if (!di.Exists)
            {
                di.Create();
            }
            var dayDir = di.CreateSubdirectory(date.ToString("dd"));
            var extDir = dayDir.CreateSubdirectory(ext);

            var outFileName = Path.Combine(extDir.FullName, file.Name.Substring(10).Trim());
            file.MoveTo(outFileName);


        }
    }
}
