using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal class PathMaker
    {
        private const int DATE_TAKEN_ID = 36867;
        private static bool _includeYear = true;
        private static bool _includeMonth = true;

        public static bool IncludeYear {  get { return _includeYear; } set { _includeYear = value; } }
        public static bool IncludeMonth { get { return _includeMonth; } set { _includeMonth = value; } }

        public static string Make(string filePath, FileType fileType)
        {
            string path = $@"{BackupRules.FolderNames[(int)fileType]}\";

            FileInfo fileInfo = null;
            DateTime? fileDate = null;
            try
            {
                fileInfo = new FileInfo(filePath);

                using (Image file = Image.FromFile(filePath))
                {
                    PropertyItem dateTakenProp = file.GetPropertyItem(DATE_TAKEN_ID);
                    ASCIIEncoding encoding = new ASCIIEncoding();
                    string dateTakenValue = encoding.GetString(dateTakenProp.Value).Trim('\0');
                    fileDate = DateTime.ParseExact(dateTakenValue, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);
                }
            }
            catch
            {
                if (fileInfo != null)
                {
                    fileDate = fileInfo.LastWriteTime;
                }
            }

            if (!fileDate.HasValue) { return null; }

            if (_includeYear)
            {
                path += $@"{fileDate.Value.Year}\";
            }

            if(_includeMonth)
            {
                path += $"{fileDate.Value.Month}_{(Month)fileDate.Value.Month - 1}";
            }
            
            return path;
        }
    }
}
