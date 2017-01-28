using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DropboxSorter
{
    class Program
    {
        static void Main(string[] args)
        {
           
            var UserFolder=Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string mainFolder = Path.Combine(UserFolder, @"Dropbox\Camera Uploads");
            const string mask = @"\d{4}-\d{2}-\d{2} \d{2}\.\d{2}\.\d{2}.jpg";
            var re=new Regex(mask);
            var directory = new DirectoryInfo(mainFolder);
            var files = directory.GetFiles();
            var filteredfiles = files.Where(x => re.IsMatch(x.Name));


            foreach (var filteredfile in filteredfiles)
            {
                var date = filteredfile.Name.Substring(0, 10);
                const string dateFormat = "yyyy-MM-dd";
                var dt =DateTime.ParseExact(date, dateFormat, CultureInfo.CurrentCulture);
              var subdir=  directory.CreateSubdirectory(dt.ToString(dateFormat));
                var newfn = filteredfile.Name.Substring(11);
                var fullfn = Path.Combine(subdir.FullName, newfn);
                filteredfile.MoveTo(fullfn);
            }
        }
    }
}
