using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackupPhotosVideos
{
    internal class BackupRules
    {
        public static readonly string[] AllowedPhotosExtensions = new string[] {
            ".jpeg",
            ".jpg",
            ".png" };

        public static readonly string[] AllowedVideosExtensions = new string[] {
            ".mp4",
            ".mp3",
            ".avi",
            ".3gp",
            ".mov"
        };

        // Values correspond to FileType.cs
        public static readonly string[] FolderNames = new string[] {"Fotos", "Videos"};
    }
}
